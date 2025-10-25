using System.Collections.Generic;

namespace DontMissVulcan.Models.Recruitment.Matching
{
	/// <summary>
	/// 分類されたマッチを表します。
	/// </summary>
	/// <param name="SixStarsOperators">星6</param>
	/// <param name="FiveStarsOperators">星5</param>
	/// <param name="FourStarsOrHigherOperators">星4以上</param>
	/// <param name="Robots">ロボット</param>
	public sealed record MatchClassification(
			IReadOnlyCollection<Match> SixStarsOperators,
			IReadOnlyCollection<Match> FiveStarsOperators,
			IReadOnlyCollection<Match> FourStarsOrHigherOperators,
			IReadOnlyCollection<Match> Robots
		);
}
