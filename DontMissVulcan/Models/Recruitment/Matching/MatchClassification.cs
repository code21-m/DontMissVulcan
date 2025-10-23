using System.Collections.Generic;

namespace DontMissVulcan.Models.Recruitment.Matching
{
	public sealed record MatchClassification(
			IReadOnlyCollection<Match> SixStarsOperators,
			IReadOnlyCollection<Match> FiveStarsOrHigherOperators,
			IReadOnlyCollection<Match> FourStarsOrHigherOperators,
			IReadOnlyCollection<Match> Robots
		);
}
