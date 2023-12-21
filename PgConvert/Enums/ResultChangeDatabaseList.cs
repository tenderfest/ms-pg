namespace PgConvert.Enums;

/// <summary>
/// Результат измения списка баз данных
/// </summary>
public enum ResultChangeDatabaseList
{
    /// <summary>
    /// Список не изменился без ошибки
    /// </summary>
    None,

    /// <summary>
    /// Список изменился
    /// </summary>
    Ok,

    /// <summary>
    /// Ошибка, список не изменился
    /// </summary>
    Error,
}
