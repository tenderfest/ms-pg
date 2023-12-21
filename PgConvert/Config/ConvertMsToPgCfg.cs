using PgConvert.Element;
using PgConvert.Enums;
using System.Text;
using System.Text.Json.Serialization;

namespace PgConvert.Config;

/// <summary>
/// Настройки преобразования скрипта создания БД, сохраняемые в файле
/// </summary>
#pragma warning disable S2365 // Properties should not make collection or array copies
[Serializable]
public class ConvertMsToPgCfg
{
	/// <summary>
	/// "Встроенная" БД, куда пользователь должен отнести игнорируемые элементы из исходного скрипта
	/// </summary>
	public readonly OnePgDatabase IgnoreDatabase;

	/// <summary>
	/// Конструктор
	/// </summary>
	public ConvertMsToPgCfg()
	{
		if (null == Databases)
		{
			IgnoreDatabase = new OnePgDatabase(OnePgDatabase._thisDbIsIgnore)
			{
				ConnectionString = "У этой базы данных нет строки подключения"
			};
			Databases = new List<OnePgDatabase> { IgnoreDatabase };
		}
	}

	/// <summary>
	/// Список игнорируемых операций из скрипта MS-SQL
	/// </summary>
	public string[] SkipOperation { get; set; } = new string[3] { "use", "if", "set" };

	/// <summary>
	/// Список игнорируемых элементов
	/// </summary>
	public string[] SkipElement { get; set; } = new string[2] { "assembly", "database" };

	/// <summary>
	/// Набор целевых баз данных PostgreSQL (включая "встроенную")
	/// </summary>
	public List<OnePgDatabase> Databases { get; set; }

	/// <summary>
	/// Список элементов, которые нуждаются в ручной корректировке:
	/// триггеры, процедуры, вычисляемые поля таблиц
	/// </summary>
	public List<NeedCorrect> NeedCorrect { get; set; }

	/// <summary>
	/// Элементы исходного скрипта, ещё не отнесённые к какой-либо целевой базе данных
	/// </summary>
	[JsonIgnore]
	public List<DtElement> FreeElements { get; set; }

	/// <summary>
	/// Идентификаторы нераспределённых по БД элементов
	/// </summary>
	public int[] FreeElementIds =>
		FreeElements?.Select(e => e.Id).ToArray();

	/// <summary>
	/// Получение массива строк из текста
	/// </summary>
	/// <param name="text">Исходный текст, состоящий из строк, разделённых символом разделения строк</param>
	/// <returns>Массив строк, полученный из исходного текста</returns>
	public static string[] GetStringArrayFromText(string text) =>
		text?.Split('\n')
		.Select(s => s.ToLower().Trim())
		.Where(s => !string.IsNullOrEmpty(s))
		.ToArray();

	/// <summary>
	/// Добавление или удаление базы данных из набора целевых БД
	/// </summary>
	/// <param name="db">Добавляемая или удаляемая из списка БД</param>
	/// <param name="isAdd">true, если нужно добавить БД в список, иначе false</param>
	/// <returns>Результат добавление или удаления БД из списка целевых</returns>
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

	/// <summary>
	/// Набор игнорируемых элементов
	/// </summary>
	public string SkipElementAsText =>
		SkipElement.ToOneString();

	/// <summary>
	/// Набор игнорируемых операций исходного скрипта
	/// </summary>
	public string SkipOperationAsText =>
		SkipOperation.ToOneString();

	/// <summary>
	/// Добавление элемента исходного скрипта в список нераспределённых элементов
	/// </summary>
	/// <param name="element">Добавляемый элемент</param>
	internal void AddFreeElements(DtElement element)
	{
		if (!FreeElements.Contains(element))
			FreeElements.Add(element);
	}

	/// <summary>
	/// Добавление элемента исходного скрипта в список элементов, требующих ручной корректировки
	/// </summary>
	/// <param name="element">Добавляемый элемент</param>
	internal void AddNeedCorrect(DtElement element)
	{
		NeedCorrect ??= new List<NeedCorrect>();
		var presentElement = NeedCorrect.Find(x => x.Equal(element.Id));
		if (default == presentElement)
			NeedCorrect.Add(new NeedCorrect(element));
		else
			presentElement.SetElement(element);
	}
}
