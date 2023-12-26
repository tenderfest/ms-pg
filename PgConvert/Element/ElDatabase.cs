using PgConvert.Enums;

namespace PgConvert.Element;

/// <summary>
/// Элемент: база данных
/// </summary>
public class ElDatabase : DtElement
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="lines">Набор строк, определяющий изначальный MS SQL-скрипт для этого элемента</param>
	public ElDatabase(string[] lines) : base(lines) =>
		ElementType = ElmType.Database;

	/// <inheritdoc/>
	internal override string Parse()
	{
		Ignore = ElmOperation.Alter == Operation;
		return null;
	}
}
