using DontMissVulcan.Models.Recruitment.Domain;
using DontMissVulcan.Models.Recruitment.Matching;
using System.Linq;

namespace DontMissVulcan.ViewModels.Recruitment.Matching
{
	internal class MatchItemViewModel(GameData gameData, Match match)
	{
		public string TagsDisplay { get; } = string.Join(" ", match.Tags.Select(tag => gameData.TagToDisplayName[tag]));

		public string OperatorsDisplay { get; } = string.Join(" ", match.Operators.OrderByDescending(_operator => _operator.Rarity).Select(_operator => _operator.Name));
	}
}
