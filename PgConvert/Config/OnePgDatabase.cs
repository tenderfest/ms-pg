using Npgsql;
using PgConvert.Element;
using System.Text.Json.Serialization;

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
	/// Название БД для создания её на сервере
	/// </summary>
	public const string _postgresDatabase = "postgres";

	private const string _mustBeSpecified = " должен быть указан!";
	private const string _dbServer = "Server";
	private const string _dbPort = "Port";
	private const string _dbDatabase = "Database";
	private const string _dbUID = "UID";
	private const string _dbPWD = "PWD";

	/// <summary>
	/// Идентификаторы элементов, отнесённых к этой БД
	/// </summary>
	private int[] _elementIds;

	/// <summary>
	/// Конструктор
	/// </summary>
	public OnePgDatabase() { }

	/// <summary>
	/// Конструктор
	/// </summary>
	public OnePgDatabase(string databaseName) =>
		Name = databaseName;

	/// <summary>
	/// Строка подключения
	/// </summary>
	public string ConnectionString { get; set; }

	/// <summary>
	/// Название базы данных
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Части строки подключения
	/// </summary>
	private Dictionary<string, string> PartsOfConnectionString =>
		string.IsNullOrEmpty(ConnectionString)
		? new()
		: ConnectionString
			.Split(';', StringSplitOptions.RemoveEmptyEntries)
			.Select(x => x.Split('=', StringSplitOptions.TrimEntries))
			.Where(x => null != x && x.Any() && x.Length == 2)
			.ToDictionary(x => x[0], y => y[1]);

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
	/// Сборка строки подключения из элементов
	/// </summary>
	/// <returns>null, если сборка была успешной, иначе сообщение об ошибке</returns>
	public string SetConnectionString(string server, string port, string name, string login, string password)
	{
		if (string.IsNullOrEmpty(server))
			return $"Сервер{_mustBeSpecified}";
		if (string.IsNullOrEmpty(port))
			return $"Порт{_mustBeSpecified}";
		if (string.IsNullOrEmpty(login))
			return $"Логин{_mustBeSpecified}";
		if (string.IsNullOrEmpty(password))
			return $"Пароль{_mustBeSpecified}";
		var bdName = string.IsNullOrEmpty(name) ? _postgresDatabase : name;

		ConnectionString = MakeConnectionString(server, port, bdName, login, password);
		return null;
	}

	/// <summary>
	/// Получение строки подключенея из элементов
	/// </summary>
	private static string MakeConnectionString(
		string server,
		string port,
		string databaseName,
		string login,
		string password) =>
		$"{_dbServer}={server};{_dbPort}={port};{_dbDatabase}={databaseName};{_dbUID}={login};{_dbPWD}={password}";

	/// <summary>
	/// Попытка создать базу данных на сервере
	/// </summary>
	/// <returns>сообщение о результате создания базы данных на сервере</returns>
	public string TryCreate()
	{
		var _partsOfConnectionString = PartsOfConnectionString;
		if (string.IsNullOrEmpty(ConnectionString)
			|| null == _partsOfConnectionString
			|| !_partsOfConnectionString.Any())
			return "Не определена строка подключения!";

		string error = IsNotConnectionStringKey(_dbServer);
		if (error != null) return error;
		error = IsNotConnectionStringKey(_dbPort);
		if (error != null) return error;
		error = IsNotConnectionStringKey(_dbDatabase);
		if (error != null) return error;
		error = IsNotConnectionStringKey(_dbUID);
		if (error != null) return error;
		error = IsNotConnectionStringKey(_dbPWD);
		if (error != null) return error;

		try
		{
			var database = _partsOfConnectionString[_dbDatabase];
			var pgConnectionString = MakeConnectionString(
				_partsOfConnectionString[_dbServer],
				_partsOfConnectionString[_dbPort],
				_postgresDatabase,
				_partsOfConnectionString[_dbUID],
				_partsOfConnectionString[_dbPWD]);

			using NpgsqlConnection connection = new(pgConnectionString);
			connection.Open();
			new NpgsqlCommand($"CREATE DATABASE {database}", connection)
				.ExecuteNonQuery();
			return $"База данных '{database}' создана.";
		}
		catch (Exception ex)
		{
			return $"Ошибка при создании базы данных: {ex.Message}";
		}

		string IsNotConnectionStringKey(string key) =>
			_partsOfConnectionString.ContainsKey(key)
			? null
			: $"В строке подключения отсутствует ключ '{key}'.";
	}

	public override string ToString() =>
		Name;
}
