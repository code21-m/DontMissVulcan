using DontMissVulcan.Models.Extensions;
using DontMissVulcan.Models.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DontMissVulcan.Models.Recruitment.Matching
{
	/// <summary>
	/// マッチを探します。
	/// </summary>
	/// <param name="gameData">ゲームデータ</param>
	public class MatchFinder(GameData gameData)
	{
		/// <summary>
		/// ゲームデータ
		/// </summary>
		private readonly GameData _gameData = gameData;

		/// <summary>
		/// 出現したタグから全てのマッチを検索します。
		/// </summary>
		/// <param name="appearedTags">出現したタグ</param>
		/// <returns>マッチ</returns>
		public IReadOnlyCollection<Match> FindAllMathes(IEnumerable<Tag> appearedTags)
		{
			var matches = new List<Match>();
			const int maxSelectable = 3;
			for (var selectedTagCount = 1; selectedTagCount <= maxSelectable; selectedTagCount++)
			{
				foreach (var selectedTags in appearedTags.EnumerateCombinations(selectedTagCount))
				{
					var matchingOperators = FindOperators(selectedTags);
					// 対応するオペレーターが一人以上いる場合のみリストに加える。
					if (matchingOperators.Count > 0)
					{
						matches.Add(new Match([.. selectedTags], matchingOperators));
					}
				}
			}
			return matches;
		}

		/// <summary>
		/// 指定されたタグにマッチするオペレーターを検索します。
		/// </summary>
		/// <param name="selectedTags">タグ</param>
		/// <returns>タグにマッチするオペレーター</returns>
		private IReadOnlyCollection<Operator> FindOperators(IEnumerable<Tag> selectedTags)
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
				else
				{
					matchingOperators = matchingOperators.Where(o => o.Specializations.Contains(tag));
				}
			}
			return [.. matchingOperators];
		}

		/// <summary>
		/// レアタグをレアリティの数値に変換します。
		/// </summary>
		/// <param name="qualificationTag">レアタグ</param>
		/// <returns>レアリティの数値</returns>
		/// <exception cref="ArgumentException">レアタグでないタグが指定された場合にスローされます。</exception>
		private static int QualificationToRarity(Tag qualificationTag)
		{
			if (!TagCategories.QualificationTags.Contains(qualificationTag))
			{
				throw new ArgumentException($"'{qualificationTag}' is not a qualification tag", nameof(qualificationTag));
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
