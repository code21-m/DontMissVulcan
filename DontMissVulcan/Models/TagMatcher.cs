using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;

namespace DontMissVulcan.Models
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
