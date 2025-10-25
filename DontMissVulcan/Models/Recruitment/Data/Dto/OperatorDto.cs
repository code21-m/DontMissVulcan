using System.Collections.Generic;

namespace DontMissVulcan.Models.Recruitment.Data.Dto
{
	/// <summary>
	/// オペレーター情報読み込み用のDTO
	/// </summary>
	internal class OperatorDto
	{
		/// <summary>
		/// 名前
		/// </summary>
		public string? Name { get; set; }

		/// <summary>
		/// レアリティ
		/// </summary>
		public int Rarity { get; set; }

		/// <summary>
		/// 職業
		/// </summary>
		public string? Class { get; set; }

		/// <summary>
		/// ポジション
		/// </summary>
		public string? Position { get; set; }

		/// <summary>
		/// 専門
		/// </summary>
		public List<string>? Specializations { get; set; }
	}
}
