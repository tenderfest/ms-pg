namespace PgConvert.Element;

public class DtUnknown : DtElement
{
	public DtUnknown()
	{
		SelectFor = ElmType.None;
	}

	internal override void Parse() { }
}
