using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DontMissVulcan.Models
{
	internal class GameData
	{
		public ImmutableDictionary<Tag, string> TagToDisplayName { get; }
		public ImmutableDictionary<string, Tag> DisplayNameToTag { get; }
		public ImmutableList<Operator> Operators { get; }

		public GameData(string tagToDisplayNameJsonPath, string operatorsJsonPath)
		{
			var tagToDisplayNameJson = File.ReadAllText(tagToDisplayNameJsonPath);
			var tagToDisplayNameDto = JsonSerializer.Deserialize<Dictionary<string, string>>(tagToDisplayNameJson) ?? [];
			var tagToDisplayNameBuilder = ImmutableDictionary.CreateBuilder<Tag, string>();
			var displayNameToTagBuilder = ImmutableDictionary.CreateBuilder<string, Tag>();
			foreach (var kv in tagToDisplayNameDto)
			{
				var key = kv.Key ?? string.Empty;
				var value = kv.Value ?? string.Empty;
				if (Enum.TryParse<Tag>(key, out var tag))
				{
					tagToDisplayNameBuilder.Add(tag, value);
					displayNameToTagBuilder.Add(value, tag);
				}
			}
			TagToDisplayName = tagToDisplayNameBuilder.ToImmutable();
			DisplayNameToTag = displayNameToTagBuilder.ToImmutable();

			var operatorsJson = File.ReadAllText(operatorsJsonPath);
			var operatorsDto = JsonSerializer.Deserialize<OperatorsDto>(operatorsJson) ?? new OperatorsDto();
			Operators = operatorsDto.Operators?.Select(operatorDto => new Operator
			{
				Name = operatorDto.Name ?? string.Empty,
				Rarity = operatorDto.Rarity,
				Class = DisplayNameToTag[operatorDto.Class ?? string.Empty],
				Position = DisplayNameToTag[operatorDto.Position ?? string.Empty],
				Specializations = operatorDto.Specializations?.Select(s => DisplayNameToTag[s]).ToImmutableHashSet() ?? []
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
