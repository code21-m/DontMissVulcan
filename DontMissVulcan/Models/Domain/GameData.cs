using System.Collections.Generic;
using System.Collections.Immutable;

namespace DontMissVulcan.Models.Domain
{
	public class GameData(
		IReadOnlyDictionary<Tag, string> tagToDisplayName,
		IReadOnlyDictionary<string, Tag> displayNameToTag,
		IReadOnlyList<Operator> operators)
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

		// 公開プロパティは IReadOnly インターフェイスで外部に公開
		public IReadOnlyDictionary<Tag, string> TagToDisplayName => _tagToDisplayName;
		public IReadOnlyDictionary<string, Tag> DisplayNameToTag => _displayNameToTag;
		public IReadOnlyList<Operator> Operators => _operators;
	}
}
