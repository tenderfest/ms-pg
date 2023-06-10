namespace PgConvert
{
	public class ElIndex : ElBaseForTable
	{
		public ElIndex()
		{
			Type = ElmType.Index;
		}

		protected override string Name
		{
			get
			{
				if (null == name)
				{
					SetTableName(ClearBraces(FirstLineWords[FirstLineWords.Length - 1]));
					name = ClearBraces(FirstLineWords[FirstLineWords.Length - 1 - 2]);
				}
				return name;
			}
		}


	}
}
