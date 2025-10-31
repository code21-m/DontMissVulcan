using OpenCvSharp;
using OpenCvSharp.Extensions;
using Sdcb.PaddleInference;
using Sdcb.PaddleOCR;
using Sdcb.PaddleOCR.Models.Local;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace DontMissVulcan.Models.Platform
{
	/// <summary>
	/// PaddleOCRでテキスト認識を行います。
	/// </summary>
	public partial class OcrTextRecognizer : IDisposable
	{
		/// <summary>
		/// OCRエンジン
		/// </summary>
		private readonly PaddleOcrAll _paddleOcrAll = new(LocalFullModels.JapanV4, PaddleDevice.Mkldnn()) { AllowRotateDetection = false, Enable180Classification = false };

		private bool disposedValue;

		/// <summary>
		/// 指定された画像に対してテキスト認識を行います。
		/// </summary>
		/// <param name="Bitmap">画像</param>
		/// <returns>行ごとの認識されたテキスト</returns>
		public Task<IReadOnlyList<string>> RecognizeAsync(Bitmap bitmap)
		{
			return Task.Run(() =>
			{
				using var src = BitmapConverter.ToMat(bitmap);

				PaddleOcrResult result;
				// PaddleOcrは1チャンネルもしくは3チャンネルの画像に対応しているので、4チャンネルの場合は3チャンネルに変換する。
				if (src.Channels() == 4)
				{
					using var src3 = new Mat();
					Cv2.CvtColor(src, src3, ColorConversionCodes.BGRA2BGR);
					result = _paddleOcrAll.Run(src3);
				}
				else
				{
					result = _paddleOcrAll.Run(src);
				}

				return (IReadOnlyList<string>)[.. result.Regions.Select(region => region.Text)];
			});
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: マネージド状態を破棄します (マネージド オブジェクト)
					_paddleOcrAll.Dispose();
				}

				// TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
				// TODO: 大きなフィールドを null に設定します
				disposedValue = true;
			}
		}

		// // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
		// ~OcrTextRecognizer()
		// {
		//     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
