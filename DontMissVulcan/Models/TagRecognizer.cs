using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;

namespace DontMissVulcan.Models
{
	internal class TagRecognizer(Language language, GameData gameData)
	{
		private readonly GameData gameData = gameData;
		private readonly OcrEngine ocrEngine = OcrEngine.TryCreateFromLanguage(language);

		public async Task<IEnumerable<Tag>> Recognize(SoftwareBitmap softwareBitmap)
		{
			var ocrResult = await ocrEngine.RecognizeAsync(softwareBitmap);
			var words = ocrResult.Lines
				.Select(l => l.Words)
				.Select(w => string.Concat(w.Select(w => w.Text)));
			var tags = words
				.Where(t => gameData.StringToTag.ContainsKey(t))
				.Select(t => gameData.StringToTag[t]).ToHashSet();
			return tags;
		}
	}
}
