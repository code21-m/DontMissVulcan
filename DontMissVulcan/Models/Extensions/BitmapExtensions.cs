using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;

namespace DontMissVulcan.Models.Extensions
{
	/// <summary>
	/// Bitmapの拡張メソッド
	/// </summary>
	public static class BitmapExtensions
	{
		/// <summary>
		/// ピクセルフォーマットを変換したBitmapを新たに作成します。
		/// </summary>
		/// <param name="source"></param>
		/// <param name="pixelFormat">ピクセルフォーマット</param>
		/// <returns>ピクセルフォーマットを変換したBitmap</returns>
		public static Bitmap CloneWithPixelFormat(this Bitmap source, PixelFormat pixelFormat)
		{
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

		/// <summary>
		/// SoftwareBitmapに変換します。
		/// </summary>
		/// <param name="source"></param>
		/// <returns>SoftwareBitmap</returns>
		public static SoftwareBitmap ToSoftwareBitmap(this Bitmap source)
		{
			if (source.PixelFormat == PixelFormat.Format32bppPArgb)
			{
				return ToSoftwareBitmapCore(source);
			}
			else
			{
				using var bitmap32 = source.CloneWithPixelFormat(PixelFormat.Format32bppPArgb);
				return ToSoftwareBitmapCore(bitmap32);
			}
		}

		/// <summary>
		/// ピクセルフォーマットがFormat32bppPArgbのBitmapをSoftwareBitmapに変換します。
		/// </summary>
		/// <param name="bitmap">Bitmap</param>
		/// <returns>SoftwareBitmap</returns>
		private static SoftwareBitmap ToSoftwareBitmapCore(Bitmap bitmap)
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
