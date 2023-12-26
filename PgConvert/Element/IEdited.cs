using PgConvert.Config;
using PgConvert.Enums;

namespace PgConvert.Element;

/// <summary>
/// Интерфейс, содержащий методы и свойства элементов, которые нужно вручную изменить
/// перед тем, как применять в SQL-скрипте для PostgreSQL
/// </summary>
public interface IEdited
{
	/// <summary>
	/// База данных, к которой относится элемент
	/// </summary>
	OnePgDatabase Database { get; }

	/// <summary>
	/// Тип элемента
	/// </summary>
	ElmType ElementType { get; }

	/// <summary>
	/// Можно ли для этого элемента установить признак проверенности вручгую (для полей таблиц - нет)
	/// </summary>
	bool CanSetOk { get; }

	/// <summary>
	/// Метод установки признака проверенности
	/// </summary>
	/// <param name="ok">Значение признака "проверен"</param>
	void SetOk(bool ok);

	/// <summary>
	/// Признак того, что элемент проверен и, если необходимо, приведён в соответствие 
	/// с требованиями PostgreSQL
	/// </summary>
	bool IsOk { get; }
}
