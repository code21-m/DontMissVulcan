using DontMissVulcan.Models.Domain;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DontMissVulcan.Models.Data
{
	internal static class GameDataLoader
	{
		public static GameData Load(string tagToDisplayNameJsonPath, string operatorsJsonPath)
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
			var tagToDisplayName = tagToDisplayNameBuilder.ToImmutable();
			var displayNameToTag = displayNameToTagBuilder.ToImmutable();

			var operatorsJson = File.ReadAllText(operatorsJsonPath);
			var operatorsDto = JsonSerializer.Deserialize<OperatorsDto>(operatorsJson) ?? new OperatorsDto();
			var operators = operatorsDto.Operators?.Select(operatorDto => new Operator
			{
				Name = operatorDto.Name ?? string.Empty,
				Rarity = operatorDto.Rarity,
				Class = displayNameToTag[operatorDto.Class ?? string.Empty],
				Position = displayNameToTag[operatorDto.Position ?? string.Empty],
				Specializations = operatorDto.Specializations?.Select(s => displayNameToTag[s]).ToImmutableHashSet() ?? []
			}).ToImmutableList() ?? [];

			return new GameData(tagToDisplayName, displayNameToTag, operators);
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
