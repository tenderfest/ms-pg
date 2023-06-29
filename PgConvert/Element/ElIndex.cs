namespace PgConvert.Element;

public class ElIndex : ElBaseForTable
{
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

	internal override void Parse()
	{

	}
}
