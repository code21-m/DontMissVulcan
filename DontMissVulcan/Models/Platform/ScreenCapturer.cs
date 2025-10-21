using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace DontMissVulcan.Models.Platform
{
	public static class ScreenCapturer
	{
		public static Bitmap CaptureWindow(IntPtr hWnd)
		{
			var rectangle = WindowInterop.GetWindowRectangle(hWnd);
			return CaptureRectangle(rectangle);
		}

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
