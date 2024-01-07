namespace PgConvert.Config
{
	/// <summary>
	/// Элементы строки подключения к БД
	/// </summary>
	public class PgConnectionString
	{
		/// <summary>
		/// Название дефолтной БД для создания БД на сервере
		/// </summary>
		private const string _postgresDatabase = "postgres";

		private const string _mustBeSpecified = " должен быть указан!";

		private const string _dbServer = "Server";
		private const string _dbPort = "Port";
		private const string _dbDatabase = "Database";
		private const string _dbUID = "UID";
		private const string _dbPWD = "PWD";

		public string Server { get; set; }
		public string Port { get; set; }
		public string DatabaseName { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }

		public string Error { get; private set; }
		public bool IsError =>
			!string.IsNullOrEmpty(Error);

		/// <summary>
		/// Получение строки подключения к тому же серверу БД
		/// </summary>
		public PgConnectionString PartCopy =>
			new()
			{
				Login = Login,
				Password = Password,
				Server = Server,
				Port = Port,
				DatabaseName = _postgresDatabase,
			};

		public PgConnectionString()
		{ }

		public PgConnectionString(string connectionString)
		{
			var partsOfConnectionString = connectionString
				.Split(';', StringSplitOptions.RemoveEmptyEntries)
				.Select(x => x.Split('=', StringSplitOptions.TrimEntries))
				.Where(x => null != x && x.Any() && x.Length == 2)
				.ToDictionary(x => x[0], y => y[1]);

			if (!partsOfConnectionString.Any())
			{
				Error = "Строка не содержит необходимых элементов.";
				return;
			}

			Error = CheckOfConnectionString(partsOfConnectionString);
			if (!string.IsNullOrEmpty(Error))
				return;

			SetFieldsForConnectionString(
				partsOfConnectionString[_dbServer],
				partsOfConnectionString[_dbPort],
				partsOfConnectionString[_dbDatabase],
				partsOfConnectionString[_dbUID],
				partsOfConnectionString[_dbPWD]);
		}

		private static string IsNotConnectionStringKey(string key, Dictionary<string, string> partsOfConnectionString) =>
			partsOfConnectionString.ContainsKey(key)
			? null
			: $"В строке подключения отсутствует ключ '{key}'.";

		private static string CheckOfConnectionString(Dictionary<string, string> partsOfConnectionString)
		{
			string error;
			error = IsNotConnectionStringKey(_dbServer, partsOfConnectionString);
			if (error != null) return error;
			error = IsNotConnectionStringKey(_dbPort, partsOfConnectionString);
			if (error != null) return error;
			error = IsNotConnectionStringKey(_dbDatabase, partsOfConnectionString);
			if (error != null) return error;
			error = IsNotConnectionStringKey(_dbUID, partsOfConnectionString);
			if (error != null) return error;
			error = IsNotConnectionStringKey(_dbPWD, partsOfConnectionString);
			if (error != null) return error;
			return null;
		}

		/// <summary>
		/// Получение строки подключенея из элементов
		/// </summary>
		private string GetConnectionString(bool isDefault, bool isPostgres)
		{
			if (isDefault)
				return "У этой базы данных нет строки подключения";

			var db = isPostgres
				? _postgresDatabase
				: DatabaseName;
			return $"{_dbServer}={Server};{_dbPort}={Port};{_dbDatabase}={db};{_dbUID}={Login};{_dbPWD}={Password}";
		}

		/// <summary>
		/// Сборка строки подключения из элементов
		/// </summary>
		private void SetFieldsForConnectionString(string server, string port, string name, string login, string password)
		{
			this.Server = server;
			this.Port = port;
			this.Login = login;
			this.Password = password;
			DatabaseName = !string.IsNullOrEmpty(name)
				? name
				: _postgresDatabase;
		}

		internal string GetPostgresConnectionString() =>
			GetConnectionString(false, true);

		public string GetConnectionString(bool isDefault) =>
			GetConnectionString(isDefault, false);
	}
}