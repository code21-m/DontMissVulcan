using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;

namespace DontMissVulcan.Models
{
	internal class TagRecognizer(GameData gameData, Language language)
	{
		private readonly GameData _gameData = gameData;
		private readonly OcrEngine _ocrEngine = OcrEngine.TryCreateFromLanguage(language);

		public async Task<IEnumerable<Tag>> Recognize(SoftwareBitmap softwareBitmap)
		{
			var ocrResult = await _ocrEngine.RecognizeAsync(softwareBitmap);
			var lineTexts = ocrResult.Lines
				.Select(line => line.Words)
				.Select(wordsPerLine => string.Concat(wordsPerLine.Select(word => word.Text)));
			var tags = lineTexts
				.Where(_gameData.DisplayNameToTag.ContainsKey)
				.Select(text => _gameData.DisplayNameToTag[text]).ToHashSet();
			return tags;
		}
	}
}
