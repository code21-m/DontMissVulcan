using System.Collections.Generic;

namespace DontMissVulcan.Models.Recruitment.Data.Dto
{
	/// <summary>
	/// オペレーター情報読み込み用のDTO
	/// </summary>
	internal class OperatorsDto
	{
		/// <summary>
		/// オペレーター情報
		/// </summary>
		public List<OperatorDto>? Operators { get; set; }
	}
}
