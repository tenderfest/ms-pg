namespace PgConvert.Element;

public class ElTable : DtElement
{
	DtField[] Fields { get; set; }

	public override DtField[] GetChild
		=> Fields;

	public ElTable()
	{
		SelectFor = ElmType.Table;
	}

	internal override void Parse()
	{

	}
}
