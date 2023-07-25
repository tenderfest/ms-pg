namespace PgConvert.Element;

public class ElIndex : ElBaseForTable
{
	public IdxType IndexType { get; set; }
	public string[] FieldNames { get; set; }

	public ElIndex()
	{
		ElementType = ElmType.Index;
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0056:Использовать оператор индекса", Justification = "<Ожидание>")]
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
