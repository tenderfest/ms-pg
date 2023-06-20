using System.Text;
using PgConvert.Config;

namespace PgConvert
{
	public abstract class DtElement : BaseSelectable
	{
		#region конструктор
		private protected DtElement() { }
		//private protected DtElement(
		//	string operation,
		//	string[] firstLineWords,
		//	string firstLine,
		//	ElmType type,
		//	List<string> lines,
		//	List<string> comment)
		//{
		//	FirstLineWords = firstLineWords;
		//	FirstLine = firstLine;
		//	Type = type;
		//	Lines = lines.ToArray();
		//	CommentLines = comment.ToArray();

		//	Operation = ElementOperation.GetOperation(operation);

		//	tmpCount = 1;
		//}
		#endregion

		private protected string FirstLine { get; private set; }
		private protected string[] Lines { get; private set; }
		private protected string[] CommentLines { get; private set; }

		private protected ElmOperation Operation { get; set; }
		private protected string[] FirstLineWords { get; set; }
		private int tmpCount { get; set; }

		private protected string name;
		protected virtual string Name
		{
			get
			{
				if (null == name)
				{
					name = ClearBraces(FirstLineWords.Length < 3
						? "FirstLineWords.Length < 3"
						: FirstLineWords[2]);
				}
				return name;
			}
		}

		private protected static string ClearBraces(string draftName) =>
			draftName
				.Replace("(", string.Empty)
				.Replace("[", string.Empty)
				.Replace("]", string.Empty);

		internal void IncremenCount() =>
			tmpCount++;

		/// <summary>
		/// Определение типа элемента и создание экземпляров элементов
		/// </summary>
		internal static DtElement GetElement(List<string> inLines, List<string> comment, ConvertMsToPgCfg config)
		{
			var firstLine = inLines.First();
			var firstLineWords = firstLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			var operation = firstLineWords[0].ToLower();
			var elementKey = firstLineWords.Length > 1 ? firstLineWords[1] : string.Empty;
			elementKey = elementKey.ToLower();

			if (null != config.SkipOperation && config.SkipOperation.Contains(operation))
				return default;
			if (null != config.SkipElement && config.SkipElement.Contains(elementKey))
				return default;

			DtElement element = ElementType.GetType(elementKey) switch
			{
				ElmType.Database => new ElDatabase(),
				ElmType.Index => new ElIndex(),
				ElmType.Procedure => new ElProcedure(),
				ElmType.Trigger => new ElTrigger(),
				ElmType.Table => new ElTable(),
				ElmType.View => new ElView(),
				_ => new DtUnknown(),
			};
			element.SetFields(operation, firstLine, firstLineWords, inLines.ToArray(), comment.ToArray());

			return element;
		}

		private void SetFields(string operation, string firstLine, string[] firstLineWords, string[] inLines, string[] comment)
		{
			Operation = ElementOperation.GetOperation(operation);
			FirstLine = firstLine;
			FirstLineWords = firstLineWords;
			Lines = inLines;
			CommentLines = comment;
			tmpCount = 1;
		}

		public override bool Equals(object obj)
		{
			if (obj is not DtElement x)
				return false;

			if (FirstLine != x.FirstLine)
				return false;

			if (null == Lines && null != x.Lines ||
				null != Lines && null == x.Lines)
				return false;

			if (null != Lines || null != x.Lines)
			{
				if (Lines.Length != x.Lines.Length)
					return false;
				for (int i = 0; i < Lines.Length; i++)
					if (Lines[i] != x.Lines[i])
						return false;
			}

			if (null == CommentLines && null != x.CommentLines ||
				null != CommentLines && null == x.CommentLines)
				return false;

			if (null != CommentLines || null != x.CommentLines)
			{
				if (CommentLines.Length != x.CommentLines.Length)
					return false;
				for (int i = 0; i < CommentLines.Length; i++)
					if (CommentLines[i] != x.CommentLines[i])
						return false;
			}

			return true;
		}

		public string GetEmenenlContent
		{
			get
			{
				StringBuilder sb = new();
				if (null != CommentLines)
					foreach (var str in CommentLines)
						sb.AppendLine(str);

				if (null != Lines)
					foreach (var str in Lines)
						sb.AppendLine(str);

				return sb.ToString();
			}
		}

		public virtual DtField[] GetChild => Array.Empty<DtField>();

		public override int GetHashCode() =>
			HashCode;
		public int HashCode
		{
			get
			{
				var hash = FirstLine.GetHashCode();
				if (Lines != null)
					foreach (var str in Lines)
						hash ^= str.GetHashCode();
				return hash;
			}
		}

		public override string ToString() =>
			$"{ElementOperation.GetOperationSign(Operation)} {SelectFor}: {Name}";
	}
}