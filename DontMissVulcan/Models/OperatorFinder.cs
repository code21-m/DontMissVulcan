using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontMissVulcan.Models
{
	internal class OperatorFinder(GameData gameData)
	{
		private readonly GameData gameData = gameData;

		public IEnumerable<(IEnumerable<Tag> tags, IEnumerable<Operator> operators)> EnumerateOperatorsForTagsCombinations(IEnumerable<Tag> appearedTags)
		{
			const int maximumSelectionsNumber = 3;
			for (var r = 1; r <= maximumSelectionsNumber; r++)
			{
				foreach(var selectedTags in appearedTags.EnumerateCombinations(r))
				{
					var result= GetOperatorsByTags(selectedTags);
					yield return (selectedTags, result);
				}
			}
		}

		public IEnumerable<Operator> GetOperatorsByTags(IEnumerable<Tag> selectedTags)
		{
			var employableOperators = gameData.Operators.AsEnumerable();
			if (!selectedTags.Contains(Tag.SeniorOperator))
			{
				employableOperators = employableOperators.Where(o => o.Rarity <= 5);
			}
			foreach (var tag in selectedTags)
			{
				if (TagCategory.Qualification.Contains(tag))
				{
					employableOperators = employableOperators.Where(o => o.Rarity == QualificationToRarity(tag));
				}
				else if (TagCategory.Class.Contains(tag))
				{
					employableOperators = employableOperators.Where(o => o.Class == tag);
				}
				else if (TagCategory.Position.Contains(tag))
				{
					employableOperators = employableOperators.Where(o => o.Position == tag);
				}
				else if (TagCategory.Specialization.Contains(tag))
				{
					employableOperators = employableOperators.Where(o => o.Specializations.Contains(tag));
				}
				else
				{
					throw new ArgumentException($"タグ '{tag}' は有効なタグではありません。", nameof(selectedTags));
				}
			}
			return employableOperators;
		}

		private static int QualificationToRarity(Tag qualificationTag)
		{
			if (!TagCategory.Qualification.Contains(qualificationTag))
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
