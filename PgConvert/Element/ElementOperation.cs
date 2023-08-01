namespace PgConvert.Element;

public enum ElmOperation
{
	None = 0,
	Create,
	Alter,
	Exec,
	Set,
	Use,
}

internal static class ElementOperation
{
	internal static ElmOperation GetOperation(string operation) =>
		operation.ToLower() switch
		{
			"create" => ElmOperation.Create,
			"set" => ElmOperation.Set,
			"alter" => ElmOperation.Alter,
			"exec" => ElmOperation.Exec,
			"use" => ElmOperation.Use,
			_ => ElmOperation.None,
		};

	internal static string GetOperationSign(ElmOperation operation) =>
	operation switch
	{
		ElmOperation.Create => "C",
		ElmOperation.Set => "S",
		ElmOperation.Alter => "A",
		ElmOperation.Exec => "E",
		ElmOperation.Use => "U",
		_ => "?",
	};
}
