using System.Collections.Immutable;
using System.Collections.Generic;

namespace DontMissVulcan.Models.Domain
{
	public static class TagCategories
	{
		public static readonly IReadOnlySet<Tag> QualificationTags =
			ImmutableHashSet.Create(Tag.SeniorOperator, Tag.TopOperator);

		public static readonly IReadOnlySet<Tag> ClassTags =
			ImmutableHashSet.Create(
				Tag.Vanguard,
				Tag.Guard,
				Tag.Defender,
				Tag.Sniper,
				Tag.Caster,
				Tag.Medic,
				Tag.Supporter,
				Tag.Specialist
			);

		public static readonly IReadOnlySet<Tag> PositionTags =
			ImmutableHashSet.Create(Tag.Melee, Tag.Ranged);

		public static readonly IReadOnlySet<Tag> SpecializationTags =
			ImmutableHashSet.Create(
				Tag.AoE,
				Tag.CrowdControl,
				Tag.DPS,
				Tag.DPRecovery,
				Tag.Debuff,
				Tag.FastRedeploy,
				Tag.Defense,
				Tag.Elemental,
				Tag.Healing,
				Tag.Nuker,
				Tag.Shift,
				Tag.Slow,
				Tag.Summon,
				Tag.Support,
				Tag.Survival,
				Tag.Starter,
				Tag.Robot
			);
	}
}
