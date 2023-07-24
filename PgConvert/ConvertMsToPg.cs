using System;
using System.IO.Compression;
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
	Dictionary<int, DtElement> Elements { get; set; }

	private static readonly JsonSerializerOptions _jsonOptions = new()
	{
		Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
		WriteIndented = true,
	};

	#region config
	public ConvertMsToPgCfg GetConfig()
		=> Config;

	public void SetConfig(ConvertMsToPgCfg newCfg)
	{
		Config = newCfg;
		NeedUpdateConfig = true;
	}
	#endregion config

	public DtElement[] GetAllElements()
		=> null == Elements
		? Array.Empty<DtElement>()
		: Elements
		.Select(s => s.Value)
		.ToArray();

	public DtElement[] GetElements(ElmType elmType)
		=> null == Elements
		? Array.Empty<DtElement>()
		: Elements
		.Where(s => elmType == s.Value.SelectFor)
		.Select(s => s.Value)
		.ToArray();

	public string LoadFile(string fileName)
		=> Path.GetExtension(fileName) switch
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
				// чтение настроек
				if (entry.Name == _cfgFileName)
				{
					using var reader = new StreamReader(entry.Open());
					string cfgText = reader.ReadToEnd();
					Config = (ConvertMsToPgCfg)JsonSerializer.Deserialize(cfgText, typeof(ConvertMsToPgCfg));
					continue;
				}

				if (Path.GetExtension(entry.Name) != _extSql)
					continue;

				// чтение обрабатываемого файла
				using (var reader = new StreamReader(entry.Open()))
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

	/// <summary>
	/// разбор файла
	/// </summary>
	public string ParseSource()
	{
		string errorMessage;

		errorMessage = ParseStrings();
		if (!string.IsNullOrEmpty(errorMessage))
			return errorMessage;

		errorMessage = ParseElements();
		if (!string.IsNullOrEmpty(errorMessage))
			return errorMessage;

		// установка взаимосвязей

		return null;
	}

	/// <summary>
	/// Разбор каждого элемента по состовляющим
	/// </summary>
	private string ParseElements()
	{
		foreach (var el in Elements.Values)
		{
			string errorMessage = el.Parse();
			if (!string.IsNullOrEmpty(errorMessage))
			{
				return errorMessage;
			}
		}
		return null;
	}

	/// <summary>
	/// Чтение и разбор входного потока
	/// </summary>
	private string ParseStrings()
	{
		int i = 0;
		List<string> inLines = new();
		List<string> commentBuffer = new();
		Elements = new();
		foreach (var inLine in InFile)
		{
			// дошли до конца определения элемента, сохраняем его
			if (inLine == GO)
			{
				var dicValue = DtElement.GetElement(inLines, commentBuffer, Config);
				if (default != dicValue)
				{
					var equalElement = Elements.Values.FirstOrDefault(e => e.Equals(dicValue));
					if (equalElement == default)
						Elements.Add(i++, dicValue);
					else
						return $"Элемент {dicValue} уже есть в общем списке элементов";
				}

				inLines = new();
				commentBuffer = new();
			}
			else if (!string.IsNullOrEmpty(inLine))
			{
				if (inLine.StartsWith("--"))
					commentBuffer.Add(inLine);
				else
					inLines.Add(inLine);
			}
		}
		return null;
	}

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
			if (NeedUpdateConfig)
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
