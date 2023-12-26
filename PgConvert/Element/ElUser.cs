using PgConvert.Enums;

namespace PgConvert.Element;

/// <summary>
/// Элемент: пользователь БД
/// </summary>
internal class ElUser : DtElement
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="lines">Набор строк, определяющий изначальный MS SQL-скрипт для этого элемента</param>
	public ElUser(string[] lines) : base(lines) =>
		ElementType = ElmType.User;
}
