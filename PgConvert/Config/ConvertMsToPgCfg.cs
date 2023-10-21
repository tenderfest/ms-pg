using PgConvert.Element;
using System.Text;
using System.Text.Json.Serialization;

namespace PgConvert.Config;

[Serializable]
public class ConvertMsToPgCfg
{

	public readonly OnePgDatabase IgnoreDatabase;

	public ConvertMsToPgCfg()
	{
		if (null == Databases)
		{
			IgnoreDatabase = new OnePgDatabase(OnePgDatabase.ThisIgnore)
			{
				ConnectionString = "У этой базы данных нет строки подключения"
			};
			Databases = new List<OnePgDatabase> { IgnoreDatabase };
		}
	}


	public string[] SkipOperation { get; set; }
	public string[] SkipElement { get; set; }
	public List<OnePgDatabase> Databases { get; set; }
	public List<NeedCorrect> NeedCorrect { get; set; } = new List<NeedCorrect>();

	[JsonIgnore]
	public List<DtElement> FreeElements { get; set; }

	private int[] _freeElementIds;
	public int[] FreeElementIds
	{
#pragma warning disable S2365 // Properties should not make collection or array copies
#pragma warning disable S4275 // Getters and setters should access the expected fields
		get => FreeElements?.Select(e => e.Id).ToArray();
#pragma warning restore S4275 // Getters and setters should access the expected fields
#pragma warning restore S2365 // Properties should not make collection or array copies

		set => _freeElementIds = value;
	}

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

	public ResultChangeDatabaseList AddDelDatabase(OnePgDatabase db, bool isAdd)
	{
		if (null == db || db.IsDefault)
			return ResultChangeDatabaseList.None;

		if (isAdd)
		{
			if (Databases.Exists(ddd => ddd.Name == db.Name))
				return ResultChangeDatabaseList.Error;
			Databases.Add(db);
			return ResultChangeDatabaseList.Ok;
		}

		return Databases.Remove(db)
			? ResultChangeDatabaseList.Ok
			: ResultChangeDatabaseList.None;
	}

	public string GetSkipElementAsText() =>
		GetStringArrayAsText(SkipElement);

	public string GetSkipOperationAsText() =>
		GetStringArrayAsText(SkipOperation);

	internal void AddFreeElements(DtElement element)
	{
		if (!FreeElements.Contains(element))
			FreeElements.Add(element);
	}

	internal void AddNeedCorrect(DtElement element)
	{
		if (NeedCorrect.Find(x => x.Id == element.Id) == default)
			NeedCorrect.Add(new NeedCorrect(element));
	}
}
