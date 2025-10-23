using DontMissVulcan.Models.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DontMissVulcan.Models.Recruitment.Matching
{
	public static class MatchClassifier
	{
		public static MatchClassification ClassifyMatches(IEnumerable<Match> matches)
		{
			var sixStarsOperators = new List<Match>();
			var fiveStarsOrHigherOperators = new List<Match>();
			var fourStarsOrHigherOperators = new List<Match>();
			var robots = new List<Match>();
			foreach (var match in matches)
			{
				if (match.Operators.Count == 0)
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
			return new MatchClassification(sixStarsOperators, fiveStarsOrHigherOperators, fourStarsOrHigherOperators, robots);
		}
	}
}
