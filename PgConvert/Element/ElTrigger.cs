namespace PgConvert.Element;

public class ElTrigger : ElBaseForTable
{
	public ElTrigger(string[] lines) : base(lines)
	{
		ElementType = ElmType.Trigger;
	}

	internal protected override string Name
	{
		get
		{
			if (null == name)
			{
				SetTableName(ClearBraces(Lines.Length < 2
					? "Lines.Length < 2"
					: Lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]));
				name = ClearBraces(FirstLineWords[2]);
			}
			return name;
		}
	}

	//internal override string Parse()
	//{

	//	return null;
	//}
}
