namespace PgConvert
{
	public class DtFieldType
	{
		FldType FieldType { get; set; }
		int Len { get; set; }
		int LenDecimal { get; set; }
		bool NotNull { get; set; }

		internal static FldType GetFieldTypeMs(string typeNameMs)
		{
			if (string.IsNullOrEmpty(typeNameMs)) return FldType.None;
			var typeName = typeNameMs.ToLower();
			return typeName switch
			{
				"varchar" => FldType.Varchar,
				_ => FldType.None,
			};
		}
	}
}