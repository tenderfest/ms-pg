namespace PgConvert.Element;

internal class ElRole : DtElement
{
	public ElRole(string[] lines) : base(lines)
	{
		ElementType = ElmType.Role;
	}
}
