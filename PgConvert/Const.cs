using Force.Crc32;
using System.Text;

namespace PgConvert;

internal static class Const
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

	private static string[] _indexSign = new string[]
	{
		CONSTRAINT,
		PRIMARY,
		UNIQUE,
	};
	internal static string[] IndexSign => _indexSign;

	#region метод подсчёта контрольной суммы

	readonly static Crc32Algorithm crc32 = new();
	internal static int Crc32(this string str)
	{
		var crcBytes = crc32.ComputeHash(Encoding.UTF8.GetBytes(str));
		int result = crcBytes[0] << 24;
		result += crcBytes[1] << 16;
		result += crcBytes[2] << 8;
		result += crcBytes[3];
		return result;
	}
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
