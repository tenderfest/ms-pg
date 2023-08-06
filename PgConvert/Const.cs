namespace PgConvert;

internal static class Const
{
	internal const string FOREIGN = "foreign";
	internal const string KEY = "key";
	internal const string FOREIGN_KEY = FOREIGN + " " + KEY;
	internal const string CONSTRAINT = "constraint";
	internal const string REFERENCES = "references";
	internal const string ON = "on";
	internal const string UPDATE = "update";
	internal const string CASCADE = "cascade";
	internal const string DELETE = "delete";

	internal readonly static string[] _indexSign = new string[]
	{
		CONSTRAINT,
		"primary",
		"unique",
	};
}
