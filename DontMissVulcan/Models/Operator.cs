using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
