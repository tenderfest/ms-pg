using System.IO.Compression;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using PgConvert.Config;
using PgConvert.Element;

namespace PgConvert;

public enum EditElementsType
{
	/// <summary> Все </summary>
	All,
	/// <summary> Таблицы </summary>
	Table,
	/// <summary> Процедуры </summary>
	Procedure,
	/// <summary> Триггер </summary>
	Trigger,
}
public enum ShowEditElements
{
	/// <summary> Все </summary>
	All,
	/// <summary> Требующие внимания </summary>
	Alert,
	/// <summary> Утверждённые </summary>
	Ok,
}

public class ConvertMsToPg
{
	const string _cfgFileName = "ConvertMsToPg.Cfg";
	//public const string _extProj = ".ms2pg";
	public const string _extProj = ".zip";
	public const string _extSql = ".sql";

	const string GO = "GO";
	const string CREATE_TABLE = "CREATE TABLE";

	private bool NeedUpdateFile = false;
	private bool NeedUpdateConfig = false;

	public ConvertMsToPgCfg Config { get; private set; } = new();
	public string FullFilePath { get; set; }
	List<string> InFile { get; set; }
	List<DtElement> Elements { get; set; }

	/// <summary>
	/// База данных, выбранная в настоящий момент ползователем
	/// </summary>
	public OnePgDatabase SelectedDataBase { get; set; }

	/// <summary>
	/// Набор элементов, выбранных для перемещения в БД, либо для отмены такого перемещения
	/// </summary>
	public List<DtElement> ElementsForAddDatabase { get; set; }

	/// <summary>
	/// Тип элементов для отображения на вкладке "Доработка текстов процедур"
	/// </summary>
	private EditElementsType CurrentEditElementsType { get; set; }

	/// <summary>
	/// Статус элементов для отображения на вкладке "Доработка текстов процедур"
	/// </summary>
	private ShowEditElements CurrentShowEditElements { get; set; }

	/// <summary>
	/// База данных, выбранная на вкладке "Доработка текстов процедур", null="все базы данных"
	/// </summary>
	public OnePgDatabase CurrentEditDatabase { get; set; }

	private static readonly JsonSerializerOptions _jsonOptions = new()
	{
		Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
		WriteIndented = true,
	};

	#region config

	public void SetConfig(
		List<OnePgDatabase> databases,
		int[] freeElementIds,
		string[] skipOperation,
		string[] skipElement)
	{
		Config = new ConvertMsToPgCfg
		{
			Databases = databases,
			FreeElementIds = freeElementIds,
			SkipElement = skipOperation,
			SkipOperation = skipElement,
		};
		NeedUpdateConfig = true;
	}
	#endregion config

	public DtElement[] GetElements(ElmType selectedElementType, bool createOnly)
	{
		IEnumerable<DtElement> elements = null != SelectedDataBase
			? SelectedDataBase.Elements
			: Config.FreeElements;
		if (null == elements)
			return Array.Empty<DtElement>();

		elements = elements.Where(s =>
			s.ElementType == selectedElementType);
		if (createOnly)
			elements = elements.Where(t =>
				t.Operation == ElmOperation.Create);

		return elements.ToArray();
	}

	private ElTable[] allTables = null;
	private ElTable[] Tables
	{
		get
		{
			if (null == allTables)
			{
				allTables = Elements
				.Where(e =>
					e.ElementType == ElmType.Table)
				.Select(t =>
					t as ElTable)
				.ToArray();
			}
			return allTables;
		}
	}

	private ElTrigger[] allTriggers = null;
	private ElTrigger[] Triggers
	{
		get
		{
			if (null == allTriggers)
			{
				allTriggers = Elements
				.Where(e =>
					e.ElementType == ElmType.Trigger)
				.Select(t =>
					t as ElTrigger)
				.ToArray();
			}
			return allTriggers;
		}
	}

	public IEnumerable<OnePgDatabase> GetDatabases =>
		Config.Databases;

	/// <summary>
	/// Есть ли выбранные элементы для перемещения в базу данных
	/// </summary>
	public bool IsPresentElementsForAddDatabase =>
		null != ElementsForAddDatabase;

	public string LoadFile(string fileName) =>
		Path.GetExtension(fileName) switch
		{
			_extSql => LoadMsSql(fileName),
			_extProj => LoadProj(fileName),
			_ => $"Неизвестный формат файла {fileName}",
		};

	private string LoadProj(string fileName)
	{
		FullFilePath = fileName;
		try
		{
			using var stream = new FileStream(fileName, FileMode.Open);
			using var zip = new ZipArchive(stream, ZipArchiveMode.Read, false);
			foreach (var entry in zip.Entries)
			{
				using var reader = new StreamReader(entry.Open());

				// чтение настроек
				if (entry.Name == _cfgFileName)
				{
					var cfgText = reader.ReadToEnd();
					Config = (ConvertMsToPgCfg)JsonSerializer.Deserialize(cfgText, typeof(ConvertMsToPgCfg));
					continue;
				}

				if (Path.GetExtension(entry.Name) != _extSql)
					continue;

				// чтение обрабатываемого файла
				ReadInLines(reader);
			}
		}
		catch (Exception ex) { return ex.Message; }
		return null;
	}

	private string LoadMsSql(string fileName)
	{
		FullFilePath = fileName;
		try
		{
			using StreamReader reader = new(fileName);
			ReadInLines(reader);
		}
		catch (Exception ex) { return ex.Message; }

		if (!InFile.Any())
			return $"Файл '{fileName}' пуст";

		NeedUpdateFile = true;

		if (null == Config.SkipOperation)
			return "Нужно проверить настройки";
		return null;
	}

	private void ReadInLines(StreamReader reader)
	{
		// сохранение исходника
		InFile = new();
		while (true)
		{
			var inLine = reader.ReadLine();
			if (inLine == null) break;
			InFile.Add(inLine);
		}
	}

	#region ParseSource

	/// <summary>
	/// разбор файла
	/// </summary>
	public string ParseSource()
	{
		string errorMessage;

		// чтение и разбор входного потока
		errorMessage = ParseStrings();
		if (!string.IsNullOrEmpty(errorMessage))
			return errorMessage;

		// разбор каждого элемента по составляющим
		errorMessage = ParseElements(Config.AddNeedCorrect);
		if (!string.IsNullOrEmpty(errorMessage))
			return errorMessage;

		// установка взаимосвязей между элементами
		errorMessage = RelationElements();
		if (!string.IsNullOrEmpty(errorMessage))
			return errorMessage;

		// разнесение элементов по разным БД
		errorMessage = SortingElements();
		if (!string.IsNullOrEmpty(errorMessage))
			return errorMessage;

		return null;
	}

	/// <summary>
	/// Установка взаимосвязей между элементами
	/// </summary>
	private string RelationElements()
	{
		var createTables = Tables
			.Where(e =>
				e.Operation == ElmOperation.Create)
			.ToArray();

		foreach (var table in createTables)
		{
			// изменения таблицы
			table.AddAlterTable(
				Tables
				.Where(at =>
					at.Operation == ElmOperation.Alter && at.IsRelatedToTable(table.Name))
				.ToArray());

			// триггеры
			table.AddTriggers(
				Triggers
				.Where(tr =>
					tr.IsRelatedToTable(table.Name))
				.ToArray());
		}

		return null;
	}

	/// <summary>
	/// Разнесение элементов по базам данных
	/// </summary>
	private string SortingElements()
	{
		if (Config.Databases.Contains(null))
			Config.Databases = Config.Databases
				.Where(db =>
					db != null)
				.ToList();

		foreach (var db in Config.Databases)
			db.Elements ??= new List<DtElement>();

		int freeElementsNum = null == Config.FreeElementIds
			? 0
			: Config.FreeElementIds.Length;
		try
		{
			List<DtElement> freeElements = new();
			foreach (var element in Elements)
			{
				foreach (var db in GetDatabases.Where(db => db.IsContainsElementIds(element.Id)))
				{
					element.Database = db;
					db.Elements.Add(element);
				}

				if (null == element.Database)
					freeElements.Add(element);
			}
			Config.FreeElements = freeElements;
			if (freeElementsNum != Config.FreeElementIds.Length)
				NeedUpdateConfig = true;

			return null;
		}
		catch (Exception ex)
		{
			return ex.Message;
		}
	}

	/// <summary>
	/// Разбор каждого элемента по составляющим
	/// </summary>
	private string ParseElements(Action<DtElement> addNeedCorrect)
	{
		foreach (var element in Elements)
		{
			var errorMessage = element.Parse();
			if (!string.IsNullOrEmpty(errorMessage))
				return errorMessage;

			// определение элементов, требующих доработки
			if (element.ElementType == ElmType.Table && ((ElTable)element).IsGeneratedFields ||
				element.ElementType == ElmType.Procedure ||
				element.ElementType == ElmType.Trigger)
			{
				addNeedCorrect(element);
			}
		}

		// сортировка элементов по имени
		Elements
			.Sort((a, b) =>
				a.Name.CompareTo(b.Name));
		return null;
	}

	/// <summary>
	/// Чтение и разбор входного потока
	/// </summary>
	private string ParseStrings()
	{
		List<string> inLines = new();
		List<DtElement> dtElements = new();
		foreach (var inLine in InFile)
		{
			// дошли до конца определения элемента, сохраняем его
			if (inLine == GO)
			{
				if (!inLines.Any())
					continue;

				var dicValue = DtElement.GetElement(inLines.ToArray(), Config);
				if (default != dicValue)
				{
					var equalElement = dtElements
						.Find(e =>
							e.Equals(dicValue));
					if (default == equalElement)
						dtElements.Add(dicValue);
				}

				inLines = new();
			}
			else if (!string.IsNullOrEmpty(inLine))
			{
					inLines.Add(inLine);
			}
		}

		Elements = dtElements;
		return null;
	}

	#endregion

	public string SaveFile(string path, out string projectFile)
	{
		projectFile = null;
		if (string.IsNullOrEmpty(path))
			return "Необходимо указать путь для сохраняемого файла";

		try
		{
			projectFile = Path.ChangeExtension(FullFilePath, _extProj);
			using var stream = new FileStream(Path.Combine(path, projectFile), FileMode.OpenOrCreate);
			using var zip = new ZipArchive(stream, ZipArchiveMode.Update, false);

			// сохранение обрабатываемого файла
			if (NeedUpdateFile)
			{
				var fileName = Path.GetFileName(FullFilePath);
				var fileEntry = zip.GetEntry(fileName);
				fileEntry?.Delete();
				fileEntry = zip.CreateEntry(fileName);
				using var writer = new StreamWriter(fileEntry.Open());
				InFile.ForEach(writer.WriteLine);
			}

			// сохранение настроек
#if !DEBUG
			if (NeedUpdateConfig)
#endif
			{
				var configEntry = zip.GetEntry(_cfgFileName);
				configEntry?.Delete();
				configEntry = zip.CreateEntry(Path.GetFileName(_cfgFileName));
				using var writer = new StreamWriter(configEntry.Open());
				JsonSerializer.Serialize(writer.BaseStream, Config, _jsonOptions);
			}
		}
		catch (Exception ex) { return ex.Message; }

		NeedUpdateFile = NeedUpdateConfig = false;
		return null;
	}

	public void SetElementsForAddDatabase(List<DtElement> list) =>
		ElementsForAddDatabase = list;

	public void AddSelectedElementsToDatabase() =>
		SetElementsToDatabase(ElementsForAddDatabase);
	public void RemoveElementsFromDatabase(List<DtElement> selectedElements) =>
		RemoveFromDatabase(selectedElements);

	private void SetElementsToDatabase(IEnumerable<DtElement> elementsForAddDatabase)
	{
		if (null == elementsForAddDatabase ||
			null == SelectedDataBase)
			return;

		foreach (var element in elementsForAddDatabase)
		{
			if (SelectedDataBase.Elements.Contains(element))
				continue;

			if (element.Database != null && element.Database != SelectedDataBase)
				element.Database.Elements.Remove(element);
			else
				Config.FreeElements.Remove(element);

			element.Database = SelectedDataBase;
			SelectedDataBase.Elements.Add(element);
			if (element is ElTable table)
			{
				SetElementsToDatabase(table.AlterTable);
				SetElementsToDatabase(table.Triggers);
			}
		}
	}

	private void RemoveFromDatabase(IEnumerable<DtElement> elementsForAddDatabase)
	{
		if (null == elementsForAddDatabase)
			return;

		foreach (var element in elementsForAddDatabase)
		{
			Config.AddFreeElements(element);
			if (element.Database != null)
			{
				element.Database.Elements.Remove(element);
				element.Database = null;
			}
			if (element is ElTable table)
			{
				RemoveFromDatabase(table.AlterTable);
				RemoveFromDatabase(table.Triggers);
			}
		}
	}

	public void SetEditElementsType(EditElementsType editElementsType) =>
		CurrentEditElementsType = editElementsType;

	public void SetShowEditElements(ShowEditElements showEditElements) =>
		CurrentShowEditElements = showEditElements;

	public List<DtElement> GetEditElements()
	{
		var editElements = Elements.AsEnumerable().Where(e => e.Operation == ElmOperation.Create);
		if (CurrentEditDatabase != null)
			editElements = editElements.Where(e => e.Database == CurrentEditDatabase);

		var elmType = EditElementsTypeToElmType(CurrentEditElementsType);
		if (elmType != ElmType.None)
			editElements = editElements.Where(e => e.ElementType == elmType);

		switch (CurrentShowEditElements)
		{
			case ShowEditElements.Alert:
				editElements = editElements.Where(e => !e.IsOk);
				break;
			case ShowEditElements.Ok:
				editElements = editElements.Where(e => e.IsOk);
				break;
		}
		return editElements.ToList();
	}

	private static ElmType EditElementsTypeToElmType(EditElementsType editElementsType) =>
		 editElementsType switch
		 {
			 EditElementsType.Table => ElmType.Table,
			 EditElementsType.Procedure => ElmType.Procedure,
			 EditElementsType.Trigger => ElmType.Trigger,
			 _ => ElmType.None,
		 };
}
