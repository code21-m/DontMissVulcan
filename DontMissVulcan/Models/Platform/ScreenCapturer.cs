using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace DontMissVulcan.Models.Platform
{
	/// <summary>
	/// 画面キャプチャを行います。
	/// </summary>
	public static class ScreenCapturer
	{
		/// <summary>
		/// 指定されたウィンドウをキャプチャします。
		/// </summary>
		/// <param name="hWnd">ウィンドウハンドル</param>
		/// <returns>画像</returns>
		/// <exception cref="ArgumentException">適切なウィンドウハンドルが指定されていない場合にスローされます。</exception>
		/// <exception cref="InvalidOperationException">キャプチャに失敗した場合にスローされます。</exception>
		public static Bitmap CaptureWindow(IntPtr hWnd)
		{
			if (hWnd == IntPtr.Zero)
			{
				throw new ArgumentException("IntPtr.Zeroは有効なハンドルではありません。", nameof(hWnd));
			}

			var rectangle = WindowInterop.GetWindowRectangle(hWnd);
			try
			{
				return CaptureRectangle(rectangle);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"ウィンドウ(hWnd={hWnd})のキャプチャに失敗しました。", ex);
			}
		}

		/// <summary>
		/// 指定された矩形領域をキャプチャします。
		/// </summary>
		/// <param name="rectangle">矩形領域</param>
		/// <returns>画像</returns>
		/// <exception cref="ArgumentException">矩形領域の幅または高さが0の場合にスローされます。</exception>
		public static Bitmap CaptureRectangle(Rectangle rectangle)
		{
			if (rectangle.Width == 0 || rectangle.Height == 0)
			{
				throw new ArgumentException("矩形領域の幅または高さが0です。正のサイズを持つ矩形を指定してください。", nameof(rectangle));
			}
			var bitmap = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppPArgb);
			using (var graphics = Graphics.FromImage(bitmap))
			{
				graphics.CopyFromScreen(rectangle.Left, rectangle.Top, 0, 0, rectangle.Size);
			}
			return bitmap;
		}
	}
}
