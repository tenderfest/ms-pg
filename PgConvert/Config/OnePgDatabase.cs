namespace PgConvert.Config
{
	public class OnePgDatabase
	{
		public const string ThisIgnore = "Игнорировать";
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
	}
}
