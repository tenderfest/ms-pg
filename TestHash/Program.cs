using Force.Crc32;
using System.Text;

internal static class Ext
{
	internal static int Crc(this string str)
	{
		Crc32Algorithm crc32 = new();
		var crcBytes = crc32.ComputeHash(Encoding.UTF8.GetBytes(str));
		int result = crcBytes[0] << 24;
		result += crcBytes[1] << 16;
		result += crcBytes[2] << 8;
		result += crcBytes[3];
		return result;
	}
}

internal class Program
{
	private static void Main(string[] args)
	{

		Console.WriteLine($"1aa: {"aaa: ".Crc()}");
		Console.WriteLine($"2aa: {"aba: ".Crc()}");
		Console.WriteLine($"3aa: {"aaa: ".Crc()}");
		Console.WriteLine($"4aa: {"aaa: ".Crc()}");
	}
}
