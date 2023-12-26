using PgConvert.Config;
using PgConvert.Enums;
using System.Text;
using System.Text.Json.Serialization;

namespace PgConvert.Element;

/// <summary>
/// Элемент: триггер
/// </summary>
public class ElTrigger : ElBaseForTable, IEdited
{
	#region константы и поля

	private const string _after = "after";
	private const string _insert = "insert";
	private const string _update = "update";
	private const string _delete = "delete";
	private const string _insteadOf = "INSTEAD OF";
	private const string _plangNameEnd = "AS $$";
	private const string _or = " or ";
	//private const string _end = "end";

	private const char _doubleQuot = '"';
	private const char _comma = ',';
	private const char _space = ' ';

	private bool isOk;
	private string _triggerFunctionName;

	#endregion

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="lines">Набор строк, определяющий изначальный MS SQL-скрипт для этого элемента</param>
	public ElTrigger(string[] lines) : base(lines) =>
		ElementType = ElmType.Trigger;

	#region реализация интерфейса IEdited

	/// <inheritdoc/>
	public bool CanSetOk =>
		true;

	/// <inheritdoc/>
	public void SetOk(bool ok) =>
		isOk = ok;

	/// <inheritdoc/>
	public bool IsOk =>
		isOk;

	#endregion

	#region публичные свойства

	/// <summary>
	/// Условие выполнения триггера
	/// </summary>
	[JsonIgnore]
	public TriggerDoing TriggerDoing { get; private set; }

	/// <summary>
	/// Тип триггера
	/// </summary>
	[JsonIgnore]
	public TriggerType TriggerType { get; private set; }

	/// <summary>
	/// Текст тела функции для триггера в терминах PostgreSQL
	/// </summary>
	public string[] LinesPg { get; set; }

	/// <summary>
	/// Тип языка функции триггера
	/// </summary>
	[JsonIgnore]
	public Plang PLanguage { get; set; }

	/// <summary>
	/// Если язык функции триггера не стандартный, он сохраняется здесь
	/// </summary>
	[JsonIgnore]
	public string OwnVariantLanguage { get; internal set; }

	/// <summary>
	/// Является ли язык триггерной функции пользовательским (не стандартным)
	/// </summary>
	public bool IsOwnVariantLanguage =>
		PLanguage == Plang.OwnVariant;

	/// <summary>
	/// Текст для самого триггера в терминах PostgreSQL
	/// </summary>
	public string TriggerPg { get; set; }

	/// <summary>
	/// Нименование триггера
	/// </summary>
	public override string Name =>
		_name;

	/// <summary>
	/// Имя триггерной функции
	/// </summary>
	public string TriggerFunctionName
	{
		get
		{
			if (string.IsNullOrEmpty(_triggerFunctionName))
			{
				var name = Name;
				bool isDoubleQuot = name.EndsWith(_doubleQuot);
				if (isDoubleQuot)
					name = name[..^1];
				_triggerFunctionName = $"{name}_function{(isDoubleQuot ? _doubleQuot : string.Empty)}";
			}

			return _triggerFunctionName;
		}
		set =>
			_triggerFunctionName = value;
	}

	/// <summary>
	/// Окончание скрипта создания триггерной функции
	/// </summary>
	public static string TriggerFunctionTextEnd =>
		"$$;";

	/// <summary>
	/// Начало скрипта создания триггерной функции
	/// </summary>
	public string TriggerFunctionTextBegin =>
		$"CREATE OR REPLACE FUNCTION {TriggerFunctionName}() RETURNS TRIGGER LANGUAGE ";

	/// <summary>
	/// Название процедурного языка триггерной функции
	/// </summary>
	public string PlangName =>
		PLanguage?.Name ?? OwnVariantLanguage;

	#endregion

	#region публичные методы

	/// <summary>
	/// Возврат текста определения триггера в зависимости от имени триггерной функции
	/// </summary>
	/// <param name="triggerFunctionName">Имя триггерной функции</param>
	/// <returns>Текст определения триггера</returns>
	public string GetTriggerText(string triggerFunctionName)
	{
		if (TriggerType == TriggerType.None)
			return "Err: Не определён тип триггера!";

		if (null != triggerFunctionName)
			TriggerFunctionName = triggerFunctionName;

		var afterInstd = TriggerDoing.After == TriggerDoing
			? _after.ToUpper()
			: _insteadOf;

		return $@"CREATE TRIGGER {Name}
{afterInstd} {GetIud()} ON {TableNames[0]}
FOR EACH ROW EXECUTE FUNCTION {TriggerFunctionName}();";

		string GetIud()
		{
			var iud = new StringBuilder();
			var isAppend = IsInsert;
			if (isAppend)
				iud.Append(_insert);
			if (IsUpdate)
			{
				iud.Append((isAppend ? _or : string.Empty) + _update);
				isAppend = true;
			}
			if (IsDelete)
				iud.Append((isAppend ? _or : string.Empty) + _delete);
			return iud.ToString().ToUpper();
		}
	}

	/// <summary>
	/// Получить первую строку скрипта создания триггерной функции
	/// </summary>
	/// <param name="nameIsNull">Являетася ли язык триггерной функции до сих пор неопределённым</param>
	public string GetTriggerFunctionFirstString(out bool nameIsNull)
	{
		nameIsNull = string.IsNullOrEmpty(PlangName);
		return $"{(nameIsNull ? PLanguage?.Name : PlangName)} {_plangNameEnd}";
	}

	/// <summary>
	/// Определение значения свойства OwnVariantLanguage
	/// </summary>
	/// <param name="text">Текст первой строки скрипта создания триггерной функции,
	/// изменённый пользователем</param>
	public void SetOwnVariantLanguage(string text) =>
		OwnVariantLanguage = string.IsNullOrEmpty(text)
		? null
		: text
			.Replace(_plangNameEnd, string.Empty)
			.Replace(" ", string.Empty)
			.Trim();

	/// <inheritdoc/>
	internal override string Parse()
	{
		if (FirstLineWords.Length < 2)
			return "Err: FirstLineWords.Length < 2";
		if (ClearLines.Length < 3)
			return "Err: ClearLines.Length < 3";

		_name = ClearBraces(FirstLineWords[2]);

		// определение тела функции
		if (null == LinesPg)
			NeedCorrect.LinesPgFromLines(this);

		// определение имени таблицы
		SetTableName(ClearBraces(ClearLines[1].Split(_space, StringSplitOptions.RemoveEmptyEntries)[1]));

		// определение типа триггера
		var clearLine2 = ClearLines[2]
			.ToLower()
			.Split(new char[] { _comma, _space }, StringSplitOptions.RemoveEmptyEntries);
		if (clearLine2.Length > 1)
		{
			if (clearLine2[0] == _after)
				TriggerDoing = TriggerDoing.After;
			for (var i = 1; i < clearLine2.Length; i++)
				switch (clearLine2[i])
				{
					case _insert: SetInsert(); break;
					case _delete: SetDelete(); break;
					case _update: SetUpdate(); break;
					default: return $"Err: Unknown trigger type '{clearLine2[i]}'.";
				}
		}
		return null;
	}

	/// <inheritdoc/>
	public override string ToString() =>
		base.ToString() + $" ON ({string.Join(_comma, TableNames)})";

	#endregion

	#region приватные свойства

	/// <summary>
	/// Относится ли триггер к событию INSERT
	/// </summary>
	private bool IsInsert =>
		(TriggerType | TriggerType.Insert) == TriggerType.Insert;

	/// <summary>
	/// Относится ли триггер к событию DELETE
	/// </summary>
	private bool IsDelete =>
		 (TriggerType | TriggerType.Delete) == TriggerType.Delete;

	/// <summary>
	/// Относится ли триггер к событию UPDATE
	/// </summary>
	private bool IsUpdate =>
		(TriggerType | TriggerType.Update) == TriggerType.Update;

	#endregion

	#region приватные методы

	/// <summary>
	/// Отнесение триггера к событию INSERT
	/// </summary>
	private void SetInsert() =>
		TriggerType |= TriggerType.Insert;

	/// <summary>
	/// Отнесение триггера к событию DELETE
	/// </summary>
	private void SetDelete() =>
		TriggerType |= TriggerType.Delete;

	/// <summary>
	/// Отнесение триггера к событию UPDATE
	/// </summary>
	private void SetUpdate() =>
		TriggerType |= TriggerType.Update;

	#endregion
}
