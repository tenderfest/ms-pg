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

	//public DtField(string name, DtFieldType fieldType)
	//{
	//	Name = name;
	//	FieldType = fieldType;
	//}

	public DtField(string[] pieces)
	{
		Name = Clear(pieces[0]);
		var secondPiece = Clear(pieces[1]);
		if (secondPiece.EndsWith(','))
			secondPiece += Clear(pieces[2]);

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
			_not == Clear(pieces[pieces.Length - 2]) &&
			_null == Clear(pieces[pieces.Length - 1]);
	}

	private static string Clear(string str)
		=> str
		.Trim()
		.Replace("[", null)
		.Replace("]", null)
		.ToLower();

	public override string ToString()
		=> $"{Name} {FieldType}";
}