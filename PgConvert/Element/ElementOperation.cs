using PgConvert.Enums;

namespace PgConvert.Element;

/// <summary>
/// Методы обработки операций элементов
/// </summary>
internal static class ElementOperation
{
	/// <summary>
	/// Получить тип операции по её текстовому описанию
	/// </summary>
	/// <param name="operation">Текстовое описание операции</param>
	/// <returns>Тип операции</returns>
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

	/// <summary>
	/// Получить буквенное обозначение операции
	/// </summary>
	/// <param name="operation">Операция</param>
	/// <returns>Буква для короткого отображения операции</returns>
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
