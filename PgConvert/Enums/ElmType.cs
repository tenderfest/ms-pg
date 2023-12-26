namespace PgConvert.Enums;

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
