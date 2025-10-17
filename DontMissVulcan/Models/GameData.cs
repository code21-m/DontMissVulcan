using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DontMissVulcan.Models
{
	internal class GameData
	{
		public Tags Tags { get; }
		public ImmutableList<Operator> Operators { get; }

		public GameData(string tagsJsonPath, string operatorsJsonPath)
		{
			var tagsJson = File.ReadAllText(tagsJsonPath);
			var tagsDto = JsonSerializer.Deserialize<TagsDto>(tagsJson) ?? new TagsDto();

			Tags = new Tags
			{
				Rarity = tagsDto.Rarity?.ToImmutableList() ?? [],
				Job = tagsDto.Job?.ToImmutableList() ?? [],
				Position = tagsDto.Position?.ToImmutableList() ?? [],
				Other = tagsDto.Other?.ToImmutableList() ?? []
			};

			var operatorsJson = File.ReadAllText(operatorsJsonPath);
			var operatorsDto = JsonSerializer.Deserialize<OperatorsDto>(operatorsJson) ?? new OperatorsDto();

			Operators = operatorsDto.Operators?
				.Select(o => new Operator
				{
					Name = o.Name ?? string.Empty,
					Rarity = o.Rarity,
					Job = o.Job ?? string.Empty,
					Position = o.Position ?? string.Empty,
					Tags = o.Tags?.ToImmutableList() ?? []
				})
				.ToImmutableList() ?? [];
		}

		private class TagsDto
		{
			[JsonPropertyName("rarity")]
			public List<string>? Rarity { get; set; }
			[JsonPropertyName("job")]
			public List<string>? Job { get; set; }
			[JsonPropertyName("position")]
			public List<string>? Position { get; set; }
			[JsonPropertyName("other")]
			public List<string>? Other { get; set; }
		}

		private class OperatorsDto
		{
			[JsonPropertyName("operators")]
			public List<OperatorDto>? Operators { get; set; }
		}

		private class OperatorDto
		{
			[JsonPropertyName("name")]
			public string? Name { get; set; }
			[JsonPropertyName("rarity")]
			public int Rarity { get; set; }
			[JsonPropertyName("job")]
			public string? Job { get; set; }
			[JsonPropertyName("position")]
			public string? Position { get; set; }
			[JsonPropertyName("tags")]
			public List<string>? Tags { get; set; }
		}
	}
}
