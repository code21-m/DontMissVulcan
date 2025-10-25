using System.Collections.Generic;

namespace DontMissVulcan.Models.Recruitment.Domain
{
	/// <summary>
	/// オペレーター情報を格納します。
	/// </summary>
	public class Operator
	{
		/// <summary>
		/// 名前
		/// </summary>
		public required string Name { get; init; }

		/// <summary>
		/// レアリティ
		/// </summary>
		public required int Rarity { get; init; }

		/// <summary>
		/// 職業
		/// </summary>
		public required Tag Class { get; init; }

		/// <summary>
		/// ポジション
		/// </summary>
		public required Tag Position { get; init; }

		/// <summary>
		/// 専門
		/// </summary>
		public required IReadOnlySet<Tag> Specializations { get; init; }
	}
}
