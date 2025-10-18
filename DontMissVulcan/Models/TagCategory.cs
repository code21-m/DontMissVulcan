using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontMissVulcan.Models
{
	internal static class TagCategory
	{
		public static readonly ImmutableHashSet<Tag> Qualification = [
			Tag.SeniorOperator,
			Tag.TopOperator
		];

		public static readonly ImmutableHashSet<Tag> Class = [
			Tag.Vanguard,
			Tag.Guard,
			Tag.Defender,
			Tag.Sniper,
			Tag.Caster,
			Tag.Medic,
			Tag.Supporter,
			Tag.Specialist
		];

		public static readonly ImmutableHashSet<Tag> Position = [
			Tag.Melee,
			Tag.Ranged
		];

		public static readonly ImmutableHashSet<Tag> Specialization =
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
