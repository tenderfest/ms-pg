using PgConvert.Config;

namespace PgConvert.Element;

public class ElProcedure : DtElement, IEdited
{
	public ElProcedure(string[] lines) : base(lines)
	{
		ElementType = ElmType.Procedure;
	}

	/// <summary>
	/// Текст процедуры в терминах PostgreSQL
	/// </summary>
	public string[] LinesPg { get; set; }

	private bool isOk;
	public bool IsOk => isOk;

	OnePgDatabase IEdited.Database => Database;

	public void SetOk(bool ok) =>
		isOk = ok;

	internal override string Parse()
	{
		LinesPg ??= new string[Lines.Length];
		Lines.CopyTo(LinesPg, 0);
		return null;
	}
}
