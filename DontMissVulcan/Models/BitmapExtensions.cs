using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

namespace DontMissVulcan.Models
{
	internal static class BitmapExtensions
	{
		public static Bitmap CloneWithPixelFormat(this Bitmap source, PixelFormat pixelFormat)
		{
			ArgumentNullException.ThrowIfNull(source);

			if (source.PixelFormat == pixelFormat)
			{
				return (Bitmap)source.Clone();
			}

			var newBitmap = new Bitmap(source.Width, source.Height, pixelFormat);
			using (var graphics = Graphics.FromImage(newBitmap))
			{
				graphics.CompositingMode = CompositingMode.SourceCopy;
				graphics.DrawImageUnscaled(source, 0, 0);
			}
			return newBitmap;
		}

		public static SoftwareBitmap ToSoftwareBitmap(this Bitmap source)
		{
			ArgumentNullException.ThrowIfNull(source);

			if (source.PixelFormat == PixelFormat.Format32bppPArgb)
			{
				return BitmapFormat32bppPArgbToSotwareBitmap(source);
			}
			else
			{
				using var bitmap32 = source.CloneWithPixelFormat(PixelFormat.Format32bppPArgb);
				return BitmapFormat32bppPArgbToSotwareBitmap(bitmap32);
			}
		}

		private static SoftwareBitmap BitmapFormat32bppPArgbToSotwareBitmap(Bitmap bitmap)
		{
			Debug.Assert(bitmap.PixelFormat == PixelFormat.Format32bppPArgb);

			var rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
			var data = bitmap.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppPArgb);
			try
			{
				int bytes = Math.Abs(data.Stride) * bitmap.Height;
				var pixels = new byte[bytes];
				Marshal.Copy(data.Scan0, pixels, 0, bytes);
				var softwareBitmap = new SoftwareBitmap(BitmapPixelFormat.Bgra8, bitmap.Width, bitmap.Height, BitmapAlphaMode.Premultiplied);
				var buffer = pixels.AsBuffer();
				softwareBitmap.CopyFromBuffer(buffer);
				return softwareBitmap;
			}
			finally
			{
				bitmap.UnlockBits(data);
			}
		}
	}
}
