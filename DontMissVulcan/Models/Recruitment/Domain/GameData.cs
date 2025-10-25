using System.Collections.Generic;
using System.Collections.Immutable;

namespace DontMissVulcan.Models.Recruitment.Domain
{
	/// <summary>
	/// ゲームデータを格納します。
	/// </summary>
	/// <param name="tagToDisplayName">タグを表示名に変換する辞書</param>
	/// <param name="displayNameToTag">表示名をタグに変換する辞書</param>
	/// <param name="operators">オペレーター情報</param>
	public class GameData(
		IDictionary<Tag, string> tagToDisplayName,
		IDictionary<string, Tag> displayNameToTag,
		IEnumerable<Operator> operators)
	{
		private readonly ImmutableDictionary<Tag, string> _tagToDisplayName = tagToDisplayName switch
		{
			ImmutableDictionary<Tag, string> im => im,
			var r => ImmutableDictionary.CreateRange(r)
		};
		private readonly ImmutableDictionary<string, Tag> _displayNameToTag = displayNameToTag switch
		{
			ImmutableDictionary<string, Tag> im => im,
			var r => ImmutableDictionary.CreateRange(r)
		};
		private readonly ImmutableList<Operator> _operators = operators switch
		{
			ImmutableList<Operator> im => im,
			var r => [.. r]
		};

		/// <summary>
		/// タグを表示名に変換する辞書
		/// </summary>
		public IReadOnlyDictionary<Tag, string> TagToDisplayName => _tagToDisplayName;

		/// <summary>
		/// 表示名をタグに変換する辞書
		/// </summary>
		public IReadOnlyDictionary<string, Tag> DisplayNameToTag => _displayNameToTag;

		/// <summary>
		/// オペレーター情報
		/// </summary>
		public IReadOnlyList<Operator> Operators => _operators;
	}
}
