using DontMissVulcan.Models.Recruitment.Domain;
using System.Collections.Generic;

namespace DontMissVulcan.Models.Recruitment.Matching
{
	/// <summary>
	/// タグとマッチするオペレーターを表します。
	/// </summary>
	/// <param name="Tags">タグ</param>
	/// <param name="Operators">オペレーター</param>
	public sealed record Match(IReadOnlyCollection<Tag> Tags, IReadOnlyCollection<Operator> Operators);
}
