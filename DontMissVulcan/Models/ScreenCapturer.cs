using System.Drawing;
using System.Drawing.Imaging;
using Windows.Win32.Foundation;

namespace DontMissVulcan.Models
{
	internal static class ScreenCapturer
	{
		public static Bitmap CaptureWindow(HWND hWnd)
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
