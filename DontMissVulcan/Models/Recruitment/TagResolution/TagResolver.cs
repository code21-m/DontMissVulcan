using DontMissVulcan.Models.Recruitment.Domain;
using System.Collections.Generic;
using System.Linq;

namespace DontMissVulcan.Models.Recruitment.TagResolution
{
	/// <summary>
	/// 文字列をタグに変換します。
	/// </summary>
	/// <param name="gameData">ゲームデータ</param>
	public class TagResolver(GameData gameData)
	{
		/// <summary>
		/// ゲームデータ
		/// </summary>
		private readonly GameData _gameData = gameData;

		/// <summary>
		/// 候補となる文字列に対し、タグへの変換を試みます。
		/// </summary>
		/// <param name="candidates">候補文字列</param>
		/// <returns>変換できたタグ</returns>
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
