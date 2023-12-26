using PgConvert.Config;
using PgConvert.Enums;

namespace PgConvert.Element;

/// <summary>
/// Элемент: хранимая процедура
/// </summary>
public class ElProcedure : DtElement, IEdited
{
	/// <summary>
	/// Признак того, что элемент проверен
	/// </summary>
	private bool isOk;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="lines">Набор строк, определяющий изначальный MS SQL-скрипт для этого элемента</param>
	public ElProcedure(string[] lines) : base(lines) =>
		ElementType = ElmType.Procedure;

	#region реализация интерфейса IEdited

	/// <inheritdoc/>
	public bool CanSetOk =>
		true;

	/// <inheritdoc/>
	public void SetOk(bool ok) =>
		isOk = ok;

	/// <inheritdoc/>
	public bool IsOk =>
		isOk;

	#endregion

	/// <summary>
	/// Текст процедуры в терминах PostgreSQL
	/// </summary>
	public string[] LinesPg { get; set; }

	/// <inheritdoc/>
	internal override string Parse()
	{
		// определение тела процедуры
		if (null == LinesPg)
			NeedCorrect.LinesPgFromLines(this);

		return null;
	}
}
