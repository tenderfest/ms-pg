namespace PgConvert.Element;

public class ElTrigger : ElBaseForTable
{
	public ElTrigger(string[] lines) : base(lines)
	{
		ElementType = ElmType.Trigger;
	}

	/// <summary>
	/// Текст триггера в терминах PostgreSQL
	/// </summary>
	public string[] LinesPg { get; set; }

	public override string ToString() =>
		base.ToString() + $" ON ({string.Join(',', TableNames)})";

	internal protected override string Name =>
		name;

	internal override string Parse()
	{
		LinesPg ??= new string[Lines.Length];
		Lines.CopyTo(LinesPg, 0);

		SetTableName(ClearBraces(Lines.Length < 2
			? "Lines.Length < 2"
			: Lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]));
		name = ClearBraces(FirstLineWords[2]);

		return null;
	}
}
