using System.Text;
using System.Text.Json.Serialization;
using PgConvert.Config;

namespace PgConvert.Element;

#pragma warning disable S2365 // Properties should not make collection or array copies
public abstract class DtElement
{
	protected DtElement() =>
		_hashCode = 0;

	protected DtElement(string[] lines)
	{
		Lines = lines;
		// вычисление хэша для этого элемента
		_hashCode = lines.Crc32();
	}

	public ElmType ElementType { get; private protected set; }
	public string DatabaseName { get; private protected set; }
	public string[] Lines { get; }
	public string[] ClearLines =>
		Lines
		.Where(x => !x.Trim().StartsWith("--"))
		.ToArray();

	/// <summary>
	/// Устанавливаемый человеком признак необходимости корректировки элемента
	/// </summary>
	public bool IsNeedCorrect { get; set; } = true;

	internal bool Ignore { get; private protected set; }
	internal protected ElmOperation Operation { get; set; }
	internal OnePgDatabase Database { get; set; }

	private protected string[] FirstLineWords { get; set; }

	private protected string name;
	internal protected virtual string Name
	{
		get
		{
			if (null == name)
			{
				if (null == FirstLineWords || !FirstLineWords.Any())
					return null;

				name = ClearBraces(FirstLineWords.Length < 3
					? "FirstLineWords.Length < 3"
					: FirstLineWords[2]);
			}
			return name;
		}
	}

	public static string ClearBraces(string draftName) =>
		draftName
			.Replace("(", string.Empty)
			.Replace("[", "\"")
			.Replace("]", "\"");

	/// <summary>
	/// Определение типа элемента и создание экземпляров элементов
	/// </summary>
	internal static DtElement GetElement(string[] lines, ConvertMsToPgCfg config)
	{
		var firstNotCommentLine = Array.Find(lines, x => !x.StartsWith("--"));
		if (firstNotCommentLine == null)
			return new DtUnknown(lines);
		var firstLineWords = firstNotCommentLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		var operation = firstLineWords[0].ToLower();
		var elementKey = firstLineWords.Length > 1 ? firstLineWords[1] : string.Empty;
		elementKey = elementKey.ToLower();

		if (null != config.SkipOperation && config.SkipOperation.Contains(operation))
			return default;
		if (null != config.SkipElement && config.SkipElement.Contains(elementKey))
			return default;

		DtElement element = Element.ElementType.GetType(elementKey, operation) switch
		{
			ElmType.Database => new ElDatabase(lines),
			ElmType.User => new ElUser(lines),
			ElmType.Role => new ElRole(lines),
			ElmType.Schema => new ElSchema(lines),
			ElmType.Table => new ElTable(lines),
			ElmType.Procedure => new ElProcedure(lines),
			ElmType.Trigger => new ElTrigger(lines),
			ElmType.Index => new ElIndex(lines, null),
			ElmType.View => new ElView(lines),
			ElmType.Exec => new ElExec(lines),
			_ => new DtUnknown(lines),
		};
		element.SetFields(operation, firstLineWords);

		return element;
	}

	private void SetFields(string operation, string[] firstLineWords)
	{
		Operation = ElementOperation.GetOperation(operation);
		FirstLineWords = firstLineWords;
	}

	//public void SetFields(DtElement fromElement)
	//{
	//	Operation = fromElement.Operation;
	//	FirstLineWords = fromElement.FirstLineWords;
	//	CommentLines = fromElement.CommentLines;
	//}

	[JsonIgnore]
	public string GetElementContent
	{
		get
		{
			StringBuilder sb = new();
			if (null != Lines)
				foreach (var str in Lines)
					sb.AppendLine(str);
			return sb.ToString();
		}
	}

	[JsonIgnore]
	public virtual DtField[] GetFields =>
		Array.Empty<DtField>();

	public override string ToString() =>
		$"{IgnoreAsString}{ElementOperation.GetOperationSign(Operation)} {ElementType}: {Name}";

	/// <summary>
	/// Разбор элемента
	/// </summary>
	/// <returns>Сообщение об ошибке, или null в случае отсутствия ошибок</returns>
	internal virtual string Parse() =>
		null;

	protected string IgnoreAsString =>
		Ignore ? "-" : string.Empty;

	private readonly int _hashCode;
	public int Id =>
		_hashCode;
	public bool Equals(DtElement dtElement) =>
		dtElement != null && Id == dtElement.Id;

	private string linesAsString = null;
	protected string LinesAsString
	{
		get
		{
			if (null == linesAsString)
			{
				var stringBuilder = new StringBuilder();
				foreach (var str in Lines)
				{
					stringBuilder.Append(str.Trim());
					stringBuilder.Append(' ');
				}
				linesAsString = stringBuilder.ToString().Trim();
			}
			return linesAsString;
		}
	}

	private string linesAsStringLower = null;
	protected string LinesAsStringLower =>
		linesAsStringLower ??= LinesAsString.ToLower();
}
