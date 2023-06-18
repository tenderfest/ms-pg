namespace PgConvert.Config
{
	public class OnePgDatabase
	{
		public const string PostgresDatabase = "postgres";
		public const string ThisIgnore = "Игнорировать";

		private const string _mustBeSpecified = " должен быть указан!";

		public OnePgDatabase(string databaseName)
		{
			Name = databaseName;
		}

		public string Name { get; set; }
		public string ConnectionString { get; set; }
		public DtElement[] Elements { get; set; }

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

		public bool IsDefault =>
			Name == ThisIgnore;
		public override string ToString() =>
			Name;

		public string SetConnectionString(string server, string port, string login, string password, string name)
		{
			if (string.IsNullOrEmpty(server))
				return "Сервер" + _mustBeSpecified;
			if (string.IsNullOrEmpty(port))
				return "Порт" + _mustBeSpecified;
			if (string.IsNullOrEmpty(login))
				return "Логин" + _mustBeSpecified;
			if (string.IsNullOrEmpty(password))
				return "Пароль" + _mustBeSpecified;
			var bdName = string.IsNullOrEmpty(name) ? PostgresDatabase : name;

			ConnectionString = $"Server={server};Port={port};Database={bdName};UID={login};PWD={password}";
			return null;
		}
	}
}
