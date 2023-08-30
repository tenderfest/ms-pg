namespace PgConvert.Element;

internal class ElUser : DtElement
{
	public ElUser(string[] lines) : base(lines)
	{
		ElementType = ElmType.User;
	}
}
