using System.Collections.Generic;
using System.Collections.Immutable;

namespace DontMissVulcan.Models.Recruitment.Domain
{
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

		public IReadOnlyDictionary<Tag, string> TagToDisplayName => _tagToDisplayName;
		public IReadOnlyDictionary<string, Tag> DisplayNameToTag => _displayNameToTag;
		public IReadOnlyList<Operator> Operators => _operators;
	}
}
