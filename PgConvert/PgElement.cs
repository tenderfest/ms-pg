namespace PgConvert
{
	public class PgElement
	{
		public ElmType Type { get; }
		public ElmOperation Operation { get; }
		public string Name { get; }
		public string GetEmenenlContentPostgreSql =>
			"тест О";
	}
}