using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontMissVulcan.Models
{
	internal class OperatorSearcher(GameData gameData)
	{
		private readonly GameData gameData = gameData;

		public List<Operator> SearchEmployableOperatorsFromTags(List<string> tags)
		{
			var employableOperators = gameData.Operators.AsEnumerable();
			if (tags.Contains("上級エリート"))
			{
				employableOperators = employableOperators.Where(o => o.Rarity == 6);
				tags.Remove("上級エリート");
			}
			else if (tags.Contains("エリート"))
			{
				employableOperators = employableOperators.Where(o => o.Rarity == 5);
				tags.Remove("エリート");
			}
			else
			{
				employableOperators = employableOperators.Where(o => o.Rarity <= 5);
			}
				foreach (var tag in tags)
				{
					if (gameData.Tags.Job.Contains(tag))
					{
						employableOperators = employableOperators.Where(o => o.Job == JobTagToJob(tag));
					}
					else if (gameData.Tags.Position.Contains(tag))
					{
						employableOperators = employableOperators.Where(o => o.Position == tag);
					}
					else if (gameData.Tags.Other.Contains(tag))
					{
						employableOperators = employableOperators.Where(o => o.Tags.Contains(tag));
					}
					else
					{
						throw new ArgumentException($"タグ '{tag}' は有効なタグではありません。", nameof(tags));
					}
				}
			return [.. employableOperators];
		}

		private string JobTagToJob(string jobTag)
		{
			if (!gameData.Tags.Job.Contains(jobTag))
			{
				throw new ArgumentException($"jobTag '{jobTag}' は有効なジョブタグではありません。", nameof(jobTag));
			}
			return jobTag[..2];
		}
	}
}
