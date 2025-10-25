using DontMissVulcan.Models.Recruitment.Domain;
using DontMissVulcan.Models.Recruitment.Matching;
using System.Collections.ObjectModel;

namespace DontMissVulcan.ViewModels.Recruitment.Matching
{
	/// <summary>
	/// マッチング結果表示のViewModel
	/// </summary>
	/// <param name="gameData">ゲームデータ</param>
	internal class MatchResultsViewModel(GameData gameData)
	{
		/// <summary>
		/// 分類されたマッチング結果表示のViewModel
		/// </summary>
		public ObservableCollection<MatchGroupViewModel> MatchGroups = [];

		/// <summary>
		/// ゲームデータ
		/// </summary>
		private GameData _gameData = gameData;

		/// <summary>
		/// マッチング結果を設定する。
		/// </summary>
		/// <param name="matchClassification">マッチング結果</param>
		public void SetResults(MatchClassification matchClassification)
		{
			MatchGroups.Clear();
			if (matchClassification.SixStarsOperators.Count != 0)
			{
				MatchGroups.Add(new(_gameData, "星6", matchClassification.SixStarsOperators));
			}
			if (matchClassification.FiveStarsOperators.Count != 0)
			{
				MatchGroups.Add(new(_gameData, "星5", matchClassification.FiveStarsOperators));
			}
			if (matchClassification.FourStarsOrHigherOperators.Count != 0)
			{
				MatchGroups.Add(new(_gameData, "星4以上", matchClassification.FourStarsOrHigherOperators));
			}
			if (matchClassification.Robots.Count != 0)
			{
				MatchGroups.Add(new(_gameData, "ロボット", matchClassification.Robots));
			}
		}
	}
}
