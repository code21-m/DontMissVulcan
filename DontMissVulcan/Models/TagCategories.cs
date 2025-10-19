using System.Collections.Immutable;

namespace DontMissVulcan.Models
{
	internal static class TagCategories
	{
		public static readonly ImmutableHashSet<Tag> QualificationTags = [
			Tag.SeniorOperator,
			Tag.TopOperator
		];

		public static readonly ImmutableHashSet<Tag> ClassTags = [
			Tag.Vanguard,
			Tag.Guard,
			Tag.Defender,
			Tag.Sniper,
			Tag.Caster,
			Tag.Medic,
			Tag.Supporter,
			Tag.Specialist
		];

		public static readonly ImmutableHashSet<Tag> PositionTags = [
			Tag.Melee,
			Tag.Ranged
		];

		public static readonly ImmutableHashSet<Tag> SpecializationTags =
		[
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
		];
	}
}
