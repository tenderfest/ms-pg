using Npgsql;
using PgConvert.Element;
using System.Text.Json.Serialization;

namespace PgConvert.Config;

public class OnePgDatabase
{
	public const string PostgresDatabase = "postgres";
	public const string ThisIgnore = "Игнорировать";

	private const string _mustBeSpecified = " должен быть указан!";
	private const string _dbServer = "Server";
	private const string _dbPort = "Port";
	private const string _dbDatabase = "Database";
	private const string _dbUID = "UID";
	private const string _dbPWD = "PWD";
	private Dictionary<string, string> PartsOfConnectionString =>
		string.IsNullOrEmpty(ConnectionString)
		? new Dictionary<string, string>()
		: ConnectionString
			.Split(';', StringSplitOptions.RemoveEmptyEntries)
			.Select(x => x.Split('=', StringSplitOptions.TrimEntries))
			.Where(x => null != x && x.Any() && x.Length == 2)
			.ToDictionary(x => x[0], y => y[1]);

	public OnePgDatabase() { }
	public OnePgDatabase(string databaseName)
	{
		Name = databaseName;
	}

	public string ConnectionString { get; set; }

	public string Name { get; set; }

	[JsonIgnore]
	public List<DtElement> Elements { get; set; }

	private int[] _elementIds;
	public int[] ElementIds
	{
#pragma warning disable S2365 // Properties should not make collection or array copies
#pragma warning disable S4275 // Getters and setters should access the expected fields
		get => Elements?.Select(e => e.HashCode).ToArray();
#pragma warning restore S4275 // Getters and setters should access the expected fields
#pragma warning restore S2365 // Properties should not make collection or array copies

		set => _elementIds = value;
	}
	internal bool IsContainsElementIds(int hashCode) =>
		null != _elementIds && _elementIds.Contains(hashCode);

	public string TestConnectDatabase()
	{
		try
		{
			using Npgsql.NpgsqlConnection connection = new(ConnectionString);
			connection.Open();
		}
		catch (Exception ex) { return $"Ошибка подключения к БД PostgreSQL: {ex.Message}"; }
		return null;
	}

	[JsonIgnore]
	public bool IsDefault =>
		Name == ThisIgnore;
	public override string ToString() =>
		Name;

	/// <summary>
	/// Сборка строки подключения из элементов
	/// </summary>
	/// <returns>Сообщение об ошибке или null, если ошибки нет</returns>
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
		var bdName = string.IsNullOrEmpty(name) ? PostgresDatabase : name;

		ConnectionString = MakeConnectionString(server, port, bdName, login, password);
		return null;
	}

	/// <summary>
	/// Получить строку подключенея из элементов
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
				PostgresDatabase,
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
}
