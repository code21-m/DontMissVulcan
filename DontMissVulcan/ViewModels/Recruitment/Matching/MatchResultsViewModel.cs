using DontMissVulcan.Models.Recruitment.Domain;
using DontMissVulcan.Models.Recruitment.Matching;
using System.Collections.ObjectModel;

namespace DontMissVulcan.ViewModels.Recruitment.Matching
{
	internal class MatchResultsViewModel(GameData gameData)
	{
		public ObservableCollection<MatchGroupViewModel> MatchGroups = [];

		private GameData _gameData = gameData;

		public void SetResults(MatchClassification matchClassification)
		{
			MatchGroups.Clear();
			if (matchClassification.SixStarsOperators.Count != 0)
			{
				MatchGroups.Add(new MatchGroupViewModel(_gameData, "星6", matchClassification.SixStarsOperators));
			}
			if (matchClassification.FiveStarsOperators.Count != 0)
			{
				MatchGroups.Add(new MatchGroupViewModel(_gameData, "星5", matchClassification.FiveStarsOperators));
			}
			if (matchClassification.FourStarsOrHigherOperators.Count != 0)
			{
				MatchGroups.Add(new MatchGroupViewModel(_gameData, "星4以上", matchClassification.FourStarsOrHigherOperators));
			}
			if (matchClassification.Robots.Count != 0)
			{
				MatchGroups.Add(new MatchGroupViewModel(_gameData, "ロボット", matchClassification.Robots));
			}
		}
	}
}
