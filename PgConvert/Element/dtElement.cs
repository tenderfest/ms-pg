using System.Text;
using System.Text.Json.Serialization;
using PgConvert.Config;

namespace PgConvert.Element;

public class DtElement : BaseSelectable
{
	public DtElement() { }

	internal bool Ignore { get; private protected set; }
	private protected string FirstLine { get; private set; }
	public string[] Lines { get; private set; }
	public string[] CommentLines { get; private set; }

	internal protected ElmOperation Operation { get; set; }
	private protected string[] FirstLineWords { get; set; }

	internal OnePgDatabase Database { get; set; }

	private protected string name;
	protected virtual string Name
	{
		get
		{
			if (null == name)
			{
				name = ClearBraces(FirstLineWords.Length < 3
					? "FirstLineWords.Length < 3"
					: FirstLineWords[2]);
			}
			return name;
		}
	}

	private protected static string ClearBraces(string draftName) =>
		draftName
			.Replace("(", string.Empty)
			.Replace("[", string.Empty)
			.Replace("]", string.Empty);

	/// <summary>
	/// Определение типа элемента и создание экземпляров элементов
	/// </summary>
	internal static DtElement GetElement(List<string> inLines, List<string> comment, ConvertMsToPgCfg config)
	{
		var firstLine = inLines.First();
		var firstLineWords = firstLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		var operation = firstLineWords[0].ToLower();
		var elementKey = firstLineWords.Length > 1 ? firstLineWords[1] : string.Empty;
		elementKey = elementKey.ToLower();

		if (null != config.SkipOperation && config.SkipOperation.Contains(operation))
			return default;
		if (null != config.SkipElement && config.SkipElement.Contains(elementKey))
			return default;

        DtElement element = Element.ElementType.GetType(elementKey, operation) switch
		{
            ElmType.Database => new ElDatabase(),
            ElmType.Index => new ElIndex(),
            ElmType.Procedure => new ElProcedure(),
            ElmType.Trigger => new ElTrigger(),
            ElmType.Table => new ElTable(),
            ElmType.View => new ElView(),
            ElmType.User => new ElUser(),
            ElmType.Role => new ElRole(),
            ElmType.Schema => new ElSchema(),
            ElmType.Exec => new ElExec(),
			_ => new DtUnknown(),
		};
		element.SetFields(operation, firstLine, firstLineWords, inLines.ToArray(), comment.ToArray());

		return element;
	}

	private void SetFields(string operation, string firstLine, string[] firstLineWords, string[] inLines, string[] comment)
	{
		Operation = ElementOperation.GetOperation(operation);
		FirstLine = firstLine;
		FirstLineWords = firstLineWords;
		Lines = inLines;
		CommentLines = comment;
	}

	public override bool Equals(object obj)
	{
		if (obj is not DtElement x)
			return false;

		if (FirstLine != x.FirstLine)
			return false;

		if (null == Lines && null != x.Lines ||
			null != Lines && null == x.Lines)
			return false;

		return GetHashCode() == x.GetHashCode();
	}

	[JsonIgnore]
	public string GetEmenenlContent
	{
		get
		{
			StringBuilder sb = new();
			if (null != CommentLines)
				foreach (var str in CommentLines)
					sb.AppendLine(str);

			if (null != Lines)
				foreach (var str in Lines)
					sb.AppendLine(str);

			return sb.ToString();
		}
	}

	[JsonIgnore]
	public virtual DtField[] GetFields => Array.Empty<DtField>();

	int? _hashCode;
	public override int GetHashCode()
	{
		if (!_hashCode.HasValue)
		{
			var hash = FirstLine.GetHashCode();
			if (Lines != null)
				foreach (var str in Lines)
					hash ^= str.GetHashCode();
			_hashCode = hash;
		}
		return _hashCode.Value;
	}

	public override string ToString()
		=> $"{(Ignore ? "-" : null)}{ElementOperation.GetOperationSign(Operation)} {ElementType}: {Name}";

	internal virtual string Parse() { return null; }

	private string linesAsString = null;
	protected string LinesAsString
	{
		get
		{
			if (null == linesAsString)
			{
				var stringBuilder = new StringBuilder();
				foreach (var str in Lines)
					stringBuilder.Append(str);
				linesAsString = stringBuilder.ToString();
			}
			return linesAsString;
		}
	}
}
