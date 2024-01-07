using Npgsql;
using PgConvert.Element;
using System.Text;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PgConvert.Config;

/// <summary>
/// Информация об одной базе данных PostgreSQL
/// </summary>
#pragma warning disable S2365 // Properties should not make collection or array copies
#pragma warning disable S4275 // Getters and setters should access the expected fields
public class OnePgDatabase
{
	/// <summary>
	/// Название "встроенной" БД
	/// </summary>
	public const string _thisDbIsIgnore = "Игнорировать";

	/// <summary>
	/// Идентификаторы элементов, отнесённых к этой БД
	/// </summary>
	private int[] _elementIds;

	/// <summary>
	/// Конструктор
	/// </summary>
	public OnePgDatabase() =>
		PgConnectionString = new PgConnectionString();

	/// <summary>
	/// Конструктор
	/// </summary>
	public OnePgDatabase(string databaseName) : this() =>
		Name = databaseName;

	/// <summary>
	/// Строка подключения
	/// </summary>
	public string ConnectionString
	{
		get =>
			PgConnectionString.GetConnectionString(IsDefault);
		set
		{
			PgConnectionString = new PgConnectionString(value);
			if (!string.IsNullOrEmpty(PgConnectionString.Error))
			{
				// TODO куда девать сообщение об ошибке?
			}
		}
	}

	/// <summary>
	/// Табличное пространство для БД, если оно не по умолчанию
	/// </summary>
	public string TableSpace { get; set; }

	/// <summary>
	/// Название базы данных
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Элементы строки подключения к БД
	/// </summary>
	[JsonIgnore]
	public PgConnectionString PgConnectionString { get; private set; }

	/// <summary>
	/// Элементы, отнесённые к этой БД
	/// </summary>
	[JsonIgnore]
	public List<DtElement> Elements { get; set; }

	/// <summary>
	/// Является ли эта БД базой данных "встроенной", куда относятся игнорируемые элементы
	/// </summary>
	[JsonIgnore]
	public bool IsDefault =>
		Name == _thisDbIsIgnore;

	/// <summary>
	/// Идентификаторы элементов, отнесённых к этой БД
	/// </summary>
	public int[] ElementIds
	{
		get =>
			Elements?.Select(e => e.Id).ToArray();
		set =>
			_elementIds = value;
	}

	/// <summary>
	/// Получить новое определение БД на основе существующей строки подключения к тому же серверу БД
	/// </summary>
	[JsonIgnore]
	public OnePgDatabase PartCopy =>
		new()
		{
			PgConnectionString = PgConnectionString.PartCopy,
		};

	/// <summary>
	/// Относится ли элемент к этой БД?
	/// </summary>
	/// <param name="hashCode">Хэш-код проверяемого элемента</param>
	/// <returns>true, если проверяемый элемент отнесён к это БД, иначе false</returns>
	internal bool IsContainsElementIds(int hashCode) =>
		null != _elementIds &&
		_elementIds.Contains(hashCode);

	/// <summary>
	/// Метод проверки подключения к этой БД
	/// </summary>
	/// <returns>null, если проверка была успешной, иначе сообщение об ошибке</returns>
	public string TestConnectDatabase()
	{
		if (IsDefault)
			return "База данных 'Игнорировать' не предназначена для подключения.";

		try
		{
			using NpgsqlConnection connection = new(ConnectionString);
			connection.Open();
		}
		catch (Exception ex)
		{
			return $"Ошибка подключения к БД PostgreSQL: {ex.Message}";
		}
		return null;
	}

	/// <summary>
	/// Попытка создать базу данных на сервере
	/// </summary>
	/// <returns>сообщение о результате создания базы данных на сервере</returns>
	public string TryCreate()
	{
		if (IsDefault)
			return $"База данных '{_thisDbIsIgnore}' не предназначена для содания.";

		if (PgConnectionString.IsError)
			return PgConnectionString.Error;

		var database = PgConnectionString.DatabaseName;
		var sb = new StringBuilder($"CREATE DATABASE {database}");
		if (!string.IsNullOrEmpty(TableSpace))
			sb.Append($" TABLESPACE {TableSpace}");
		sb.Append(';');

		var errMessage = ExecutePostgresCommand(sb.ToString());
		return !string.IsNullOrEmpty(errMessage)
			? $"Ошибка при создании базы данных: {errMessage}"
			: $"База данных '{database}' создана.";
	}

	private string ExecutePostgresCommand(string sqlCommand)
	{
		try
		{
			using NpgsqlConnection connection = new(PgConnectionString.GetPostgresConnectionString());
			connection.Open();
			new NpgsqlCommand(sqlCommand, connection).ExecuteNonQuery();
		}
		catch (Exception ex)
		{
			return ex.Message;
		}
		return null;
	}

	private string QueryPostgresCommand(string sqlCommand, out string[] strings)
	{
		try
		{
			using NpgsqlConnection connection = new(PgConnectionString.GetPostgresConnectionString());
			connection.Open();
			var lines = new List<string>();
			using var reader = new NpgsqlCommand(sqlCommand, connection).ExecuteReader();
			while (reader.Read())
			{
				lines.Add(reader.GetString(0));
			}
			strings = lines.ToArray();
			return null;
		}
		catch (Exception ex)
		{
			strings = null;
			return ex.Message;
		}
	}

	public override string ToString() =>
		Name;

	public string AddTableSpace(string tableSpace, string location) =>
		ExecutePostgresCommand($"CREATE TABLESPACE {tableSpace} LOCATION '{location.Trim()}';");

	public string GetTableSpaces(out string[] strings) =>
		QueryPostgresCommand("SELECT spcname FROM pg_tablespace;", out strings);
}
