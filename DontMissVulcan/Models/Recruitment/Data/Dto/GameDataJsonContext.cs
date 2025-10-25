using DontMissVulcan.Models.Recruitment.Data.Dto;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DontMissVulcan.Models.Data.Dto
{
	/// <summary>
	/// ゲームデータ読み込み用のJsonContext
	/// </summary>
	[JsonSerializable(typeof(Dictionary<string, string>))]
	[JsonSerializable(typeof(OperatorsDto))]
	internal partial class GameDataJsonContext : JsonSerializerContext
	{
	}
}
