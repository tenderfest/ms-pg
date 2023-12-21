namespace PgConvert.Element;

/// <summary>
/// Базовый класс для сущностей, относящихся к таблицам: триггеры и индексы
/// </summary>
public abstract class ElBaseForTable : DtElement
{
	protected ElBaseForTable(string[] lines) : base(lines) { }

	/// <summary>
	/// Имена таблиц, от которых зависит этот элемент
	/// </summary>
	private protected List<string> TableNames { get; } = new List<string>();

	/// <summary>
	/// Ссылки на таблицы, от которых зависит этот элемент
	/// </summary>
	protected ElTable[] Tables { get; set; }

	internal bool IsRelatedToTable(string name) =>
		TableNames.Contains(name);

	internal void SetTableName(string tableName) =>
		TableNames.Add(tableName);
}
