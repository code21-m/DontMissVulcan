using DontMissVulcan.Models.Recruitment.Domain;
using DontMissVulcan.Models.Recruitment.Matching;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DontMissVulcan.ViewModels.Recruitment.Matching
{
	/// <summary>
	/// 分類されたマッチング結果表示のViewModel
	/// </summary>
	/// <param name="gameData">ゲームデータ</param>
	/// <param name="name">分類名</param>
	/// <param name="matches">マッチ</param>
	internal class MatchGroupViewModel(GameData gameData, string name, IEnumerable<Match> matches)
	{
		/// <summary>
		/// 分類名
		/// </summary>
		public string Name { get; } = name;

		/// <summary>
		/// マッチごとの表示のViewModel
		/// </summary>
		public ObservableCollection<MatchItemViewModel> MatchItems { get; } = new(matches.OrderByDescending(match => match.Tags.Count).Select(match => new MatchItemViewModel(gameData, match)));
	}
}
