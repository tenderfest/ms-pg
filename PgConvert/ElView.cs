namespace PgConvert
{
	public class ElView : DtElement
	{
		ElTable[] Tables { get; set; }

		public ElView()
		{
			Type = ElmType.View;
		}
	}
}
