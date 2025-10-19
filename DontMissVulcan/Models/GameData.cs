using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DontMissVulcan.Models
{
	internal class GameData
	{
		public ImmutableDictionary<Tag, string> TagToString { get; }
		public ImmutableDictionary<string, Tag> StringToTag { get; }
		public ImmutableList<Operator> Operators { get; }

		public GameData(string tagLanguageJsonPath, string operatorsJsonPath)
		{
			var tagLanguageJson = File.ReadAllText(tagLanguageJsonPath);
			var tagLanguageDto = JsonSerializer.Deserialize<Dictionary<string, string>>(tagLanguageJson) ?? [];
			var tagToStringBuilder = ImmutableDictionary.CreateBuilder<Tag, string>();
			var stringToTagBuilder = ImmutableDictionary.CreateBuilder<string, Tag>();
			foreach (var kv in tagLanguageDto)
			{
				var key = kv.Key ?? string.Empty;
				var value = kv.Value ?? string.Empty;
				if (Enum.TryParse<Tag>(key, out var tag))
				{
					tagToStringBuilder.Add(tag, value);
					stringToTagBuilder.Add(value, tag);
				}
			}
			TagToString = tagToStringBuilder.ToImmutable();
			StringToTag = stringToTagBuilder.ToImmutable();

			var operatorsJson = File.ReadAllText(operatorsJsonPath);
			var operatorsDto = JsonSerializer.Deserialize<OperatorsDto>(operatorsJson) ?? new OperatorsDto();
			Operators = operatorsDto.Operators?.Select(o => new Operator
			{
				Name = o.Name ?? string.Empty,
				Rarity = o.Rarity,
				Class = StringToTag[o.Class ?? string.Empty],
				Position = StringToTag[o.Position ?? string.Empty],
				Specializations = o.Specializations?.Select(s => StringToTag[s]).ToImmutableList() ?? []
			}).ToImmutableList() ?? [];
		}

		private class OperatorsDto
		{
			public List<OperatorDto>? Operators { get; set; }
		}

		private class OperatorDto
		{
			public string? Name { get; set; }
			public int Rarity { get; set; }
			public string? Class { get; set; }
			public string? Position { get; set; }
			public List<string>? Specializations { get; set; }
		}
	}
}
