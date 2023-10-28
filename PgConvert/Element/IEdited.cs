using PgConvert.Config;

namespace PgConvert.Element;

public interface IEdited
{
	/// <summary>
	/// Признак того, что элемент проверен и, если необходимо, приведён в соответствие с требованиями T-SQL
	/// </summary>
	bool IsOk { get; }
	OnePgDatabase Database { get; }
	ElmType ElementType { get; }
}
