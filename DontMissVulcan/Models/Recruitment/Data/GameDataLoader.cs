using DontMissVulcan.Models.Data.Dto;
using DontMissVulcan.Models.Recruitment.Data.Dto;
using DontMissVulcan.Models.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DontMissVulcan.Models.Recruitment.Data
{
	/// <summary>
	/// ゲームデータをJSONファイルから読み込みます。
	/// </summary>
	public static class GameDataLoader
	{
		/// <summary>
		/// ゲームデータをJSONファイルから読み込みます。
		/// </summary>
		/// <param name="tagToDisplayNameJsonPath">タグとその表示名を対応付けるJSONファイルのパス</param>
		/// <param name="operatorsJsonPath">オペレーター情報を格納したJSONファイルのパス</param>
		/// <returns>ゲームデータ</returns>
		/// <exception cref="InvalidOperationException">不正なJSONファイルのパスが指定された場合にスローされます。</exception>
		public static GameData Load(string tagToDisplayNameJsonPath, string operatorsJsonPath)
		{
			string tagToDisplayNameJson;
			try
			{
				tagToDisplayNameJson = File.ReadAllText(tagToDisplayNameJsonPath);
			}
			catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is System.Security.SecurityException)
			{
				throw new InvalidOperationException($"Failed to read tag-to-display-name JSON file: '{tagToDisplayNameJsonPath}'", ex);
			}

			Dictionary<string, string> tagToDisplayNameDto;
			try
			{
				tagToDisplayNameDto = JsonSerializer.Deserialize(tagToDisplayNameJson, GameDataJsonContext.Default.DictionaryStringString)
					?? [];
			}
			catch (JsonException ex)
			{
				throw new InvalidOperationException($"Failed to parse tag-to-display-name JSON file: '{tagToDisplayNameJsonPath}'", ex);
			}

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

			string operatorsJson;
			try
			{
				operatorsJson = File.ReadAllText(operatorsJsonPath);
			}
			catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is System.Security.SecurityException)
			{
				throw new InvalidOperationException($"Failed to read operators JSON file: '{operatorsJsonPath}'", ex);
			}

			OperatorsDto operatorsDto;
			try
			{
				operatorsDto = JsonSerializer.Deserialize(operatorsJson, GameDataJsonContext.Default.OperatorsDto) ?? new OperatorsDto();
			}
			catch (JsonException ex)
			{
				throw new InvalidOperationException($"Failed to parse operators JSON file: '{operatorsJsonPath}'", ex);
			}

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
