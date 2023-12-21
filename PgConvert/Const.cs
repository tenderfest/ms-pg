using Force.Crc32;
using System.Text;

namespace PgConvert;

/// <summary>
/// Вспомогательные константы и методы
/// </summary>
public static class Const
{
	internal const string FOREIGN = "foreign";
	internal const string KEY = "key";
	internal const string FOREIGN_KEY = FOREIGN + " " + KEY;
	internal const string CONSTRAINT = "constraint";
	internal const string PRIMARY = "primary";
	internal const string UNIQUE = "unique";
	internal const string REFERENCES = "references";
	internal const string ON = "on";
	internal const string UPDATE = "update";
	internal const string CASCADE = "cascade";
	internal const string DELETE = "delete";

	/// <summary>
	/// Ключевые слова, определяющие в исходном SQL-скрипте индексы
	/// </summary>
	private static readonly string[] _indexSign = new string[]
	{
		CONSTRAINT,
		PRIMARY,
		UNIQUE,
	};

	/// <summary>
	/// Ключевые слова, определяющие в исходном SQL-скрипте индексы
	/// </summary>
	internal static string[] IndexSign => _indexSign;

	/// <summary>
	/// Получение текста из массива строк
	/// </summary>
	/// <param name="lines">Исходный массив строк</param>
	/// <returns>Текст, в котором строки разделены символом разделения строк</returns>
	public static string ToOneString(this string[] lines)
	{
		StringBuilder sb = new();
		if (null != lines)
		{
			foreach (var str in lines)
			{
				sb.AppendLine(str);
			}
		}
		return sb.ToString();
	}

	/// <summary>
	/// Получение массива трок из строки с разделителями строк
	/// </summary>
	/// <param name="str">Исходная строка</param>
	/// <returns>Полученный массив строк</returns>
	public static string[] FromOneString(this string str) =>
		str.Split(Environment.NewLine);

	#region метод подсчёта контрольной суммы

	readonly static Crc32Algorithm crc32 = new();

	/// <summary>
	/// Подсчёт контрольной суммы для строки
	/// </summary>
	/// <param name="str">Строка</param>
	/// <returns>Контрольная сумма</returns>
	private static int Crc32(this string str)
	{
		var crcBytes = crc32.ComputeHash(Encoding.UTF8.GetBytes(str));
		return
			(crcBytes[0] << 24) |
			(crcBytes[1] << 16) |
			(crcBytes[2] << 8) |
			crcBytes[3];
	}

	/// <summary>
	/// Подсчёт контрольной суммы для массива строк
	/// </summary>
	/// <param name="lines">Массив строк</param>
	/// <returns>Контрольная сумма</returns>
	internal static int Crc32(this string[] lines)
	{
		if (null == lines || !lines.Any())
			return 0;

		var crc = lines[0].Crc32();
		foreach (var line in lines.Skip(1))
			crc ^= line.Crc32();

		return crc;
	}

	#endregion
}
