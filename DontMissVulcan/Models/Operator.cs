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
		public required string Job { get; init; }
		public required string Position { get; init; }
		public required ImmutableList<string> Tags { get; init; }
	}
}
