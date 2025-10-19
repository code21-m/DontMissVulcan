using System.Collections.Immutable;

namespace DontMissVulcan.Models
{
	internal class Operator
	{
		public required string Name { get; init; }
		public required int Rarity { get; init; }
		public required Tag Class { get; init; }
		public required Tag Position { get; init; }
		public required ImmutableList<Tag> Specializations { get; init; }
	}
}
