using PgConvert.Element;
using PgConvert.Enums;

namespace PgConvert.Config;

/// <summary>
/// Элемент, требующий доработки
/// </summary>
public class NeedCorrect
{
	private const string _begin = "begin";

	/// <summary>
	/// Конструктор
	/// </summary>
	public NeedCorrect() { }

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="element">Элемент скрипта, требующий доработки</param>
	public NeedCorrect(DtElement element) =>
			Element = element;

	private string[] SavedLines { get; set; }
	private int IdTemp { get; set; }
	private bool IsOkTemp { get; set; }
	private string FunctionNameTemp { get; set; }
	private string PLanguageTemp { get; set; }
	private DtElement Element { get; set; }

	/// <summary>
	/// Установка элемента скрипта БД
	/// </summary>
	/// <param name="element">Элемент скрипта, требующий доработки</param>
	internal void SetElement(DtElement element)
	{
		if (null == element)
			return;

		Element = element;
		switch (Element.ElementType)
		{
			case ElmType.Procedure:
				var procedure = (ElProcedure)Element;
				procedure.LinesPg = SavedLines;
				procedure.SetOk(IsOkTemp);
				break;

			case ElmType.Trigger:
				var trigger = (ElTrigger)Element;
				trigger.LinesPg = SavedLines;
				trigger.TriggerFunctionName = FunctionNameTemp;
				trigger.SetOk(IsOkTemp);
				trigger.PLanguage = Plang.GetByName(PLanguageTemp);
				if (trigger.PLanguage == Plang.OwnVariant)
				{
					trigger.OwnVariantLanguage = PLanguageTemp;
				}
				break;

			case ElmType.Table:
				((ElTable)Element).GeneratedFields = SavedLines;
				break;
		}
	}

	/// <summary>
	/// Утверждён ли доработанный элемент для включения его в итоговый скрипт?
	/// </summary>
	public bool IsOk
	{
		get =>
			((IEdited)Element).IsOk;

		set =>
			IsOkTemp = value;
	}

	/// <summary>
	/// Идентификатор элемента скрипта БД
	/// </summary>
	public int Id
	{
		get =>
			Element.Id;

		set =>
			IdTemp = value;
	}

	/// <summary>
	/// Наименование хранимой процедуры или триггерной функции
	/// </summary>
	public string FunctionName
	{
		get =>
			Element?.ElementType switch
			{
				ElmType.Procedure => ((ElProcedure)Element).Name,
				ElmType.Trigger => ((ElTrigger)Element).TriggerFunctionName,
				ElmType.Table => null,
				_ => null,
			};

		set =>
			FunctionNameTemp = value;
	}

	/// <summary>
	/// Название процедурного языка для триггерной функции
	/// </summary>
	public string PlangName
	{
		get =>
			Element?.ElementType == ElmType.Trigger
			? ((ElTrigger)Element).PlangName
			: null;

		set =>
			PLanguageTemp = value;
	}

	/// <summary>
	/// Тело хранимой процедуры или триггерной функции или
	/// набор переметров вычисляемых полей таблицы
	/// </summary>
	public string[] Lines
	{
		get =>
			Element.ElementType switch
			{
				ElmType.Procedure => ((ElProcedure)Element).LinesPg,
				ElmType.Trigger => ((ElTrigger)Element).LinesPg,
				ElmType.Table => GeneratedFields,
				_ => Array.Empty<string>(),
			};

		set =>
			SavedLines = value;
	}

	/// <summary>
	/// Набор вычисляемых полей таблицы
	/// </summary>
	private string[] GeneratedFields =>
		Element is ElTable table
		? table.GeneratedFields
		: Array.Empty<string>();

	/// <summary>
	/// Метод сравнения элементов
	/// </summary>
	/// <param name="id">Идентификатор какого-либо элемента скрипта</param>
	/// <returns>true, если проверяемый идентификатор соответствует элементу, который представляет
	/// этот экземпляр класса NeedCorrect, иначе false</returns>
	internal bool Equal(int id) =>
		id == IdTemp;

	/// <summary>
	/// Наполнение LinesPg изначальным значением из Lines
	/// </summary>
	public static void LinesPgFromLines(DtElement element)
	{
		if (null == element)
			return;

		var linesPg = new List<string>();
		bool isBegin = false;
		foreach (var line in element.Lines)
		{
			var lineTrim = line.Trim();
			if (lineTrim.StartsWith("--"))
			{
				linesPg.Add(line);
				continue;
			}
			if (!isBegin)
				isBegin |= lineTrim.ToLower() == _begin;
			if (!isBegin)
				continue;
			linesPg.Add(line);
		}

		var linesArray = linesPg.ToArray();
		switch (element.ElementType)
		{
			case ElmType.Procedure:
				((ElProcedure)element).LinesPg = linesArray;
				break;
			case ElmType.Trigger:
				((ElTrigger)element).LinesPg = linesArray;
				break;
		}
	}
}
