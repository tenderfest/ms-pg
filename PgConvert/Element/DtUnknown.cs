namespace PgConvert.Element;

public class DtUnknown : DtElement
{
	public DtUnknown()
	{
		ElementType = ElmType.None;
	}

	internal override string Parse() => null;
}
