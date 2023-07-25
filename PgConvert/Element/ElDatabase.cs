namespace PgConvert.Element;

public class ElDatabase : DtElement
{
	public ElDatabase()
	{
		ElementType = ElmType.Database;
	}

	internal override string Parse()
	{
		Ignore = ElmOperation.Alter == Operation;
		return null;
	}
}
