using PgConvert.Enums;

namespace PgConvert.Element;

/// <summary>
/// Элемент неизвестного типа
/// </summary>
public class DtUnknown : DtElement
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="lines">Набор строк, определяющий изначальный MS SQL-скрипт для этого элемента</param>
	public DtUnknown(string[] lines) : base(lines) =>
		ElementType = ElmType.None;
}
