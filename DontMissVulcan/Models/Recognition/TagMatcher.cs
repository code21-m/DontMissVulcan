using DontMissVulcan.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace DontMissVulcan.Models.Recognition
{
	internal class TagMatcher(GameData gameData)
	{
		private readonly GameData _gameData = gameData;

		public HashSet<Tag> MatchTags(IEnumerable<string> candidates)
		{
			var tags = candidates
				.Where(_gameData.DisplayNameToTag.ContainsKey)
				.Select(text => _gameData.DisplayNameToTag[text])
				.ToHashSet();
			return tags;
		}
	}
}
