namespace PgConvert
{
	/// <summary>
	/// тип элемента
	/// </summary>
	public enum ElmType
	{
		None = 0,
		Database,
		Table,
		Procedure,
		Trigger,
		Index,
		View,
	}

	internal static class ElementType
	{
		internal static ElmType GetType(string elementKey) =>
			elementKey.ToLower() switch
			{
				"database" => ElmType.Database,
				"table" => ElmType.Table,
				"procedure" => ElmType.Procedure,
				"trigger" => ElmType.Trigger,
				"view" => ElmType.View,

				"index" => ElmType.Index,
				"unique" => ElmType.Index,
				"nonclustered" => ElmType.Index,
				_ => ElmType.None,
			};
	}
}