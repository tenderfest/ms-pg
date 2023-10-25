using PgConvert.Element;

namespace PgConvert.Config;

/// <summary>
/// Элемент, требующий доработки
/// </summary>
public class NeedCorrect
{
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
				((ElTrigger)Element).LinesPg = SavedLines;
				break;
			case ElmType.Table:
				((ElTable)Element).GeneratedFields = SavedLines;
				break;
		};
	}

	public int Id
	{
		get => Element.Id;
		set => IdTemp = value;
	}

	private string[] SavedLines { get; set; }
	private int IdTemp { get; set; }

	public string[] Lines
	{
		get =>
			Element.ElementType switch
			{
				ElmType.Procedure => ((ElProcedure)Element).LinesPg,
				ElmType.Trigger => ((ElTrigger)Element).LinesPg,
				ElmType.Table => GeneratedFields,
				_ => Array.Empty<string>()
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

	//public bool IsNeedCorrect =>
	//	element.IsNeedCorrect;
	//public ElmType ElementType =>
	//	element.ElementType;
}
