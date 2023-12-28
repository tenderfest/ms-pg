using PgConvert.Enums;
using System.Text;

namespace PgConvert;

/// <summary>
/// Определение поля таблицы базы данных
/// </summary>
#pragma warning disable IDE0056 // Использовать оператор индекса
public class DtField
{
	public const char _sygn = '^';

	private const string _generated = "as";
	/// <summary>
	/// Признак СОХРАНЯЕМОГО вычисляемого поля
	/// </summary>
	private const string _persisted = "persisted";
	private const string _not = "not";
	private const string _null = "null";
	private const string _notNull = " NotNull";

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="pieces"></param>
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
			FormulaMs = ParseFormulaForCalculatedField(pieces, out bool isPersisted);
			FormulaPg = Clear(FormulaMs);
			Persisted = isPersisted;
			FieldType = new DtFieldType();
		}
		else
		// простое поле
		{
			FieldType = new DtFieldType(secondPiece);
		}
	}

	/// <summary>
	/// Название поля
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Тип поля
	/// </summary>
	public DtFieldType FieldType { get; set; }

	/// <summary>
	/// Является ли поле Not Null
	/// </summary>
	public bool NotNull { get; set; }

	/// <summary>
	/// Вычисляемое поле - формула MS SQL
	/// </summary>
	public string FormulaMs { get; set; }

	/// <summary>
	/// Вычисляемое поле - формула PostgreSQL
	/// </summary>
	public string FormulaPg { get; set; }

	/// <summary>
	/// Устанавливаемый пользователем признак того, что вычисляемое поле
	/// откорректировано для PostgreSQL
	/// </summary>
	public bool CorrectIsDone { get; private set; }

	/// <summary>
	/// Поле является вычисляемым сохраняемым
	/// </summary>
	public bool Persisted { get; set; }

	/// <summary>
	/// Текст для вычисляемого поля для SQL-запроса, создающего таблицу
	/// </summary>
	public string GeneratedFieldPg =>
		$"{Name} {FieldType} GENERATED ALWAYS AS ({FormulaPg}) STORED";

	/// <summary>
	/// Утверждение вычисляемого поля как подходящего для PostgreSQL
	/// </summary>
	public void SetOk(bool ok) =>
		CorrectIsDone = ok;

	/// <summary>
	/// Поле является вычисляемым
	/// </summary>
	public bool IsGenerated =>
		!string.IsNullOrEmpty(FormulaMs);

	/// <summary>
	/// Тип этого поля не определён
	/// </summary>
	public bool IsFieldTypeNone =>
		FldType.None == FieldType?.FieldType;

	/// <summary>
	/// Форматирование определения поля для сохранения в файле
	/// </summary>
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

	/// <summary>
	/// Представление вычисляемого поля для отображения
	/// </summary>
	public string GeneratedFieldToString =>
		$"{Name}{(
			Persisted ? $" {_persisted}" : string.Empty)}{(
			NotNull ? _notNull : string.Empty)}";

	public override string ToString() =>
		$"{(IsGenerated ? "(1+2) " : string.Empty)}{Name} {(
			IsFieldTypeNone ? "???" : FieldType)}{(
			NotNull ? _notNull : string.Empty)}";

	#region приватные методы

	private static string ClearToLower(string str) =>
		Clear(str).ToLower();

	private static string Clear(string str) =>
		str
		.Trim()
		.Replace("[", "\"")
		.Replace("]", "\"");

	#endregion

	#region статические методы

	/// <summary>
	/// Получение строки определения типа поля по кусочкам такой строки в исходном скрипте
	/// Разбор строки определения типа
	/// </summary>
	private static string ParseFormulaForCalculatedField(string[] pieces, out bool isPersisted)
	{
		// от 3 до конца, кроме _persisted _not _null
		var formula = pieces.Skip(2);
		SkipLast(ref formula, _null);
		SkipLast(ref formula, _not);
		isPersisted = SkipLast(ref formula, _persisted);
		return string.Join(' ', formula);

		/// <summary>
		/// Отсечение последнего элемента описания, если он соответствует переданному
		/// </summary>
		/// <returns>true, если отсечение произошло, иначе false</returns>
		static bool SkipLast(ref IEnumerable<string> formula, string skipField)
		{
			var needSkip = ClearToLower(formula.Last()) == skipField;
			if (needSkip)
				formula = formula.SkipLast(1);
			return needSkip;
		}
	}

	/// <summary>
	/// Разбор определения поля из сохранённого в файле
	/// </summary>
	/// <param name="correctField"></param>
	/// <param name="fields"></param>
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

	/// <summary>
	/// Установка значений Precision и Scale для типа этого поля
	/// </summary>
	/// <param name="precision">Точность числового или длина строкового поля</param>
	/// <param name="scale">Масштаб числового поля</param>
	/// <returns>Сообщение об ошибке, если значения некорректны, либо null, если ошибок нет</returns>
	public string SetPrecisionScale(decimal precision, decimal scale)
	{
		try
		{
			FieldType.Precision = Convert.ToInt32(precision);
			FieldType.Scale = Convert.ToInt32(scale);
			return null;
		}
		catch (Exception ex)
		{
			return $"{ex}";
		}
	}

	#endregion
}