using System;
using System.Collections.Generic;
using System.Linq;
using static DontMissVulcan.Models.OperatorMatchFinder;

namespace DontMissVulcan.Models
{
	internal static class OperatorMatchClassifier
	{
		public sealed record MatchClassification(
			IEnumerable<OperatorMatch> SixStarsOperators,
			IEnumerable<OperatorMatch> FiveStarsOrHigherOperators,
			IEnumerable<OperatorMatch> FourStarsOrHigherOperators,
			IEnumerable<OperatorMatch> Robots
		);

		public static MatchClassification ClassifyMatches(IEnumerable<OperatorMatch> matches)
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
			return new MatchClassification(sixStarsOperators, fiveStarsOrHigherOperators, fourStarsOrHigherOperators, robots);
		}
	}
}
