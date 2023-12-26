namespace PgConvert.Element;

/// <summary>
/// Внешний ключ для таблицы
/// </summary>
public class DtForeignKey
{
	#region публичные свойства

	/// <summary>
	/// Имя внешнего ключа
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Имя таблицы, которой принадлежит этот внешний ключ
	/// </summary>
	public string FromTableName { get; set; }

	/// <summary>
	/// Имя таблицы, на которую ссылается этот внешний ключ
	/// </summary>
	public string ToTableName { get; set; }

	/* TODO см. коммент ниже
	public List<string> FromField { get; set; }
	public List<string> ToField { get; set; }
	public bool OnUpdate { get; set; }
	public bool OnUpdateCascade { get; set; }
	public bool OnDelete { get; set; }
	public bool OnDeleteCascade { get; set; }
	*/

	#endregion

	/// <summary>
	/// Контруктор без параметров
	/// </summary>
	public DtForeignKey()
	{
	}

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="piecesLower">Массив, состоящий из слов строки исходного SQL-скрипта,
	/// относящихся к внешнему ключу, в нижнем регистре</param>
	/// <param name="pieces">Массив, состоящий из слов строки исходного SQL-скрипта,
	/// относящихся к внешнему ключу</param>
	/// <param name="fromTableName">Имя таблицы, которой принадлежит этот внешний ключ</param>
	public DtForeignKey(string[] piecesLower, string[] pieces, string fromTableName)
	{
		FromTableName = fromTableName;

		int posCONSTRAINT = 0;
		int posFOREIGN = 0;
		int posKEY = 0;
		int posREFERENCES = 0;
		/* TODO см. коммент ниже
		int posON = 0;
		int posUPDATE = 0;
		int posDELETE = 0;
		int posCASCADEdel = 0;
		int posCASCADEupd = 0;
		*/
		for (var i = 3; i < piecesLower.Length; i++)
		{
			if (posCONSTRAINT == 0 && piecesLower[i] == Const.CONSTRAINT)
			{
				posCONSTRAINT = i; continue;
			}
			if (posFOREIGN == 0 && piecesLower[i] == Const.FOREIGN)
			{
				posFOREIGN = i; continue;
			}
			if (posFOREIGN > 0 && posKEY == 0 && piecesLower[i].StartsWith(Const.KEY))
			{
				posKEY = i; continue;
			}
			if (posREFERENCES == 0 && piecesLower[i] == Const.REFERENCES)
			{
				posREFERENCES = i; continue;
			}
			/* TODO см. коммент ниже
			if (posON == 0 && piecesLower[i] == Const.ON)
			{
				posON = i; continue;
			}
			if (posUPDATE == 0 && piecesLower[i] == Const.UPDATE)
			{
				posUPDATE = i; continue;
			}
			if (posDELETE == 0 && piecesLower[i] == Const.DELETE)
			{
				posDELETE = i; continue;
			}
			if (piecesLower[i] == Const.CASCADE)
			{
				if (posDELETE != 0)
					posCASCADEdel = i;
				else if (posUPDATE != 0)
					posCASCADEupd = i;
			}
			*/
		}
		if (piecesLower.Length > posCONSTRAINT)
			Name = DtElement.ClearBraces(pieces[posCONSTRAINT + 1]);
		if (piecesLower.Length > posREFERENCES)
			ToTableName = DtElement.ClearBraces(pieces[posREFERENCES + 1]);

		/* TODO надо ли определять поля и разбирать ALTER TABLE с внешним ключом полностью? наверное, нет
		OnUpdate = posON != 0 && posUPDATE != 0;
		OnUpdateCascade = OnUpdate && posCASCADEupd != 0;
		OnDelete = posON != 0 && posDELETE != 0;
		OnDeleteCascade = OnDelete && posCASCADEdel != 0;

		// определение FromField 
		if (posKEY > 0)
		{

			FromField = null;
		}

		// определение ToField 
		ToField = null;
		*/
	}
}
