using DontMissVulcan.Models.Extensions;
using DontMissVulcan.Models.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DontMissVulcan.Models.Recruitment.Matching
{
	public class MatchFinder(GameData gameData)
	{
		private readonly GameData _gameData = gameData;

		public IReadOnlyCollection<Match> FindAllMathes(IEnumerable<Tag> appearedTags)
		{
			var matches = new List<Match>();
			const int maxSelectable = 3;
			for (var selectedTagCount = 1; selectedTagCount <= maxSelectable; selectedTagCount++)
			{
				foreach (var selectedTags in appearedTags.EnumerateCombinations(selectedTagCount))
				{
					var matchingOperators = FindOperators(selectedTags);
					matches.Add(new Match([.. selectedTags], matchingOperators));
				}
			}
			return matches;
		}

		private IReadOnlyCollection<Operator> FindOperators(IEnumerable<Tag> selectedTags)
		{
			var matchingOperators = _gameData.Operators.AsEnumerable();
			if (!selectedTags.Contains(Tag.SeniorOperator))
			{
				matchingOperators = matchingOperators.Where(o => o.Rarity <= 5);
			}
			foreach (var tag in selectedTags)
			{
				if (TagCategories.QualificationTags.Contains(tag))
				{
					matchingOperators = matchingOperators.Where(o => o.Rarity == QualificationToRarity(tag));
				}
				else if (TagCategories.ClassTags.Contains(tag))
				{
					matchingOperators = matchingOperators.Where(o => o.Class == tag);
				}
				else if (TagCategories.PositionTags.Contains(tag))
				{
					matchingOperators = matchingOperators.Where(o => o.Position == tag);
				}
				else
				{
					matchingOperators = matchingOperators.Where(o => o.Specializations.Contains(tag));
				}
			}
			return [.. matchingOperators];
		}

		private static int QualificationToRarity(Tag qualificationTag)
		{
			if (!TagCategories.QualificationTags.Contains(qualificationTag))
			{
				throw new ArgumentException($"'{qualificationTag}' is not a qualification tag", nameof(qualificationTag));
			}

			return qualificationTag switch
			{
				Tag.SeniorOperator => 6,
				Tag.TopOperator => 5,
				_ => 0,
			};
		}
	}
}
