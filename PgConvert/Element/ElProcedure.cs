namespace PgConvert.Element;

public class ElProcedure : DtElement
{
	public ElProcedure(string[] lines) : base(lines)
	{
		ElementType = ElmType.Procedure;
	}
	internal override string Parse()
	{

		return null;
	}

}
