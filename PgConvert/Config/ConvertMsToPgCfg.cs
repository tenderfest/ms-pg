using System.Text;

namespace PgConvert.Config
{
	[Serializable]
	public class ConvertMsToPgCfg
	{
		public ConvertMsToPgCfg()
		{
			Databases = new OnePgDatabase[]
			{
				new OnePgDatabase {
					Name = "Игнорировать",
				}
			};
		}
		public OnePgDatabase[] Databases { get; set; }
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
	}
}
