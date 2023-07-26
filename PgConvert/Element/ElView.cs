namespace PgConvert.Element;

public class ElView : DtElement
{
	ElTable[] Tables { get; set; }

	public ElView(string[] lines) : base(lines)
	{
		ElementType = ElmType.View;
	}

	internal override string Parse()
	{

		return null;
	}
}
