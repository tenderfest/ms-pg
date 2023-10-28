using PgConvert.Config;
using System.Text.Json.Serialization;

namespace PgConvert.Element;

public class ElTrigger : ElBaseForTable, IEdited
{
	public ElTrigger(string[] lines) : base(lines)
	{
		ElementType = ElmType.Trigger;
	}

	/// <summary>
	/// Тип триггера
	/// </summary>
	[JsonIgnore]
	public TriggerType TriggerType { get; }

	/// <summary>
	/// Текст тела функции для триггера в терминах PostgreSQL
	/// </summary>
	public string[] LinesPg { get; set; }

	private bool isOk;
	public bool IsOk => isOk;
	OnePgDatabase IEdited.Database => base.Database;

	public void SetOk(bool ok) =>
		isOk = ok;

	/// <summary>
	/// Текст для самого триггера в терминах PostgreSQL
	/// </summary>
	public string TriggerPg { get; set; }

	public override string ToString() =>
		base.ToString() + $" ON ({string.Join(',', TableNames)})";

	internal protected override string Name =>
		name;

	internal override string Parse()
	{
		LinesPg ??= new string[Lines.Length];
		Lines.CopyTo(LinesPg, 0);

		SetTableName(ClearBraces(ClearLines.Length < 2
			? "ClearLines.Length < 2"
			: ClearLines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]));
		name = ClearBraces(FirstLineWords[2]);

		return null;
	}
}
