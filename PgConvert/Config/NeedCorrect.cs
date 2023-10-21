using PgConvert.Element;

namespace PgConvert.Config
{
#pragma warning disable S2365 // Properties should not make collection or array copies

	/// <summary>
	/// Элемент, требующий доработки
	/// </summary>
	public class NeedCorrect
	{
		public NeedCorrect(DtElement element) =>
			Element = element;

		private DtElement Element { get; }

		public int Id =>
			Element.Id;

		public string[] Lines =>
			 Element.ElementType switch
			 {
				 ElmType.Procedure => ((ElProcedure)Element).LinesPg,
				 ElmType.Trigger => Element.Lines,
				 ElmType.Table => GeneratedFields,
				 _ => Array.Empty<string>()
			 };

		private string[] GeneratedFields =>
			((ElTable)Element).Fields
			.Where(x => x.IsGenerated)
			.Select(x => x.NeedCorrect)
			.ToArray();

		//public bool IsNeedCorrect =>
		//	element.IsNeedCorrect;
		//public ElmType ElementType =>
		//	element.ElementType;
	}
}
