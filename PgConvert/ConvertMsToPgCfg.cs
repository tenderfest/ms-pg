using System.Text;

namespace PgConvert
{
	[Serializable]
	public class ConvertMsToPgCfg
	{
		public string ConnectionStringToDic { get; set; }
		public string ConnectionStringToWrk { get; set; }
		public string ConnectionStringToArc { get; set; }
		public string[] SkipOperation { get; set; }
		public string[] SkipElement { get; set; }
		public DtElement[] ForDatabase_Dict { get; set; }
		public DtElement[] ForDatabase_Work { get; set; }
		public DtElement[] ForDatabase_Ignore { get; set; }

		public static string[] GetSkipArrayFromText(string text) =>
			text?.Split('\n')
			.Select(s => s.ToLower().Trim())
			.Where(s => !string.IsNullOrEmpty(s))
			.ToArray();

		private static string GetStringArrayAsText(string[] stringArray)
		{
			var sb = new StringBuilder();
			stringArray?.ToList().ForEach(s => sb.AppendLine(s));
			return sb.ToString();
		}

		public string GetSkipElementAsText() =>
			GetStringArrayAsText(SkipElement);

		public string GetSkipOperationAsText() =>
			GetStringArrayAsText(SkipOperation);

		public string TestConnectDatabase()
		{
			if (string.IsNullOrEmpty(ConnectionStringToDic))
				return $"{nameof(ConnectionStringToDic)} пуст";
			if (string.IsNullOrEmpty(ConnectionStringToWrk))
				return $"{nameof(ConnectionStringToWrk)} пуст";
			if (string.IsNullOrEmpty(ConnectionStringToArc))
				return $"{nameof(ConnectionStringToArc)} пуст";

			try
			{
				using Npgsql.NpgsqlConnection connection = new(ConnectionStringToDic);
				connection.Open();
			}
			catch (Exception ex) { return $"Ошибка подключения к БД PostgreSQL: {ex.Message}"; }

			try
			{
				using Npgsql.NpgsqlConnection connection = new(ConnectionStringToWrk);
				connection.Open();
			}
			catch (Exception ex) { return $"Ошибка подключения к БД PostgreSQL: {ex.Message}"; }

			try
			{
				using Npgsql.NpgsqlConnection connection = new(ConnectionStringToArc);
				connection.Open();
			}
			catch (Exception ex) { return $"Ошибка подключения к БД PostgreSQL: {ex.Message}"; }

			return null;
		}
	}
}
