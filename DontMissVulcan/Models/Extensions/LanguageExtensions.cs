using System;
using System.Linq;
using Windows.Globalization;

namespace DontMissVulcan.Models.Extensions
{
	internal static class LanguageExtensions
	{
		private static readonly string[] s_noSpaceDelimitedLanguagePrefixes =
		{
			"ja", // Japanese
			"zh", // Chinese (Simplified/Traditional)
			"ko", // Korean
			"th", // Thai
			"lo", // Lao
			"km", // Khmer
			"my", // Burmese
			"bo"  // Tibetan
		};

		public static bool IsSpaceDelimited(this Language source)
		{
			ArgumentNullException.ThrowIfNull(source);

			var languagePrefix = source.LanguageTag.Split('-')[0];
			return !s_noSpaceDelimitedLanguagePrefixes.Contains(languagePrefix, StringComparer.OrdinalIgnoreCase);
		}
	}
}
