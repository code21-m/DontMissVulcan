using System;
using System.Linq;
using Windows.Globalization;

namespace DontMissVulcan.Models.Extensions
{
	/// <summary>
	/// Languageの拡張メソッド
	/// </summary>
	public static class LanguageExtensions
	{
		/// <summary>
		/// 既知のスペースで区切られない言語
		/// </summary>
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

		/// <summary>
		/// スペースで区切られる言語かどうか判定します。
		/// </summary>
		/// <param name="source"></param>
		/// <returns>スペースで区切られる言語ならばTrue（例: en-US）、そうでなければFalse（例: ja-JP）</returns>
		public static bool IsSpaceDelimited(this Language source)
		{
			var languagePrefix = source.LanguageTag.Split('-')[0];
			return !s_noSpaceDelimitedLanguagePrefixes.Contains(languagePrefix, StringComparer.OrdinalIgnoreCase);
		}
	}
}
