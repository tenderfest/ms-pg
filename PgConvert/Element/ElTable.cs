using PgConvert.Enums;

namespace PgConvert.Element;

/// <summary>
/// Элемент: таблица базы данных
/// </summary>
#pragma warning disable S2365 // Properties should not make collection or array copies
public class ElTable : ElBaseForTable, IEdited
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="lines">Набор строк, определяющий изначальный MS SQL-скрипт для этого элемента</param>
	public ElTable(string[] lines) : base(lines)
	{
		ElementType = ElmType.Table;
	}

	#region реализация интерфейса IEdited

	/// <inheritdoc/>
	public bool CanSetOk =>
		false;

	/// <inheritdoc/>
	public void SetOk(bool ok)
	{
	}

	/// <inheritdoc/>
	public bool IsOk =>
		!FieldsForCorrect.Any();

	#endregion

	#region публичные свойства

	/// <summary>
	/// Поля таблицы
	/// </summary>
	public List<DtField> Fields { get; private set; } = new List<DtField>();

	/// <summary>
	/// Признак того, что таблица содержит вычисляемые поля
	/// </summary>
	public bool IsGeneratedFields =>
		Fields?.Count(x => x.IsGenerated) > 0;

	/// <summary>
	/// Набор триггеров, относящихся к таблице
	/// </summary>
	public List<ElTrigger> Triggers { get; private set; } = new List<ElTrigger>();

	/// <summary>
	/// Внутритабличные индексы
	/// </summary>
	public List<ElIndex> IndexCreateTable { get; private set; } = new List<ElIndex>();

	/// <summary>
	/// Изменения таблицы: внешние ключи и индексы
	/// </summary>
	public List<ElTable> AlterTable { get; private set; } = new List<ElTable>();

	/// <summary>
	/// Параметры внешего ключа для ALTER TABLE
	/// </summary>
	public DtForeignKey ForeignKey { get; private set; }

	/// <summary>
	/// Индексы таблицы
	/// </summary>
	public IEnumerable<ElBaseForTable> Indexes =>
		IndexCreateTable
			.Select(x => x as ElBaseForTable)
		.Union(AlterTable
			.Select(x => x as ElBaseForTable));

	/// <summary>
	/// Вычисляемые поля таблицы, требующие утверждения пользователем
	/// </summary>
	public DtField[] FieldsForCorrect =>
		Fields
		.Where(x => x.IsGenerated && !x.CorrectIsDone)
		.ToArray();

	/// <summary>
	/// Спосок вычисляемых полей таблицы
	/// </summary>
	public string[] GeneratedFields
	{
		get => Fields
			.Where(x => x.IsGenerated)
			.Select(x => x.NeedCorrect)
			.ToArray();
		set
		{
			foreach (var correctField in value)
			{
				DtField.SetCorrectField(correctField, Fields);
			}
		}
	}

	#endregion

	#region публичные методы

	/// <summary>
	/// Добавление элементов к списку изменений этой таблицы
	/// </summary>
	/// <param name="alterTables">Добавляемые элементы</param>
	internal void AddAlterTable(IEnumerable<ElTable> alterTables) =>
		AlterTable.AddRange(alterTables);

	/// <summary>
	/// Добавление триггеров к списку триггеров этой балицы
	/// </summary>
	/// <param name="trigger">Добавляемые триггеры</param>
	internal void AddTriggers(ElTrigger[] trigger) =>
		Triggers.AddRange(trigger);

	/// <inheritdoc/>
	internal override string Parse()
	{
		// изменение таблицы
		if (ElmOperation.Alter == Operation)
		{
			var pieces = LinesAsString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (pieces.Length < 4)
				return $"ALTER TABLE таблицы {Name} содержит меньше четырёх элементов.";

			// если эта запись ALTER TABLE не внешний ключ, просто ничего не делаем
			if (!LinesAsStringLower.Contains(Const.FOREIGN_KEY))
				return null;

			var piecesLower = LinesAsStringLower.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (pieces.Length != piecesLower.Length)
				return $"Длины итоговых строк ALTER TABLE не совпадают.";

			ForeignKey = new DtForeignKey(piecesLower, pieces, Name);

			SetTableName(ForeignKey.FromTableName);
			SetTableName(ForeignKey.ToTableName);
			return null;
		}

		if (ElmOperation.Create != Operation)
			return null;
		// ниже по коду - обработка создания таблицы

		// определение позиций запятых внутри внешних круглых скобок
		var commaIndexList = GetSymbolIndexes(out int? indexFieldsOpen, out int lengthFieldsClose);
		// если нет открывающей или закрывающей круглой скобки
		if (!indexFieldsOpen.HasValue || 0 == lengthFieldsClose)
			return null;

		var fieldsDraft = ParseDraftFields(commaIndexList, indexFieldsOpen, lengthFieldsClose);
		if (!fieldsDraft.Any())
			return null;

		// определение полей по их строкам описания
		foreach (var fieldDraft in fieldsDraft)
		{
			var pieces = fieldDraft.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (!pieces.Any() || pieces.Length < 3)
				return $"Описание поля таблицы {Name} '{fieldDraft}' содержит меньше трёх элементов.";

			try
			{
				// индекс
				if (Const.IndexSign.Contains(pieces[0].ToLower()))
				{
					IndexCreateTable.Add(new ElIndex(new[] { fieldDraft }, Name, pieces[1]));
				}
				else
					// обычное поле
					Fields.Add(new DtField(pieces));
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}
		return null;
	}

	/// <inheritdoc/>
	public override string ToString() =>
		null == ForeignKey
			? base.ToString()
			: $"{IgnoreAsString}{ElementOperation.GetOperationSign(Operation)} {ElementType}: {ForeignKey.Name}";

	#endregion

	#region приватные методы

	/// <summary>
	/// Разбор полей по запятым
	/// </summary>
	/// <param name="commaIndexList">Список позиций запятых внутри внешних круглых скобок</param>
	/// <param name="indexFieldsOpen">Позиция первой (внешней) открывающей круглой скобки</param>
	/// <param name="lengthFieldsClose">Длина строки от внешней открывающей до внешней закрывающей круглой скобки</param>
	/// <returns>Набор подстрок, соответстваующих полям траблицы</returns>
	private List<string> ParseDraftFields(List<int> commaIndexList, int? indexFieldsOpen, int lengthFieldsClose)
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

	#endregion
}
