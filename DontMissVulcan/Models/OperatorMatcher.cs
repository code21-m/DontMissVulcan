using System;
using System.Collections.Generic;
using System.Linq;

namespace DontMissVulcan.Models
{
	internal class OperatorMatcher(GameData gameData)
	{
		private readonly GameData _gameData = gameData;

		public IEnumerable<(IEnumerable<Tag> tags, IEnumerable<Operator> operators)> EnumerateMatchingOperatorsForTagCombinations(IEnumerable<Tag> appearedTags)
		{
			const int MaxSelectionCount = 3;
			for (var size = 1; size <= MaxSelectionCount; size++)
			{
				foreach (var selectedTags in appearedTags.EnumerateCombinations(size))
				{
					var matchingOperators = GetMatchingOperators(selectedTags);
					yield return (selectedTags, matchingOperators);
				}
			}
		}

		public IEnumerable<Operator> GetMatchingOperators(IEnumerable<Tag> selectedTags)
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
				else if (TagCategories.SpecializationTags.Contains(tag))
				{
					matchingOperators = matchingOperators.Where(o => o.Specializations.Contains(tag));
				}
				else
				{
					throw new ArgumentException($"タグ '{tag}' は有効なタグではありません。", nameof(selectedTags));
				}
			}
			return matchingOperators;
		}

		private static int QualificationToRarity(Tag qualificationTag)
		{
			if (!TagCategories.QualificationTags.Contains(qualificationTag))
			{
				throw new ArgumentException($"qualificationTag '{qualificationTag}' は有効なレア度タグではありません。", nameof(qualificationTag));
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
