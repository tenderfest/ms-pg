using PgConvert.Enums;

namespace PgConvert.Element;

/// <summary>
/// Элемент: схема базы данных
/// </summary>
internal class ElSchema : DtElement
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="lines">Набор строк, определяющий изначальный MS SQL-скрипт для этого элемента</param>
	public ElSchema(string[] lines) : base(lines) =>
		ElementType = ElmType.Schema;

	/// <inheritdoc/>
	internal override string Parse() =>
		null;
}
