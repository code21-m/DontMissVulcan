using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontMissVulcan.Models
{
	internal enum Tag
	{
		// Qualification
		SeniorOperator, // 上級エリート
		TopOperator,    // エリート

		// Class
		Vanguard,       // 先鋒タイプ
		Guard,          // 前衛タイプ
		Defender,       // 重装タイプ
		Sniper,         // 狙撃タイプ
		Caster,         // 術師タイプ
		Medic,          // 医療タイプ
		Supporter,      // 補助タイプ
		Specialist,     // 特殊タイプ

		// Position
		Melee,          // 近距離
		Ranged,         // 遠距離

		// Specialization
		AoE,            // 範囲攻撃
		CrowdControl,   // 牽制
		DPS,            // 火力
		DPRecovery,     // COST回復
		Debuff,         // 弱化
		FastRedeploy,   // 高速再配置
		Defense,        // 防御
		Elemental,      // 元素
		Healing,        // 治療
		Nuker,          // 爆発力
		Shift,          // 強制移動
		Slow,           // 減速
		Summon,         // 召喚
		Support,        // 支援
		Survival,       // 生存
		Starter,        // 初期
		Robot           // ロボット
	}
}
