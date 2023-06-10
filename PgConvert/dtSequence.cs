namespace PgConvert
{
	internal class dtSequence
	{
		string Name { get; set; }
		long? Minvalue { get; set; }
		long? Maxvalue { get; set; }
		long? Start { get; set; }
		long? IncrementBy { get; set; }

		string GetDrop()
		{
			return $"DROP SEQUENCE IF EXISTS {Name};";
		}
	}
}
