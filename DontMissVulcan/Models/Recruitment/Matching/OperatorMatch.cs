using DontMissVulcan.Models.Recruitment.Domain;
using System.Collections.Generic;

namespace DontMissVulcan.Models.Recruitment.Matching
{
	public sealed record OperatorMatch(IEnumerable<Tag> Tags, IEnumerable<Operator> Operators);
}
