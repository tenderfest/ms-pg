namespace PgConvert.Enums;

/// <summary>
/// Операция, выполняемая с элементом в SQL-скрипте
/// </summary>
public enum ElmOperation
{
	None = 0,
	Create,
	Alter,
	Exec,
	Set,
	Use,
}
