using PgConvert.Element;
using System.Text;

namespace PgConvert;

public class DtFieldType
{
	private const string _max = "max";

	public DtFieldType()
	{
	}

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

		if (null == pieces || pieces.Length < 1)
		{
			FieldType = FldType.None;
			return;
		}

		FieldType = GetFieldTypeMs(pieces[0]);
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

	FldType FieldType { get; set; }
	int Len { get; set; }
	int LenDecimal { get; set; }
	bool IsMax { get; set; }

	internal static FldType GetFieldTypeMs(string typeNameMs)
	{
		return typeNameMs switch
		{
			"bigint" => FldType.Int64,
			"bit" => FldType.Bool,
			"char" => FldType.Char,
			"date" => FldType.Date,
			"datetime" => FldType.DateTime,
			"datetime2" => FldType.TimeStamp,
			"decimal" => FldType.Numeric,
			"int" => FldType.Int32,
			"nvarchar" => FldType.Varchar,
			"smallint" => FldType.Int16,
			"tinyint" => FldType.Byte,
			"uniqueidentifier" => FldType.Guid,
			"varbinary" => FldType.ByteA,
			"varchar" => FldType.Varchar,
			_ => FldType.None,
		};
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
		return sb.ToString();
	}
}