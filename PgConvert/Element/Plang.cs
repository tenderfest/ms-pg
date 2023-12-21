namespace PgConvert.Element;

/// <summary>
/// Процедурный язык, использующийся в триггерной функции
/// </summary>
public class Plang
{
	private const string _PgSQL = "plpgsql";
	private const string _Tcl = "pltcl";
	private const string _Perl = "plperl";
	private const string _Python = "plpython3u";
	private const string _C = "C";
	private const string _SQL = "SQL";

	private readonly string _name;
	private readonly string _showName;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="name">Название языка для SQL-скрипта</param>
	/// <param name="showName">Отображаемое название языка</param>
	public Plang(string name, string showName)
	{
		_name = name;
		_showName = showName;
	}

	/// <summary>
	/// Название процедурного языка
	/// </summary>
	public string Name =>
		_name;

	public override string ToString() =>
		_showName;

	#region статические элементы

	public static readonly Plang OwnVariant = new(null, "Собственный вариант");
	public static readonly Plang PgSQL = new(_PgSQL, "PL/pgSQL");
	public static readonly Plang Tcl = new(_Tcl, "PL/Tcl");
	public static readonly Plang Perl = new(_Perl, "PL/Perl");
	public static readonly Plang Python = new(_Python, "PL/Python");
	public static readonly Plang C = new(_C, "C");
	public static readonly Plang SQL = new(_SQL, "SQL");

	/// <summary>
	/// Массив всех языков
	/// </summary>
	private static readonly Plang[] _langs = new Plang[] {
		OwnVariant, PgSQL, Tcl, Perl, Python, C, SQL,
	};

	/// <summary>
	/// Массив всех языков
	/// </summary>
	public static Plang[] Langs =>
		_langs;

	/// <summary>
	/// Получение языка по его названию
	/// </summary>
	/// <param name="languageName">Название языка</param>
	/// <returns>Язык, соответстующий названию. Если такого названия нет, то "собственный язык"</returns>
	internal static Plang GetByName(string languageName) =>
		languageName switch
		{
			_C => C,
			_SQL => SQL,
			_Tcl => Tcl,
			_Perl => Perl,
			_PgSQL => PgSQL,
			_Python => Python,
			_ => OwnVariant,
		};

	#endregion
}
