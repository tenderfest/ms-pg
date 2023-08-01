namespace PgConvert.Element;

internal class ElSchema : DtElement
{
	public ElSchema(string[] lines) : base(lines)
	{
		ElementType = ElmType.Schema;
	}
	internal override string Parse()
	{

		return null;
	}
}
