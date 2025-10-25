using DontMissVulcan.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;

namespace DontMissVulcan.Models.Platform
{
	/// <summary>
	/// Windows標準のOCRでテキスト認識を行います。
	/// </summary>
	/// <param name="language">言語</param>
	public class OcrTextRecognizer(Language language)
	{
		/// <summary>
		/// OCRエンジン
		/// </summary>
		private readonly OcrEngine _ocrEngine = OcrEngine.TryCreateFromLanguage(language);

		/// <summary>
		/// 指定された画像に対してテキスト認識を行います。
		/// </summary>
		/// <param name="softwareBitmap">画像</param>
		/// <returns>行ごとの認識されたテキスト</returns>
		public async Task<IReadOnlyList<string>> RecognizeAsync(SoftwareBitmap softwareBitmap)
		{
			var ocrResult = await _ocrEngine.RecognizeAsync(softwareBitmap);
			var lines = ocrResult.Lines;
			var lineTexts = _ocrEngine.RecognizerLanguage.IsSpaceDelimited()
				? lines.Select(line => line.Text.Trim())
				: lines.Select(line => line.Words).Select(words => string.Concat(words.Select(word => word.Text).Where(text => !string.IsNullOrWhiteSpace(text))));
			return [.. lineTexts];
		}
	}
}
