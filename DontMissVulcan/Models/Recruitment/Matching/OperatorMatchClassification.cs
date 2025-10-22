using System.Collections.Generic;

namespace DontMissVulcan.Models.Recruitment.Matching
{
	public sealed record OperatorMatchClassification(
			IEnumerable<OperatorMatch> SixStarsOperators,
			IEnumerable<OperatorMatch> FiveStarsOrHigherOperators,
			IEnumerable<OperatorMatch> FourStarsOrHigherOperators,
			IEnumerable<OperatorMatch> Robots
		);
}
