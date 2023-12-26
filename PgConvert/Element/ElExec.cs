using PgConvert.Enums;

namespace PgConvert.Element;

/// <summary>
/// Элемент: инстукция исполнения в скрипте MS SQL
/// </summary>
internal class ElExec : DtElement
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="lines">Набор строк, определяющий изначальный MS SQL-скрипт для этого элемента</param>
	public ElExec(string[] lines) : base(lines) =>
		ElementType = ElmType.Exec;

	/// <inheritdoc/>
	internal override string Parse() =>
		null;
}
