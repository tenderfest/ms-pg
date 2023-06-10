namespace PgConvert
{
	public abstract class ElBaseForTable : DtElement
	{
		internal string TableName { get; set; }

		internal void SetTableName(string tableName)
		{
			TableName = tableName;
		}

		public override string ToString() =>
			$"{ElementOperation.GetOperationSign(Operation)} {Type}: {Name} ON {TableName}";
	}
}
