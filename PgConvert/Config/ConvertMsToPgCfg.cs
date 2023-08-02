using PgConvert.Element;
using System.Text;

namespace PgConvert.Config;

[Serializable]
public class ConvertMsToPgCfg
{
	public ConvertMsToPgCfg()
	{
		if (null == Databases)
		{
			Databases = new OnePgDatabase[]
			{
				new OnePgDatabase (OnePgDatabase.ThisIgnore)
			};
		}
	}
	public OnePgDatabase[] Databases { get; set; }
	public DtElement[] FreeElements { get; set; }
	public string[] SkipOperation { get; set; }
	public string[] SkipElement { get; set; }

	public static string[] GetSkipArrayFromText(string text) =>
		text?.Split('\n')
		.Select(s =>
			s.ToLower().Trim())
		.Where(s =>
			!string.IsNullOrEmpty(s))
		.ToArray();

	private static string GetStringArrayAsText(string[] stringArray)
	{
		var sb = new StringBuilder();
		stringArray?.ToList().ForEach(s =>
			sb.AppendLine(s));
		return sb.ToString();
	}

	public void AddDelDatabase(OnePgDatabase db, bool isAdd)
	{
		if (null == db || db.IsDefault)
			return;

		var databases = Databases.ToList();
		if (isAdd)
			databases.Add(db);
		else
			databases.Remove(db);
		Databases = databases.ToArray();
	}

	public string GetSkipElementAsText() =>
		GetStringArrayAsText(SkipElement);

	public string GetSkipOperationAsText() =>
		GetStringArrayAsText(SkipOperation);
}
