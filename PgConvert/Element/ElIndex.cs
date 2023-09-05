namespace PgConvert.Element;

public class ElIndex : ElBaseForTable
{
	private const string CHECK = "CHECK";
	private const string CONSTRAINT = "CONSTRAINT";

	public string[] FieldNames { get; set; }

	/// <summary>
	/// Имя таблицы, к которой применяется этот индекс
	/// </summary>
	public string TableName { get; set; }

	public ElIndex(string[] lines, string fromTable, string indexName = null) : base(lines)
	{
		if (lines == null || lines.Length == 0)
			throw new ArgumentNullException(nameof(lines));

		ElementType = ElmType.Index;
		Operation = ElmOperation.Create;
		if (!string.IsNullOrEmpty(fromTable))
		{
			SetTableName(fromTable);
			name = ClearBraces(indexName);
		}
	}

	public override string ToString() =>
		$"{base.ToString()} ON ({string.Join(',', TableNames)})";

	internal protected override string Name =>
		name;

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0056:Использовать оператор индекса", Justification = "<Ожидание>")]
	internal override string Parse()
	{
		SetTableName(ClearBraces(FirstLineWords[FirstLineWords.Length - 1]));
		name = ClearBraces(FirstLineWords[FirstLineWords.Length - 1 - 2]);
		return null;
	}

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
}
