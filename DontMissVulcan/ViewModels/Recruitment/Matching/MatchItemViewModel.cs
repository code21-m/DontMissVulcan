using DontMissVulcan.Models.Recruitment.Domain;
using DontMissVulcan.Models.Recruitment.Matching;
using System.Linq;

namespace DontMissVulcan.ViewModels.Recruitment.Matching
{
	/// <summary>
	/// マッチごとの表示のViewModel
	/// </summary>
	/// <param name="gameData">ゲームデータ</param>
	/// <param name="match">マッチ</param>
	internal class MatchItemViewModel(GameData gameData, Match match)
	{
		/// <summary>
		/// タグの表示文字列
		/// </summary>
		public string TagsDisplay { get; } = string.Join(" ", match.Tags.Select(tag => gameData.TagToDisplayName[tag]));

		/// <summary>
		/// オペレータの表示文字列
		/// </summary>
		public string OperatorsDisplay { get; } = string.Join(" ", match.Operators.OrderByDescending(_operator => _operator.Rarity).Select(_operator => _operator.Name));
	}
}
