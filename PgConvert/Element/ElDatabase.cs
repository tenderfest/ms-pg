namespace PgConvert.Element;

public class ElDatabase : DtElement
{
	public ElDatabase(string[] lines) : base(lines)
	{
		ElementType = ElmType.Database;
	}

	internal override string Parse()
	{
		Ignore = ElmOperation.Alter == Operation;
		return null;
	}
}
