using PgConvert.Element;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace PgConvert.Config;
#pragma warning disable S2365 // Properties should not make collection or array copies
#pragma warning disable S4275 // Getters and setters should access the expected fields

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
	public List<NeedCorrect> NeedCorrect { get; set; }

	[JsonIgnore]
	public List<DtElement> FreeElements { get; set; }

	//private int[] _freeElementIds;
	public int[] FreeElementIds
	{
		get => FreeElements?.Select(e => e.Id).ToArray();
		//set => _freeElementIds = value;
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

	internal void AddNeedCorrect(DtElement element, List<DtElement> elementList)
	{
		NeedCorrect ??= new List<NeedCorrect>();
		var presentElement = NeedCorrect.Find(x => x.Equal(element.Id));
		if (default == presentElement)
			NeedCorrect.Add(new NeedCorrect(element));
		else
			presentElement.SetElement(element);
	}
}
