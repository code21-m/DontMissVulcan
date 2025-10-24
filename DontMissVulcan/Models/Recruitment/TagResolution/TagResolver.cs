using DontMissVulcan.Models.Recruitment.Domain;
using System.Collections.Generic;
using System.Linq;

namespace DontMissVulcan.Models.Recruitment.TagResolution
{
	public class TagResolver(GameData gameData)
	{
		private readonly GameData _gameData = gameData;

		public IReadOnlyList<Tag> ResolveTags(IEnumerable<string> candidates)
		{
			var tags = candidates
				.Where(_gameData.DisplayNameToTag.ContainsKey)
				.Select(text => _gameData.DisplayNameToTag[text])
				.ToList();
			return tags;
		}
	}
}
