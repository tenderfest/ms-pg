﻿using Force.Crc32;
using System.Text;

namespace PgConvert;

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

	private static readonly string[] _indexSign = new string[]
	{
		CONSTRAINT,
		PRIMARY,
		UNIQUE,
	};
	internal static string[] IndexSign => _indexSign;

	public static string ToOneString(this string[] lines)
	{
		StringBuilder sb = new();
		if (null != lines)
			foreach (var str in lines)
				sb.AppendLine(str);
		return sb.ToString();
	}

	#region метод подсчёта контрольной суммы

	readonly static Crc32Algorithm crc32 = new();
	private static int Crc32(this string str)
	{
		var crcBytes = crc32.ComputeHash(Encoding.UTF8.GetBytes(str));
		return
			(crcBytes[0] << 24) |
			(crcBytes[1] << 16) |
			(crcBytes[2] << 8) |
			crcBytes[3];
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
