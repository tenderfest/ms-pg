using PgConvert.Element;

namespace PgConvert;

#pragma warning disable IDE0056 // Использовать оператор индекса
public class DtField 
{
	public const string _correctSygn = "^";
	private const string _generated = "as";
	private const string _persisted = "persisted"; // СОХРАНЯЕТСЯ
	private const string _not = "not";
	private const string _null = "null";

	private string GeneratedFieldPg =>
		$"{Name} numeric GENERATED ALWAYS AS (height_cm / 2.54) STORED";

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
			NotNull ? " NotNull" : string.Empty)}";

	internal static (string, string) GetCorrectFieldName(string correctField)
	{
		if (string.IsNullOrEmpty(correctField))
			return (null, null);
		var name = correctField.Split(_correctSygn);
		if (name.Length != 2)
			return (null, null);
		return (name[0], name[1]);
	}

	public string NeedCorrect =>
		$"{Name}{_correctSygn}{CorrectIsDone}{_correctSygn}{FormulaPg}";
}