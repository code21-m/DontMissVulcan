using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DontMissVulcan.Models.Data.Dto
{
	[JsonSerializable(typeof(Dictionary<string, string>))]
	[JsonSerializable(typeof(OperatorsDto))]
	internal class GameDataJsonContext
	{
	}
}
