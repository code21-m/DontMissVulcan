using System.Collections.Generic;

namespace DontMissVulcan.Models.Matching
{
	public sealed record MatchClassification(
			IEnumerable<OperatorMatch> SixStarsOperators,
			IEnumerable<OperatorMatch> FiveStarsOrHigherOperators,
			IEnumerable<OperatorMatch> FourStarsOrHigherOperators,
			IEnumerable<OperatorMatch> Robots
		);
}
