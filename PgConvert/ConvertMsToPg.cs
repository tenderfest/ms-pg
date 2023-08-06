﻿using System.IO.Compression;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using PgConvert.Config;
using PgConvert.Element;

namespace PgConvert;

public class ConvertMsToPg
{
	const string _cfgFileName = "ConvertMsToPg.Cfg";
	public const string _extProj = ".ms2pg";
	public const string _extSql = ".sql";

	const string GO = "GO";
	const string CREATE_TABLE = "CREATE TABLE";

	private bool NeedUpdateFile = false;
	private bool NeedUpdateConfig = false;

	ConvertMsToPgCfg Config { get; set; } = new();
	public string FullFilePath { get; set; }
	List<string> InFile { get; set; }
	List<DtElement> Elements { get; set; }

	private static readonly JsonSerializerOptions _jsonOptions = new()
	{
		Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
		WriteIndented = true,
	};

	#region config
	public ConvertMsToPgCfg GetConfig() =>
		Config;

	public void SetConfig(
		OnePgDatabase[] databases,
		DtElement[] freeElements,
		string[] skipOperation,
		string[] skipElement)
	{
		Config = new ConvertMsToPgCfg
		{
			Databases = databases,
			FreeElements = freeElements,
			SkipElement = skipOperation,
			SkipOperation = skipElement,
		};
		NeedUpdateConfig = true;
	}
	#endregion config

	public DtElement[] GetAllElements() =>
		null == Elements
		? Array.Empty<DtElement>()
		: Elements.ToArray();

	public DtElement[] GetElements(ElmType elmType)
	{
		if (null == Elements)
			return Array.Empty<DtElement>();

		var elements = Elements.Where(s =>
			elmType == s.ElementType);

		//// для таблиц возвращаем только их создание
		//if (ElmType.Table == elmType)
		//	elements = elements.Where(t =>
		//		ElmOperation.Create == t.Operation);

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
		errorMessage = ParseElements();
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
				.Where(e =>
					e.Operation == ElmOperation.Alter && e.Name == table.Name));

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
				.ToArray();

		foreach (var db in Config.Databases)
			db.Elements ??= Array.Empty<DtElement>();

		int freeElementsNum = Config.FreeElements == null ? 0 : Config.FreeElements.Length;
		try
		{
			List<DtElement> freeElements = new();
			foreach (var element in Elements)
			{
				foreach (var db in Config.Databases
					.Where(db =>
						db.Elements.Contains(element)))
				{
					element.Database = db;
				}

				if (null == element.Database)
					freeElements.Add(element);
			}
			Config.FreeElements = freeElements.ToArray();
			if (freeElementsNum != Config.FreeElements.Length)
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
	private string ParseElements()
	{
		foreach (var element in Elements)
		{
			var errorMessage = element.Parse();
			if (!string.IsNullOrEmpty(errorMessage))
			{
				return errorMessage;
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
		List<string> commentBuffer = new();
		List<DtElement> dtElements = new();
		foreach (var inLine in InFile)
		{
			// дошли до конца определения элемента, сохраняем его
			if (inLine == GO)
			{
				if (!inLines.Any())
					continue;

				var dicValue = DtElement.GetElement(inLines, commentBuffer, Config);
				if (default != dicValue)
				{
					var equalElement = dtElements
						.Find(e =>
							e.Equals(dicValue));
					if (equalElement == default)
						dtElements.Add(dicValue);
					else
						return $"Элемент {dicValue} уже есть в общем списке элементов";
				}

				inLines = new();
				commentBuffer = new();
			}
			else if (!string.IsNullOrEmpty(inLine))
			{
				if (inLine.TrimStart().StartsWith("--"))
					commentBuffer.Add(inLine);
				else
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
}
