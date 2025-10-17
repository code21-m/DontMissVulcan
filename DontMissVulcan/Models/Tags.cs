using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontMissVulcan.Models
{
	public class Tags
	{
		public required ImmutableList<string> Rarity { get; init; }
		public required ImmutableList<string> Job { get; init; }
		public required ImmutableList<string> Position { get; init; }
		public required ImmutableList<string> Other { get; init; }
	}
}
