using PgConvert.Element;

namespace PgConvert;

public class BaseSelectable
{
	public ElmType SelectFor { get; private protected set; } = ElmType.None;
}
