using Microsoft.VisualBasic.FileIO;
using PgConvert.Element;

namespace PgConvert;

public class DtField
{
	private const string _persisted = "persisted";
	private const string _generated = "as";
	private const string _not = "not";
	private const string _null = "null";

	string Name { get; set; }
	DtFieldType FieldType { get; set; }
	bool NotNull { get; set; }
	bool Generated { get; set; }
	public bool IsFieldTypeNone
		=> FldType.None == FieldType.FieldType;

	//public DtField(string name, DtFieldType fieldType)
	//{
	//	Name = name;
	//	FieldType = fieldType;
	//}

	public DtField(string[] pieces)
	{
		Name = Clear(pieces[0]);
		var secondPiece = ClearToLower(pieces[1]);
		if (secondPiece.EndsWith(','))
			secondPiece += ClearToLower(pieces[2]);

		Generated = _generated == secondPiece;
		if (!Generated)
		{
			FieldType = new DtFieldType(secondPiece);
		}
		else
		{
			FieldType = new DtFieldType();
		}
		// ограничение NULL
		NotNull =
			_not == ClearToLower(pieces[pieces.Length - 2]) &&
			_null == ClearToLower(pieces[pieces.Length - 1]);
	}

	private string ClearToLower(string str)
		=> Clear(str).ToLower();

	private static string Clear(string str)
		=> str
		.Trim()
		.Replace("[", null)
		.Replace("]", null);

	public override string ToString()
		=> $"{(IsFieldTypeNone ? "--- " : string.Empty)}{Name} {FieldType}";
}