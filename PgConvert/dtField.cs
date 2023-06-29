namespace PgConvert;

public class DtField
{
	string Name { get; set; }
	DtFieldType FieldType { get; set; }
	public DtField(string name, DtFieldType fieldType)
	{
		Name = name;
		FieldType = fieldType;
	}

	public override string ToString() =>
		Name;
}