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
	public OnePgDatabase(string databaseName) =>
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

	public string GetBdName =>
		PgConnectionString?.DatabaseName;
	public string GetServer =>
		PgConnectionString?.Server;
	public string GetPort =>
		PgConnectionString?.Port;
	public string GetLogin =>
		PgConnectionString?.Login;
	public string GetPassword =>
		PgConnectionString?.Password;

	/// <summary>
	/// Относится ли элемент к этой БД?
	/// </summary>
	/// <param name="hashCode">Хэш-код проверяемого элемента</param>
	/// <returns>true, если проверяемый элемент отнесён к это БД, иначе false</returns>
	internal bool IsContainsElementIds(int hashCode) =>
		null != _elementIds && _elementIds.Contains(hashCode);

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

		try
		{
			var database = PgConnectionString.DatabaseName;
			var sb = new StringBuilder($"CREATE DATABASE {database}");
			if (!string.IsNullOrEmpty(TableSpace))
				sb.Append($" TABLESPACE {TableSpace}");
			sb.Append(';');
			using NpgsqlConnection connection = new(PgConnectionString.GetPostgresConnectionString());
			connection.Open();
			new NpgsqlCommand($"{sb}", connection).ExecuteNonQuery();
			return $"База данных '{database}' создана.";
		}
		catch (Exception ex)
		{
			return $"Ошибка при создании базы данных: {ex.Message}";
		}
	}

	public override string ToString() =>
		Name;
}
