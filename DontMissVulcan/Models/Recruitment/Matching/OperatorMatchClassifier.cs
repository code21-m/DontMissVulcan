using DontMissVulcan.Models.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DontMissVulcan.Models.Recruitment.Matching
{
	public static class OperatorMatchClassifier
	{
		public static OperatorMatchClassification ClassifyMatches(IEnumerable<OperatorMatch> matches)
		{
			var sixStarsOperators = new List<OperatorMatch>();
			var fiveStarsOrHigherOperators = new List<OperatorMatch>();
			var fourStarsOrHigherOperators = new List<OperatorMatch>();
			var robots = new List<OperatorMatch>();
			foreach (var match in matches)
			{
				if (!match.Operators.Any())
				{
					continue;
				}
				else if (match.Tags.Contains(Tag.SeniorOperator))
				{
					sixStarsOperators.Add(match);
				}
				else if (match.Operators.All(_operator => _operator.Rarity == 1))
				{
					robots.Add(match);
				}
				else
				{
					var minRarity = match.Operators
						.Select(_operator => _operator.Rarity)
						.Where(rarity => rarity != 1)
						.Min();
					switch (minRarity)
					{
						case 5:
							fiveStarsOrHigherOperators.Add(match);
							break;
						case 4:
							fourStarsOrHigherOperators.Add(match);
							break;
					}
				}
			}
			return new OperatorMatchClassification(sixStarsOperators, fiveStarsOrHigherOperators, fourStarsOrHigherOperators, robots);
		}
	}
}
