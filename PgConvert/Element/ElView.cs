using PgConvert.Enums;

namespace PgConvert.Element;

/// <summary>
/// Элемент: представление
/// </summary>
public class ElView : ElBaseForTable
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="lines">Набор строк, определяющий изначальный MS SQL-скрипт для этого элемента</param>
	public ElView(string[] lines) : base(lines) =>
		ElementType = ElmType.View;

	/// <inheritdoc/>
	public override string ToString() =>
		base.ToString() + $" ON ({string.Join(',', TableNames)})";

	/// <inheritdoc/>
	internal override string Parse() =>
		null;
}
