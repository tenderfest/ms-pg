﻿using System.Text;
using System.Text.Json.Serialization;
using PgConvert.Config;

namespace PgConvert.Element;

public class DtElement : BaseSelectable, IEquatable<DtElement>
{
	public DtElement()
	{
		_hashCode = 0;
	}

	public DtElement(string[] lines)
	{
		Lines = lines;
		// вычисление хэша для этого элемента
		_hashCode = lines.Crc32();
	}

	public string[] Lines { get; }
	public string[] CommentLines { get; private set; }
	public string DatabaseName { get; private protected set; }

	/// <summary>
	/// Признак того, что элемент проверен и, если необходимо, приведён в соответствие с требованиями T-SQL
	/// </summary>
	public bool IsOk { get; internal set; }

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
	internal static DtElement GetElement(List<string> inLines, List<string> comment, ConvertMsToPgCfg config)
	{
		var firstLineWords = inLines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
		var operation = firstLineWords[0].ToLower();
		var elementKey = firstLineWords.Length > 1 ? firstLineWords[1] : string.Empty;
		elementKey = elementKey.ToLower();

		if (null != config.SkipOperation && config.SkipOperation.Contains(operation))
			return default;
		if (null != config.SkipElement && config.SkipElement.Contains(elementKey))
			return default;

		var lines = inLines.ToArray();

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
		element.SetFields(operation, firstLineWords, comment.ToArray());

		return element;
	}

	private void SetFields(string operation, string[] firstLineWords, string[] comment)
	{
		Operation = ElementOperation.GetOperation(operation);
		FirstLineWords = firstLineWords;
		CommentLines = comment;
	}

	//public void SetFields(DtElement fromElement)
	//{
	//	Operation = fromElement.Operation;
	//	FirstLineWords = fromElement.FirstLineWords;
	//	CommentLines = fromElement.CommentLines;
	//}

	//public override bool Equals(object obj) =>
	//	obj is DtElement x && GetHashCode() == x.GetHashCode();

	[JsonIgnore]
	public string GetElementContent
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
	public virtual DtField[] GetFields =>
		Array.Empty<DtField>();

	private readonly int _hashCode;
	public override int GetHashCode() =>
		_hashCode;
	public int HashCode =>
		_hashCode;

	protected string IgnoreAsString =>
		Ignore ? "-" : string.Empty;

	public override string ToString() =>
		$"{IgnoreAsString}{ElementOperation.GetOperationSign(Operation)} {ElementType}: {Name}";

	internal virtual string Parse() =>
		null;

	public bool Equals(DtElement other) =>
		GetHashCode() == other?.GetHashCode();

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
