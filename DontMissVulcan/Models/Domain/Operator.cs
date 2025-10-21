using System.Collections.Generic;

namespace DontMissVulcan.Models.Domain
{
	public class Operator
	{
		public required string Name { get; init; }
		public required int Rarity { get; init; }
		public required Tag Class { get; init; }
		public required Tag Position { get; init; }
		public required IReadOnlySet<Tag> Specializations { get; init; }
	}
}
