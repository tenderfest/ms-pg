namespace PgConvert.Element;

/// <summary>
/// Базовый класс для сущностей, относящихся к таблицам: триггеры и индексы
/// </summary>
public abstract class ElBaseForTable : DtElement
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="lines">Набор строк, определяющий изначальный MS SQL-скрипт для этого элемента</param>
	protected ElBaseForTable(string[] lines) : base(lines)
	{
	}

	#region публичные свойства

	/// <summary>
	/// Ссылки на таблицы, от которых зависит этот элемент
	/// </summary>
	internal protected ElTable[] Tables { get; set; }

	#endregion

	#region публичные методы

	/// <summary>
	/// Признак того, что элемент относится к указанной таблице
	/// </summary>
	/// <param name="name">Имя проверяемой таблицы</param>
	/// <returns>true, если этот элемент связан с таблицей, в противном случае false</returns>
	internal bool IsRelatedToTable(string name) =>
		TableNames.Contains(name);

	/// <summary>
	/// Установка связи элемента с таблицей
	/// </summary>
	/// <param name="tableName">Имя таблицы</param>
	internal void SetTableName(string tableName) =>
		TableNames.Add(tableName);

	#endregion

	#region приватные свойства

	/// <summary>
	/// Имена таблиц, от которых зависит этот элемент
	/// </summary>
	private protected List<string> TableNames { get; } = new List<string>();

	#endregion
}
