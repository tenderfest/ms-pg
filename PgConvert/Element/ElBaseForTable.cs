namespace PgConvert.Element;

/// <summary>
/// базовый класс для сущностей, относящихся к таблицам: триггеры и индексы
/// </summary>
public abstract class ElBaseForTable : DtElement
{
	protected ElBaseForTable(string[] lines) : base(lines) { }

	internal string TableName { get; set; }

	internal void SetTableName(string tableName)
	{
		TableName = tableName;
	}

	public override string ToString()
		=> $"{ElementOperation.GetOperationSign(Operation)} {ElementType}: {Name} ON {TableName}";
}
