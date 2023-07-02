using System.Reflection;
using System.Text;

namespace PgConvert.Element;

public class ElTable : DtElement
{
	DtField[] Fields { get; set; }

	public override DtField[] GetChild
		=> Fields;

	public ElTable()
	{
		SelectFor = ElmType.Table;
	}

	internal override void Parse()
	{
		if (ElmOperation.Create != Operation)
			return;

		var allStr = LinesAsString;

		int? indexFieldsOpen = null;
		int lengthFieldsClose = 0, innerParentheses = 0;
		List<int> commaIndexList = new();
		for (int i = 0; i < allStr.Length; i++)
		{
			switch (allStr[i])
			{
				case '(':
					if (!indexFieldsOpen.HasValue)
						indexFieldsOpen = i + 1;
					else
						innerParentheses++;
					continue;

				case ')':
					if (innerParentheses > 0)
					{
						innerParentheses--;
						continue;
					}
					if (!indexFieldsOpen.HasValue)
						return;
					lengthFieldsClose = i - indexFieldsOpen.Value;
					continue;

				case ',':
					if (innerParentheses <= 0)
						commaIndexList.Add(i);
					continue;
			}
		}
		if (!indexFieldsOpen.HasValue || 0 == lengthFieldsClose)
			return;

		var fieldsOpenIndex = indexFieldsOpen.Value;
		List<string> fieldsDraft = new();
		if (commaIndexList.Count > 0)
		{
			fieldsDraft.Add(allStr[fieldsOpenIndex..commaIndexList[0]].Trim());
			for (int i = 0; i < commaIndexList.Count - 1; i++)
			{
				fieldsDraft.Add(
					allStr.Substring(
						commaIndexList[i] + 1,
						commaIndexList[i + 1] - commaIndexList[i] - 1
					).Trim()
				);
			}
		}

	}

}
