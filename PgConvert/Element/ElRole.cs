using PgConvert.Enums;

namespace PgConvert.Element;

/// <summary>
/// Элемент: роль базы данных
/// </summary>
internal class ElRole : DtElement
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="lines">Набор строк, определяющий изначальный MS SQL-скрипт для этого элемента</param>
	public ElRole(string[] lines) : base(lines) =>
		ElementType = ElmType.Role;
}
