namespace PgConvert.Element;

public class Plang
{
	private readonly string _name;
	private readonly string _showName;

	public Plang(string name, string showName)
	{
		_name = name;
		_showName = showName;
	}

	public override string ToString() =>
		_showName;

	public string Name =>
		_name;
	public static Plang[] Langs =>
		langs;

	private static readonly Plang[] langs = new Plang[7]
	{
		new("SQL", "SQL"),
		new("plpgsql", "PL/pgSQL"),
		new("pltcl", "PL/Tcl"),
		new("plperl", "PL/Perl"),
		new("plpython3u", "PL/Python"),
		new("C", "C"),
		new(null, "Собственный вариант"),
	};
}
