using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace DontMissVulcan.Models.Platform
{
	/// <summary>
	/// スクリーンショットを撮影します。
	/// </summary>
	public static class ScreenCapturer
	{
		/// <summary>
		/// 指定されたウィンドウのスクリーンショットを撮影します。
		/// </summary>
		/// <param name="hWnd">ウィンドウハンドル</param>
		/// <returns>スクリーンショット</returns>
		/// <exception cref="ArgumentException">適切なウィンドウハンドルが指定されていない場合にスローされます。</exception>
		public static Bitmap CaptureWindow(IntPtr hWnd)
		{
			if (hWnd == IntPtr.Zero)
			{
				throw new ArgumentException("Invalid window handle: IntPtr.Zero", nameof(hWnd));
			}

			var rectangle = WindowInterop.GetWindowRectangle(hWnd);
			return CaptureRectangle(rectangle);
		}

		/// <summary>
		/// 指定された矩形領域のスクリーンショットを撮影します。
		/// </summary>
		/// <param name="rectangle">矩形領域</param>
		/// <returns>スクリーンショット</returns>
		public static Bitmap CaptureRectangle(Rectangle rectangle)
		{
			var bitmap = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppPArgb);
			using (var graphics = Graphics.FromImage(bitmap))
			{
				graphics.CopyFromScreen(rectangle.Left, rectangle.Top, 0, 0, rectangle.Size);
			}
			return bitmap;
		}
	}
}
