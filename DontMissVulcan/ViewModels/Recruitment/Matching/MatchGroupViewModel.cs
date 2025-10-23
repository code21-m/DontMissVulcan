using DontMissVulcan.Models.Recruitment.Domain;
using DontMissVulcan.Models.Recruitment.Matching;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DontMissVulcan.ViewModels.Recruitment.Matching
{
	internal class MatchGroupViewModel(GameData gameData, string name, IEnumerable<Match> matches)
	{
		public string Name { get; } = name;

		public ObservableCollection<MatchItemViewModel> MatchItems { get; } = new(matches.OrderByDescending(match => match.Tags.Count).Select(match => new MatchItemViewModel(gameData, match)));
	}
}
