using PgConvert.Enums;

namespace PgConvert.Element;

/// <summary>
/// Элемент: индекс
/// </summary>
public class ElIndex : ElBaseForTable
{
	#region константы и поля

	private const string CHECK = "CHECK";
	private const string CONSTRAINT = "CONSTRAINT";

	#endregion

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="lines">Набор строк, определяющий изначальный MS SQL-скрипт для этого элемента</param>
	/// <param name="fromTable">Имя таблицы, из определения которой взято определение этого индекса</param>
	/// <param name="indexName">Имя индекса</param>
	public ElIndex(string[] lines, string fromTable, string indexName = null) : base(lines)
	{
		if (lines == null || lines.Length == 0)
			throw new ArgumentNullException(nameof(lines));

		ElementType = ElmType.Index;
		Operation = ElmOperation.Create;
		if (!string.IsNullOrEmpty(fromTable))
		{
			SetTableName(fromTable);
			_name = ClearBraces(indexName);
		}
	}

	#region публичные свойства

	/// <summary>
	/// Имя индекса
	/// </summary>
	public override string Name =>
		_name;

	/// <summary>
	/// Массив полей, к которым применяется этот индекс
	/// </summary>
	public string[] FieldNames { get; set; }

	/// <summary>
	/// Имя таблицы, к которой применяется этот индекс
	/// </summary>
	public string TableName { get; set; }

	#endregion

	#region публичные методы

	/// <summary>
	/// Создание записи индекса из записи ALTER TABLE
	/// </summary>
	internal static IEnumerable<ElIndex> GetIndexesFromAlterTables(IEnumerable<ElTable> alterTables)
	{
		var list = new List<ElIndex>();
		foreach (var aTableLines in alterTables
			.Select(aTab =>
				aTab.Lines))
		{
			if (1 == aTableLines.Length)
			{
				var pieces = aTableLines[0].Split(' ');
				if (pieces.Length < 5 || CHECK == pieces[3] && CONSTRAINT == pieces[4])
					continue;
			}

			list.Add(new ElIndex(aTableLines, null));
		}
		return list;
	}

	/// <inheritdoc/>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0056:Использовать оператор индекса", Justification = "<Ожидание>")]
	internal override string Parse()
	{
		SetTableName(ClearBraces(FirstLineWords[FirstLineWords.Length - 1]));
		_name = ClearBraces(FirstLineWords[FirstLineWords.Length - 1 - 2]);
		return null;
	}

	/// <inheritdoc/>
	public override string ToString() =>
		$"{base.ToString()} ON ({string.Join(',', TableNames)})";

	#endregion
}
