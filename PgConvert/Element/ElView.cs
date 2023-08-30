namespace PgConvert.Element;

public class ElView : ElBaseForTable
{
	public ElView(string[] lines) : base(lines)
	{
		ElementType = ElmType.View;
	}

	public override string ToString() =>
		base.ToString() + $" ON ({string.Join(',', TableNames)})";

	internal override string Parse()
	{

		return null;
	}
}
