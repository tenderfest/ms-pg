using PgConvert.Element;

namespace PgConvert.Config;
#pragma warning disable S4275 // Getters and setters should access the expected fields

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
				((ElProcedure)Element).LinesPg = _savedLines;
				break;
			case ElmType.Trigger:
				((ElTrigger)Element).LinesPg = _savedLines;
				break;
			case ElmType.Table:
				((ElTable)Element).GeneratedFields = _savedLines;
				break;
		};
	}

	public int Id
	{
		get => Element.Id;
		set => _id = value;
	}

	private string[] _savedLines;
	private int _id;

	public string[] Lines
	{
		get => Element.ElementType switch
		{
			ElmType.Procedure => ((ElProcedure)Element).LinesPg,
			ElmType.Trigger => ((ElTrigger)Element).LinesPg,
			ElmType.Table => GeneratedFields,
			_ => Array.Empty<string>()
		};
		set
		{
			_savedLines = value;
		}
	}

	private string[] GeneratedFields =>
		Element is ElTable table
		? table.GeneratedFields
		: Array.Empty<string>();

	internal bool Equal(int id) =>
		id == _id;

	//public bool IsNeedCorrect =>
	//	element.IsNeedCorrect;
	//public ElmType ElementType =>
	//	element.ElementType;
}
