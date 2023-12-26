namespace PgConvert.Enums;

/// <summary>
/// Условие выполнение триггера
/// </summary>
public enum TriggerDoing
{
    None = 0,
    After,
    /// <summary>
    /// Триггеры INSTEAD OF могут определяться только для представлений и только на уровне строк
    /// </summary>
    InsteadOf,
}
