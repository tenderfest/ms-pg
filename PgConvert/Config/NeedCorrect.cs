using PgConvert.Element;

namespace PgConvert.Config;

/// <summary>
/// Элемент, требующий доработки
/// </summary>
public class NeedCorrect
{
	private const string _begin = "begin";
	private string[] SavedLines { get; set; }
	private int IdTemp { get; set; }
	private string FunctionNameTemp { get; set; }
	private string PLanguageTemp { get; set; }
	private DtElement Element { get; set; }

	public NeedCorrect() { }
	public NeedCorrect(DtElement element) =>
			Element = element;

	internal void SetElement(DtElement element)
	{
		Element = element;
		switch (Element.ElementType)
		{
			case ElmType.Procedure:
				((ElProcedure)Element).LinesPg = SavedLines;
				break;

			case ElmType.Trigger:
				var trigger = (ElTrigger)Element;
				trigger.LinesPg = SavedLines;
				trigger.TriggerFunctionName = FunctionNameTemp;
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

	public int Id
	{
		get =>
			Element.Id;

		set =>
			IdTemp = value;
	}

	public string FunctionName
	{
		get =>
			Element.ElementType switch
			{
				ElmType.Procedure => ((ElProcedure)Element).Name,
				ElmType.Trigger => ((ElTrigger)Element).TriggerFunctionName,
				ElmType.Table => null,
				_ => null,
			};

		set =>
			FunctionNameTemp = value;
	}

	public string PlangName
	{
		get =>
			Element.ElementType == ElmType.Trigger
			? ((ElTrigger)Element).PlangName
			: null;

		set =>
			PLanguageTemp = value;
	}

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

	private string[] GeneratedFields =>
		Element is ElTable table
		? table.GeneratedFields
		: Array.Empty<string>();

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
