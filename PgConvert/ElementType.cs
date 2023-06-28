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
		User,
		Role,
		Schema,
		Exec,
	}

	internal static class ElementType
	{
		internal static ElmType GetType(string elementKey, string operation) =>
			("exec" == operation)
			? ElmType.Exec
			: elementKey.ToLower() switch
			{
				"database" => ElmType.Database,
				"table" => ElmType.Table,
				"procedure" => ElmType.Procedure,
				"trigger" => ElmType.Trigger,
				"view" => ElmType.View,
				"user" => ElmType.User,
				"role" => ElmType.Role,
				"schema" => ElmType.Schema,

				"index" => ElmType.Index,
				"unique" => ElmType.Index,
				"nonclustered" => ElmType.Index,
				_ => ElmType.None,
			};
	}
}