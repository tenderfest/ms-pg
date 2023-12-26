using System.Text;
using PgConvert.Config;
using PgConvert.Enums;

namespace PgConvert.Element;

/// <summary>
/// Базовый класс для всех элементов SQL-скрипта
/// </summary>
#pragma warning disable S2365 // Properties should not make collection or array copies
public abstract class DtElement
{
	#region константы и поля

	private readonly int _hashCode;
	private protected string _name;
	private string _linesAsString = null;
	private string _linesAsStringLower = null;

	#endregion

	/// <summary>
	/// Конструктор без параметров
	/// </summary>
	protected DtElement() =>
		_hashCode = 0;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="lines">Набор строк, определяющий изначальный MS SQL-скрипт для этого элемента</param>
	protected DtElement(string[] lines)
	{
		Lines = lines;
		// вычисление хэша для этого элемента
		_hashCode = lines.Crc32();
	}

	#region публичные свойства

	/// <summary>
	/// Тип элемента
	/// </summary>
	public ElmType ElementType { get; private protected set; }

	/// <summary>
	/// Набор строк, определяющий изначальный MS SQL-скрипт для этого элемента
	/// </summary>
	public string[] Lines { get; }

	/// <summary>
	/// Устанавливаемый человеком признак необходимости корректировки элемента
	/// </summary>
	public bool IsNeedCorrect { get; set; } = true;

	/// <summary>
	/// Признак того, что этот элемент ну нужно учитывать
	/// </summary>
	internal bool Ignore { get; private protected set; }

	/// <summary>
	/// База данных, к которой отнесён этот элемент
	/// </summary>
	public OnePgDatabase Database { get; set; }

	/// <summary>
	/// Операция, которая производится с этим элементом в SQL-скрипте
	/// </summary>
	internal protected ElmOperation Operation { get; set; }

	/// <summary>
	/// Набор строк без комментариев и конечных/начальных пробелов,
	/// определяющий изначальный MS SQL-скрипт для этого элемента
	/// </summary>
	public string[] ClearLines =>
		Lines
		.Where(x => !x.Trim().StartsWith("--"))
		.ToArray();

	/// <summary>
	/// Строковое представление признака игнорируемого элемента
	/// </summary>
	protected string IgnoreAsString =>
		Ignore ? "-" : string.Empty;

	/// <summary>
	/// Идентификатор эдемента
	/// </summary>
	public int Id =>
		_hashCode;

	/// <summary>
	/// Строки исходного MS SQL-скрипта, собранные в одну строку, разделённые пробелом
	/// </summary>
	protected string LinesAsString
	{
		get
		{
			if (null == _linesAsString)
			{
				var stringBuilder = new StringBuilder();
				foreach (var str in Lines)
				{
					stringBuilder.Append(str.Trim());
					stringBuilder.Append(' ');
				}
				_linesAsString = stringBuilder.ToString().Trim();
			}
			return _linesAsString;
		}
	}

	/// <summary>
	/// Строки исходного MS SQL-скрипта в нижнем регистре, собранные в одну строку, разделённые пробелом
	/// </summary>
	protected string LinesAsStringLower =>
		_linesAsStringLower ??= LinesAsString.ToLower();

	/// <summary>
	/// Имя этого элемента
	/// </summary>
	public virtual string Name
	{
		get
		{
			if (null == _name)
			{
				if (null == FirstLineWords || !FirstLineWords.Any())
					return null;

				_name = ClearBraces(FirstLineWords.Length < 3
					? "FirstLineWords.Length < 3"
					: FirstLineWords[2]);
			}
			return _name;
		}
	}

	/*
	//[JsonIgnore]
	//public string GetElementContent
	//{
	//	get
	//	{
	//		StringBuilder sb = new();
	//		if (null != Lines)
	//			foreach (var str in Lines)
	//				sb.AppendLine(str);
	//		return sb.ToString();
	//	}
	//}

	//[JsonIgnore]
	//public virtual DtField[] GetFields =>
	//	Array.Empty<DtField>();
	*/

	#endregion

	#region публичные методы

	/// <summary>
	/// Очистка строки от открывающей круглой скобки и замена квадратных скобок двойной кавычкой
	/// </summary>
	/// <param name="inString">Исходная строка</param>
	/// <returns>Результирующая строка</returns>
	public static string ClearBraces(string inString) =>
		inString
			.Replace("(", string.Empty)
			.Replace("[", "\"")
			.Replace("]", "\"");

	/// <summary>
	/// Определение идентичности двух элементов
	/// </summary>
	/// <param name="dtElement">Элемент, сравниваемый с этим</param>
	/// <returns>true, если элементы имеют одинаковый идентификатор, иначе false</returns>
	public bool Equals(DtElement dtElement) =>
		dtElement != null && Id == dtElement.Id;

	/// <summary>
	/// Разбор элемента
	/// </summary>
	/// <returns>Сообщение об ошибке, или null в случае отсутствия ошибок</returns>
	internal virtual string Parse() =>
		null;

	/// <inheritdoc/>
	public override string ToString() =>
	$"{IgnoreAsString}{ElementOperation.GetOperationSign(Operation)} {ElementType}: {Name}";

	/*
	//public void SetFields(DtElement fromElement)
	//{
	//	Operation = fromElement.Operation;
	//	FirstLineWords = fromElement.FirstLineWords;
	//	CommentLines = fromElement.CommentLines;
	//}
	*/

	#endregion

	#region приватные свойства

	/// <summary>
	/// Набор слов, из которых состоит первая строка исходного MS SQL-скрипта для этого элемента
	/// </summary>
	private protected string[] FirstLineWords { get; set; }

	#endregion

	#region приватные методы

	/// <summary>
	/// Назначение свойств экземпляра
	/// </summary>
	/// <param name="operation">SQL-операция с этим экземпляром</param>
	/// <param name="firstLineWords">Слова первой строки исходного SQL-скрипта</param>
	private void SetFields(string operation, string[] firstLineWords)
	{
		Operation = ElementOperation.GetOperation(operation);
		FirstLineWords = firstLineWords;
	}

	#endregion

	#region статические элементы

	/// <summary>
	/// Определение типа и создание экземпляра элемента
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

	#endregion
}
