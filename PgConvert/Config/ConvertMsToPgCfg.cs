using PgConvert.Element;
using System.Text;

namespace PgConvert.Config;

[Serializable]
public class ConvertMsToPgCfg
{
	/// <summary>
	/// Результат измения списка баз данных
	/// </summary>
	public enum ResultChangeDatabaseList
	{
		/// <summary>
		/// Список не изменился без ошибки
		/// </summary>
		None,
		/// <summary>
		/// Список изменился
		/// </summary>
		Ok,
		/// <summary>
		/// Ошибка, список не изменился
		/// </summary>
		Error,
	}

	public ConvertMsToPgCfg()
	{
		if (null == Databases)
		{
			Databases = new List<OnePgDatabase>
			{
				new OnePgDatabase (OnePgDatabase.ThisIgnore)
			};
		}
	}
	public List<OnePgDatabase> Databases { get; set; }
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
}
