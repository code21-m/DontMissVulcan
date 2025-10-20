using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;

namespace DontMissVulcan.Models
{
	internal class OcrLineRecognizer(Language language)
	{
		private readonly OcrEngine _ocrEngine = OcrEngine.TryCreateFromLanguage(language);

		public async Task<IEnumerable<string>> RecognizeAsync(SoftwareBitmap softwareBitmap)
		{
			var ocrResult = await _ocrEngine.RecognizeAsync(softwareBitmap);
			var lines = ocrResult.Lines;
			var lineTexts = _ocrEngine.RecognizerLanguage.IsSpaceDelimited()
				? lines.Select(line => line.Text.Trim())
				: lines.Select(line => line.Words).Select(words => string.Concat(words.Select(word => word.Text).Where(text => !string.IsNullOrWhiteSpace(text))));
			return lineTexts;
		}
	}
}
