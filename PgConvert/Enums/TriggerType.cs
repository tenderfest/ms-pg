namespace PgConvert.Enums;

/// <summary>
/// Тип триггера
/// </summary>
[Flags]
public enum TriggerType
{
    None = 0,
    Insert = 1,
    Update = 1 << 1,
    Delete = 1 << 2,
}
