using DontMissVulcan.Models.Domain;
using System.Collections.Generic;

namespace DontMissVulcan.Models.Matching
{
	public sealed record OperatorMatch(IEnumerable<Tag> Tags, IEnumerable<Operator> Operators);
}
