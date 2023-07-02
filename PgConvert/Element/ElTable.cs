namespace PgConvert.Element;

public class ElTable : DtElement
{
	private readonly string[] _indexSign = new string[]
		{
			"primary",
			"constraint",
			"unique",
		};

	List<DtField> Fields { get; set; } = new List<DtField>();
	List<ElIndex> Indexes { get; set; } = new List<ElIndex>();
	List<ElTrigger> Triggers { get; set; } = new List<ElTrigger>();

	public override DtField[] GetChild
		=> Fields.ToArray();

	public ElTable()
	{
		SelectFor = ElmType.Table;
	}

	internal override string Parse()
	{
		if (ElmOperation.Create != Operation)
			return null;

		// определение позиций запятых внутри внешних круглых скобок
		var commaIndexList = GetSymbolIndexes(out int? indexFieldsOpen, out int lengthFieldsClose);
		// если нет открывающей или закрывающей круглой скобки
		if (!indexFieldsOpen.HasValue || 0 == lengthFieldsClose)
			return null;

		var fieldsDraft = ParseDrafgFields(commaIndexList, indexFieldsOpen, lengthFieldsClose);
		if (!fieldsDraft.Any())
			return null;

		// определение полей по их строкам описания
		foreach (var fieldDraft in fieldsDraft)
		{
			var pieces = fieldDraft.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (null == pieces || !pieces.Any() || pieces.Length < 3)
				return $"Описание поля таблицы {Name} '{fieldDraft}' содержит меньше трёх элементов.";

			// индекс
			if (_indexSign.Contains(pieces[0].ToLower()))
			{
				var index = new ElIndex();
				Indexes.Add(index);
			}
			else
			// обычное поле
			{
				var field = new DtField(pieces);
				Fields.Add(field);
			}
		}
		return null;
	}

	/// <summary>
	/// Разбор полей по запятым
	/// </summary>
	/// <param name="commaIndexList">Список позиций запятых внутри внешних круглых скобок</param>
	/// <param name="indexFieldsOpen">Позиция первой (внешней) открывающей круглой скобки</param>
	/// <param name="lengthFieldsClose">Длина строки от внешней открывающей до внешней закрывающей круглой скобки</param>
	/// <returns>Набор подстрок, соответстваующих полям траблицы</returns>
	private List<string> ParseDrafgFields(List<int> commaIndexList, int? indexFieldsOpen, int lengthFieldsClose)
	{
		var fieldsDraft = new List<string>();
		var fieldsOpenIndex = indexFieldsOpen.Value;
		if (commaIndexList.Count > 0)
		{
			// первое поле
			fieldsDraft.Add(LinesAsString[fieldsOpenIndex..commaIndexList[0]].Trim());
			// средние поля
			for (int i = 0; i < commaIndexList.Count - 1; i++)
			{
				fieldsDraft.Add(
					LinesAsString.Substring(
						commaIndexList[i] + 1,
						commaIndexList[i + 1] - commaIndexList[i] - 1
					).Trim()
				);
			}
			// последнее поле
			fieldsDraft.Add(LinesAsString[(commaIndexList[^1] + 1)..].Trim());
		}
		else
		{
			// единственное поле
			fieldsDraft.Add(LinesAsString.Substring(fieldsOpenIndex, lengthFieldsClose).Trim());
		}
		return fieldsDraft;
	}

	/// <summary>
	/// Определение позиций внешних круглых скобок и запятых внутри внешних круглых скобок
	/// </summary>
	/// <param name="indexFieldsOpen">Позиция первой (внешней) открывающей круглой скобки</param>
	/// <param name="lengthFieldsClose">Длина строки от внешней открывающей до внешней закрывающей круглой скобки</param>
	/// <returns>Список позиций запятых</returns>
	private List<int> GetSymbolIndexes(out int? indexFieldsOpen, out int lengthFieldsClose)
	{
		indexFieldsOpen = null;
		lengthFieldsClose = 0;
		List<int> commaIndexList = new();
		int innerParentheses = 0;
		for (int i = 0; i < LinesAsString.Length; i++)
		{
			switch (LinesAsString[i])
			{
				case '(':
					if (!indexFieldsOpen.HasValue)
						indexFieldsOpen = i + 1;
					else
						innerParentheses++;
					continue;

				case ')':
					if (innerParentheses > 0)
					{
						innerParentheses--;
						continue;
					}
					if (!indexFieldsOpen.HasValue)
						return commaIndexList;
					lengthFieldsClose = i - indexFieldsOpen.Value;
					continue;

				case ',':
					if (innerParentheses <= 0)
						commaIndexList.Add(i);
					continue;
			}
		}
		return commaIndexList;
	}
}
