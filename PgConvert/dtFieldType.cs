using PgConvert.Enums;
using System.Text;

namespace PgConvert;

/// <summary>
/// Тип поля таблицы
/// </summary>
public class DtFieldType
{
	private const string _max = "max";

	/// <summary>
	/// Конструктор
	/// </summary>
	public DtFieldType() { }

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="typeNameMs">Название типа поля в скрипте MS SQL</param>
	/// <exception cref="Exception">Определение типа некорректно</exception>
	public DtFieldType(string typeNameMs)
	{
		if (string.IsNullOrEmpty(typeNameMs))
		{
			FieldType = FldType.None;
			return;
		}

		var pieces = typeNameMs.Split(
			new char[] { '(', ')' },
			StringSplitOptions.RemoveEmptyEntries);

		if (pieces.Length < 1)
		{
			FieldType = FldType.None;
			return;
		}

		FieldType = GetFieldTypeMs(pieces[0].Replace("\"", null));
		if (pieces.Length > 2)
			throw new Exception($"В строке '{typeNameMs}' количество разбираемых элементов больше двух.");

		if (pieces.Length > 1)
		{
			var byComma = pieces[1].Split(',');
			if (byComma.Length > 1)
				LenDecimal = int.Parse(byComma[1]);
			IsMax = _max == byComma[0];
			if (!IsMax)
				Len = int.Parse(byComma[0]);
		}
	}

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="fieldType">Тип поля</param>
	/// <param name="precision">Точность числового или длина строкового поля</param>
	/// <param name="scale">Масштаб числового поля</param>
	public DtFieldType(FldType fieldType, int precision, int scale)
	{
		FieldType = fieldType;
		Len = precision;
		LenDecimal = scale;
	}

	/// <summary>
	/// Тип поля
	/// </summary>
	internal FldType FieldType { get; }

	/// <summary>
	/// Точность десятичных значений NUMERIC(точность, масштаб)
	/// </summary>
	/// 
	int Len { get; set; }

	/// <summary>
	/// Масштаб десятичных значений NUMERIC(точность, масштаб)
	/// </summary>
	int LenDecimal { get; set; }

	/// <summary>
	/// Максимально возможная длина поля
	/// </summary>
	bool IsMax { get; set; }

	/// <summary>
	/// Получение строки описания типа для PostgreSQL
	/// </summary>
	internal string GetFieldTypePg()
	{
		return FieldType switch
		{
			FldType.Bool => "BOOL",
			FldType.Byte => "SMALLINT",
			FldType.ByteA => "BYTEA",
			FldType.Char => "TEXT",
			FldType.Date => "DATE",
			FldType.DateTime => "TIMESTAMP(3)",
			FldType.Guid => "CHAR(16)",
			FldType.Int16 => "SMALLINT",
			FldType.Int32 => "INT",
			FldType.Int64 => "BIGINT",
			FldType.Money => "MONEY",
			FldType.Numeric => "NUMERIC",
			FldType.TimeStamp => "TIMESTAMP",
			FldType.Varchar => "TEXT",
			_ => string.Empty,
		};
	}

	/// <summary>
	/// Получение типа по строке его описания в скрипте MS SQL
	/// </summary>
	/// <param name="typeNameMs">Описание типа в скрипте MS SQL</param>
	/// <returns>Тип, соответствующий его описанию. Если определить тип не удалось, то FldType.None</returns>
	internal static FldType GetFieldTypeMs(string typeNameMs) =>
		typeNameMs switch
		{
			"bigint" => FldType.Int64,
			"bit" => FldType.Bool,
			"char" => FldType.Char,
			"date" => FldType.Date,
			"datetime" => FldType.DateTime,
			"datetime2" => FldType.TimeStamp,
			"decimal" => FldType.Numeric,
			"int" => FldType.Int32,
			"money" => FldType.Money,
			"nvarchar" => FldType.Varchar,
			"smallint" => FldType.Int16,
			"tinyint" => FldType.Byte,
			"uniqueidentifier" => FldType.Guid,
			"varbinary" => FldType.ByteA,
			"varchar" => FldType.Varchar,
			_ => FldType.None,
		};

	/// <summary>
	/// Получение определения поля для сохранения его в файле
	/// </summary>
	/// <returns></returns>
	internal string GetNeedCorrect()
	{
		StringBuilder sb = new($"{FieldType}");
		sb.Append(DtField._sygn);
		sb.Append($"{Len}");
		sb.Append(DtField._sygn);
		sb.Append($"{LenDecimal}");
		sb.Append(DtField._sygn);
		sb.Append($"{IsMax}");
		return $"{sb}";
	}

	/// <summary>
	/// Получение типа из его сохраненного определения в файле
	/// </summary>
	/// <param name="fType">Элемент перечисления типов</param>
	/// <param name="len">Точность десятичных значений NUMERIC(точность, масштаб) или длина текстового поля</param>
	/// <param name="lenDecimal">Масштаб десятичных значений NUMERIC(точность, масштаб)</param>
	/// <param name="isMax">Является ли этот тип с максимално возможной длиной поля</param>
	/// <returns>Определённый по параметрам тип, или null, если определить тип не удалось</returns>
	internal static DtFieldType GetNeedCorrect(
		string fType,
		string len,
		string lenDecimal,
		string isMax)
	{
		if (!Enum.TryParse(typeof(FldType), fType, true, out var result) ||
			!int.TryParse(len, out int precision) ||
			!int.TryParse(lenDecimal, out int scale))
			return null;
		var ft = new DtFieldType((FldType)result, precision, scale);
		if (bool.TryParse(isMax, out bool isMaxDecimal))
			ft.IsMax = isMaxDecimal;
		return ft;
	}

	public override string ToString()
	{
		var sb = new StringBuilder($"{FieldType}");
		if (IsMax)
			sb.Append("(MAX)");
		if (Len > 0)
		{
			sb.Append('(');
			sb.Append(Len);
			if (LenDecimal > 0)
			{
				sb.Append(", ");
				sb.Append(LenDecimal);
			}
			sb.Append(')');
		}
		return $"{sb}";
	}
}