namespace PgConvert.Element;

public class ElView : DtElement
{
	ElTable[] Tables { get; set; }

	public ElView()
	{
		SelectFor = ElmType.View;
	}

	internal override string Parse()
	{

		return null;
	}
}
