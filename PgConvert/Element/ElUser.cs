namespace PgConvert.Element;

internal class ElUser : DtElement
{
	public ElUser()
	{
		SelectFor = ElmType.User;
	}
	internal override void Parse()
	{
		// CREATE USER [tf] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]

	}
}
