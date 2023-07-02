namespace PgConvert.Element;

public class ElIndex : ElBaseForTable
{
	public string TableName { get; set; }
	public string[] FieldNames { get; set; }

	public ElIndex()
	{
		SelectFor = ElmType.Index;
	}

	protected override string Name
	{
		get
		{
			if (null == name)
			{
				SetTableName(ClearBraces(FirstLineWords[FirstLineWords.Length - 1]));
				name = ClearBraces(FirstLineWords[FirstLineWords.Length - 1 - 2]);
			}
			return name;
		}
	}

	//public override string ToString()
	//	=> $"{ElementOperation.GetOperationSign(Operation)} {SelectFor}: {Name} ON {TableName}";

	internal override string Parse()
	{

		return null;
	}
}
