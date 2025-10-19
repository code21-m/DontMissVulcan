using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Win32.Foundation;

namespace DontMissVulcan.Models
{
	internal static class ScreenCapturer
	{
		public static Bitmap CaptureWindow(HWND hWnd)
		{
			var rectangle = WindowHelper.GetWindowRectangle(hWnd);
			return CaptureRectangle(rectangle);
		}

		public static Bitmap CaptureRectangle(Rectangle rectangle)
		{
			var bitmap = new Bitmap(rectangle.Width, rectangle.Height);
			using (var graphics = Graphics.FromImage(bitmap))
			{
				graphics.CopyFromScreen(rectangle.Left, rectangle.Top, 0, 0, rectangle.Size);
			}
			return bitmap;
		}
	}
}
