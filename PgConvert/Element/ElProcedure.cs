namespace PgConvert.Element;

public class ElProcedure : DtElement
{
	public ElProcedure(string[] lines) : base(lines)
	{
		ElementType = ElmType.Procedure;
	}

	/// <summary>
	/// Текст процедуры в терминах PostgreSQL
	/// </summary>
	public string[] LinesPg { get; set; }

	internal override string Parse()
	{
		LinesPg ??= new string[Lines.Length];
		Lines.CopyTo(LinesPg, 0);
		return null;
	}

}
