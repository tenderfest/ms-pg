namespace PgConvert.Element;

public class DtForeignKey
{
	public string Name { get; set; }
	public string FromTableName { get; set; }
	public string ToTableName { get; set; }

	/* TODO см. коммент ниже
	public List<string> FromField { get; set; }
	public List<string> ToField { get; set; }
	public bool OnUpdate { get; set; }
	public bool OnUpdateCascade { get; set; }
	public bool OnDelete { get; set; }
	public bool OnDeleteCascade { get; set; }
	*/

	public DtForeignKey() { }
	public DtForeignKey(string[] piecesLower, string[] pieces, string name)
	{
		FromTableName = name;

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
