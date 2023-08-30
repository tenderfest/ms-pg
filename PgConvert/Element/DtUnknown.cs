namespace PgConvert.Element;

public class DtUnknown : DtElement
{
	public DtUnknown(string[] lines) : base(lines)
	{
		ElementType = ElmType.None;
	}
}
