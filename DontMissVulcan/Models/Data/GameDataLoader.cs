using DontMissVulcan.Models.Data.Dto;
using DontMissVulcan.Models.Domain;
using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DontMissVulcan.Models.Data
{
	public static class GameDataLoader
	{
		public static GameData Load(string tagToDisplayNameJsonPath, string operatorsJsonPath)
		{
			var tagToDisplayNameJson = File.ReadAllText(tagToDisplayNameJsonPath);
			var tagToDisplayNameDto = JsonSerializer.Deserialize(tagToDisplayNameJson, GameDataJsonContext.Default.DictionaryStringString) ?? [];
			var tagToDisplayNameBuilder = ImmutableDictionary.CreateBuilder<Tag, string>();
			var displayNameToTagBuilder = ImmutableDictionary.CreateBuilder<string, Tag>();
			foreach (var kv in tagToDisplayNameDto)
			{
				var tagString = kv.Key ?? string.Empty;
				var displayName = kv.Value ?? string.Empty;
				if (Enum.TryParse<Tag>(tagString, out var tag))
				{
					tagToDisplayNameBuilder.Add(tag, displayName);
					displayNameToTagBuilder.Add(displayName, tag);
				}
			}
			var tagToDisplayName = tagToDisplayNameBuilder.ToImmutable();
			var displayNameToTag = displayNameToTagBuilder.ToImmutable();

			var operatorsJson = File.ReadAllText(operatorsJsonPath);
			var operatorsDto = JsonSerializer.Deserialize(operatorsJson, GameDataJsonContext.Default.OperatorsDto) ?? new OperatorsDto();
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
	}
}
