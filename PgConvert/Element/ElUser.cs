namespace PgConvert.Element;

internal class ElUser : DtElement
{
	public ElUser()
	{
		ElementType = ElmType.User;
	}
	internal override string Parse()
	{
		// CREATE USER [tf] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]

		return null;
	}
}
