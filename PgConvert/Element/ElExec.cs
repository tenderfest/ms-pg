namespace PgConvert.Element;

internal class ElExec : DtElement
{
	public ElExec(string[] lines) : base(lines)
	{
		ElementType = ElmType.Exec;
	}
	internal override string Parse()
	{

		return null;
	}
}
