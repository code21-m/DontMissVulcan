using DontMissVulcan.Models.Recruitment.Domain;
using System.Collections.Generic;

namespace DontMissVulcan.Models.Recruitment.Matching
{
	public sealed record OperatorMatch(IReadOnlyCollection<Tag> Tags, IReadOnlyCollection<Operator> Operators);
}
