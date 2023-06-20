namespace PgConvert
{
	public class ElTrigger : ElBaseForTable
	{
		public ElTrigger()
		{
			SelectFor = ElmType.Trigger;
		}

		protected override string Name
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
	}
}
