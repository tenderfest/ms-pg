namespace PgConvert;

/// <summary>
/// Определение последовательности
/// </summary>
internal class DtSequence
{
	/// <summary>
	/// Название
	/// </summary>
	string Name { get; set; }

	/// <summary>
	/// Минимальное значение последовательности
	/// </summary>
	long? Minvalue { get; set; }

	/// <summary>
	/// Максимальное значение последовательности
	/// </summary>
	long? Maxvalue { get; set; }

	/// <summary>
	/// Начальное значение последовательности
	/// </summary>
	long? Start { get; set; }

	/// <summary>
	/// 
	/// </summary>
	long? IncrementBy { get; set; }

	/// <summary>
	/// Получение SQL-скрипта для удаления последовательности
	/// </summary>
	/// <returns>$"DROP SEQUENCE IF EXISTS {Name};"</returns>
	string GetDrop() => 
		$"DROP SEQUENCE IF EXISTS {Name};";
}
