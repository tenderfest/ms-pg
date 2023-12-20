using PgConvert.Element;
using System.Text;

namespace PgConvert;

#pragma warning disable IDE0056 // Использовать оператор индекса
public class DtField
{
	public const char _sygn = '^';
	private const string _generated = "as";
	private const string _persisted = "persisted"; // СОХРАНЯЕТСЯ
	private const string _not = "not";
	private const string _null = "null";
	private const string _notNull = " NotNull";

	/// <summary>
	/// вид поля для SQL-запроса, создающего таблицу
	/// </summary>
	public string GeneratedFieldPg =>
		$"{Name} {FieldType} GENERATED ALWAYS AS ({FormulaPg}) STORED";

	public string Name { get; set; }
	public DtFieldType FieldType { get; set; }
	public bool NotNull { get; set; }

	/// <summary>
	/// вычисляемое поле - формула T-SQL
	/// </summary>
	public string FormulaMs { get; set; }
	/// <summary>
	/// вычисляемое поле - формула PostgreSQL
	/// </summary>
	public string FormulaPg { get; set; }
	/// <summary>
	/// Устанавливаемый пользователем признак того, что вычисляемое поле
	/// откорректировано для PostgreSQL
	/// </summary>
	public bool CorrectIsDone { get; private set; }

	/// <summary>
	/// Утверждение вычисляемого поля как подходящего для PostgreSQL
	/// </summary>
	public void SetOk(bool ok) =>
		CorrectIsDone = ok;

	/// <summary>
	/// вычисляемое сохраняемое
	/// </summary>
	public bool Persisted { get; set; }
	/// <summary>
	/// поле является вычисляемым
	/// </summary>
	public bool IsGenerated =>
		!string.IsNullOrEmpty(FormulaMs);

	public bool IsFieldTypeNone =>
		FldType.None == FieldType?.FieldType;

	public DtField(string[] pieces)
	{
		while (pieces.Length < 3)
			pieces = pieces.Append(_null).ToArray();

		Name = Clear(pieces[0]);
		var secondPiece = ClearToLower(pieces[1]);
		if (secondPiece.EndsWith(','))
			secondPiece += ClearToLower(pieces[2]);

		// ограничение NULL
		NotNull =
			_not == ClearToLower(pieces[pieces.Length - 2]) &&
			_null == ClearToLower(pieces[pieces.Length - 1]);

		if (_generated == secondPiece)
		// вычисляемое поле
		{
			FormulaPg = FormulaMs = ParseFormulaForCalculatedField(pieces, out bool isPersisted);
			Persisted = isPersisted;
			FieldType = new DtFieldType();
		}
		else
		// простое поле
		{
			FieldType = new DtFieldType(secondPiece);
		}
	}

	private static string ParseFormulaForCalculatedField(string[] pieces, out bool isPersisted)
	{
		// от 3 до конца, кроме _persisted _not _null
		var formula = pieces.Skip(2);
		SkipLast(ref formula, _null);
		SkipLast(ref formula, _not);
		isPersisted = SkipLast(ref formula, _persisted);
		return string.Join(' ', formula);

		static bool SkipLast(ref IEnumerable<string> formula, string skipField)
		{
			var needSkip = ClearToLower(formula.Last()) == skipField;
			if (needSkip)
				formula = formula.SkipLast(1);
			return needSkip;
		}
	}

	private static string ClearToLower(string str) =>
		Clear(str).ToLower();

	private static string Clear(string str) =>
		str
		.Trim()
		.Replace("[", null)
		.Replace("]", null);

	public override string ToString() =>
		$"{(IsGenerated ? "(1+2) " : string.Empty)}{Name} {(
			IsFieldTypeNone ? "???" : FieldType)}{(
			NotNull ? _notNull : string.Empty)}";

	internal static void SetCorrectField(string correctField, List<DtField> fields)
	{
		if (string.IsNullOrEmpty(correctField) ||
			null == fields)
			return;

		var piece = correctField.Split(_sygn);
		if (piece.Length < 7)
			return;

		var name = piece[0];
		var field = fields.Find(x => x.Name == name);
		if (null == field)
			return;

		if (bool.TryParse(piece[1], out bool isDone))
			field.SetOk(isDone);

		field.FieldType = DtFieldType.GetNeedCorrect(piece[2], piece[3], piece[4], piece[5]);
		field.FormulaPg = piece[6];
	}

	public string NeedCorrect
	{
		get
		{
			var sb = new StringBuilder(Name);
			sb.Append(_sygn);
			sb.Append(CorrectIsDone);
			sb.Append(_sygn);
			sb.Append(FieldType.GetNeedCorrect());
			sb.Append(_sygn);
			sb.Append(FormulaPg);
			return $"{sb}";
		}
	}

	// сохранять ещё тип

	public string GeneratedFieldToString =>
		$"{Name}{(
			Persisted ? $" {_persisted}" : string.Empty)}{(
			NotNull ? _notNull : string.Empty)}";
}