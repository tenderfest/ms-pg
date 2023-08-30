namespace PgConvert.Element;

/// <summary>
/// Тип элемента
/// </summary>
public enum ElmType
{
	None = 0,
	Database,
	User,
	Role,
	Schema,
	Table,
	Procedure,
	Trigger,
	Index,
	View,
	Exec,
}

/// <summary>
/// Методы работы с типами элементов
/// </summary>
internal static class ElementType
{
	/// <summary>
	/// Элементы, зависящие от таблиц
	/// </summary>
	public static readonly ElmType[] ElementTypesForTable = new ElmType[]
	{
		ElmType.Table,
		ElmType.Trigger,
		ElmType.Index,
		ElmType.View,
	};

	/// <summary>
	/// Получение типа этемента по его описанию
	/// </summary>
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
