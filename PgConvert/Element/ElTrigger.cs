using PgConvert.Config;
using System.Text;
using System.Text.Json.Serialization;

namespace PgConvert.Element;

public class ElTrigger : ElBaseForTable, IEdited
{
	private const string _after = "after";
	private const string _insert = "insert";
	private const string _update = "update";
	private const string _delete = "delete";
	private const string _insteadOf = "INSTEAD OF";
	private const string _or = " or ";
	//private const string _end = "end";

	private const char _doubleQuot = '"';
	private const char _comma = ',';
	private const char _space = ' ';

	public ElTrigger(string[] lines) : base(lines)
	{
		ElementType = ElmType.Trigger;
	}

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

	private void SetInsert() => TriggerType |= TriggerType.Insert;
	private void SetDelete() => TriggerType |= TriggerType.Delete;
	private void SetUpdate() => TriggerType |= TriggerType.Update;

	private bool IsInsert =>
		(TriggerType | TriggerType.Insert) == TriggerType.Insert;
	private bool IsDelete =>
		 (TriggerType | TriggerType.Delete) == TriggerType.Delete;
	private bool IsUpdate =>
		(TriggerType | TriggerType.Update) == TriggerType.Update;

	/// <summary>
	/// Текст тела функции для триггера в терминах PostgreSQL
	/// </summary>
	public string[] LinesPg { get; set; }

	private bool isOk;
	public bool IsOk =>
		isOk;
	OnePgDatabase IEdited.Database =>
		Database;

	public void SetOk(bool ok) =>
		isOk = ok;

	/// <summary>
	/// Текст для самого триггера в терминах PostgreSQL
	/// </summary>
	public string TriggerPg { get; set; }

	public override string ToString() =>
		base.ToString() + $" ON ({string.Join(_comma, TableNames)})";

	public override string Name =>
		name;

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
		set
		{
			_triggerFunctionName = value;
		}
	}
	private string _triggerFunctionName;

	internal override string Parse()
	{
		if (FirstLineWords.Length < 2)
			return "Err: FirstLineWords.Length < 2";
		if (ClearLines.Length < 3)
			return "Err: ClearLines.Length < 3";

		name = ClearBraces(FirstLineWords[2]);

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

	public string GetTriggerFunctionTextEnd() =>
		"$$ LANGUAGE plpgsql;";
	public string GetTriggerFunctionTextBegin() =>
		$"CREATE OR REPLACE FUNCTION {TriggerFunctionName} RETURNS TRIGGER AS $$";
}
