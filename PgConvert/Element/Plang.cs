
namespace PgConvert.Element;

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

	public Plang(string name, string showName)
	{
		_name = name;
		_showName = showName;
	}

	public override string ToString() =>
		_showName;

	internal static Plang GetByName(string language) =>
		language switch
		{
			_C => C,
			_SQL => SQL,
			_Tcl => Tcl,
			_Perl => Perl,
			_PgSQL => PgSQL,
			_Python => Python,
			_ => OwnVariant,
		};

	public string Name =>
		_name;
	public static Plang[] Langs =>
		langs;

	public static readonly Plang OwnVariant = new(null, "Собственный вариант");
	public static readonly Plang PgSQL = new(_PgSQL, "PL/pgSQL");
	public static readonly Plang Tcl = new(_Tcl, "PL/Tcl");
	public static readonly Plang Perl = new(_Perl, "PL/Perl");
	public static readonly Plang Python = new(_Python, "PL/Python");
	public static readonly Plang C = new(_C, "C");
	public static readonly Plang SQL = new(_SQL, "SQL");

	private static readonly Plang[] langs = new Plang[7] {
		OwnVariant, PgSQL, Tcl, Perl, Python, C, SQL,
	};
}
