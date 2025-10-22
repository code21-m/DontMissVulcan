using System.Collections.Generic;

namespace DontMissVulcan.Models.Recruitment.Matching
{
	public sealed record OperatorMatchClassification(
			IReadOnlyCollection<OperatorMatch> SixStarsOperators,
			IReadOnlyCollection<OperatorMatch> FiveStarsOrHigherOperators,
			IReadOnlyCollection<OperatorMatch> FourStarsOrHigherOperators,
			IReadOnlyCollection<OperatorMatch> Robots
		);
}
