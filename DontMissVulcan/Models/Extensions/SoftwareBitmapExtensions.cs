using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;

namespace DontMissVulcan.Models.Extensions
{
	internal static class SoftwareBitmapExtensions
	{
		public static async Task SaveAsync(this SoftwareBitmap source, string filename)
		{
			ArgumentNullException.ThrowIfNull(source);

			if (string.IsNullOrWhiteSpace(filename))
			{
				throw new ArgumentException("filename must be provided", nameof(filename));
			}
			var directory = Path.GetDirectoryName(filename);
			if (string.IsNullOrEmpty(directory))
			{
				throw new ArgumentException("Invalid filename", nameof(filename));
			}
			Directory.CreateDirectory(directory);
			var ext = Path.GetExtension(filename)?.ToLowerInvariant();
			Guid encoderId;
			bool supportsAlpha;
			switch (ext)
			{
				case ".png":
					encoderId = BitmapEncoder.PngEncoderId;
					supportsAlpha = true;
					break;
				case ".jpg":
				case ".jpeg":
					encoderId = BitmapEncoder.JpegEncoderId;
					supportsAlpha = false;
					break;
				case ".bmp":
					encoderId = BitmapEncoder.BmpEncoderId;
					supportsAlpha = false;
					break;
				case ".gif":
					encoderId = BitmapEncoder.GifEncoderId;
					supportsAlpha = false;
					break;
				case ".tif":
				case ".tiff":
					encoderId = BitmapEncoder.TiffEncoderId;
					supportsAlpha = true;
					break;
				default:
					filename += ".png";
					encoderId = BitmapEncoder.PngEncoderId;
					supportsAlpha = true;
					break;
			}

			SoftwareBitmap bitmapToEncode = source.BitmapPixelFormat == BitmapPixelFormat.Bgra8 && source.BitmapAlphaMode == BitmapAlphaMode.Premultiplied
				? source
				: SoftwareBitmap.Convert(source, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);

			try
			{
				var folder = await StorageFolder.GetFolderFromPathAsync(directory);
				var file = await folder.CreateFileAsync(Path.GetFileName(filename), CreationCollisionOption.ReplaceExisting);
				using var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
				var encoder = await BitmapEncoder.CreateAsync(encoderId, stream);

				if (supportsAlpha)
				{
					encoder.SetSoftwareBitmap(bitmapToEncode);
					await encoder.FlushAsync();
				}
				else
				{
					int width = bitmapToEncode.PixelWidth;
					int height = bitmapToEncode.PixelHeight;
					var pixels = FlattenToBgra8White(bitmapToEncode);

					encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
						(uint)width, (uint)height, 96.0, 96.0, pixels);
					await encoder.FlushAsync();
				}
			}
			finally
			{
				if (!ReferenceEquals(bitmapToEncode, source))
				{
					bitmapToEncode.Dispose();
				}
			}
		}

		private static byte[] FlattenToBgra8White(SoftwareBitmap bitmap)
		{
			Debug.Assert(bitmap.BitmapAlphaMode == BitmapAlphaMode.Premultiplied);

			int width = bitmap.PixelWidth;
			int height = bitmap.PixelHeight;
			int totalBytes = width * height * 4;
			var pixels = new byte[totalBytes];
			var buffer = pixels.AsBuffer(0, totalBytes);
			bitmap.CopyToBuffer(buffer);

			for (int i = 0; i < totalBytes; i += 4)
			{
				int a = pixels[i + 3];
				int inv = 255 - a;
				pixels[i + 0] = (byte)Math.Min(255, pixels[i + 0] + inv);
				pixels[i + 1] = (byte)Math.Min(255, pixels[i + 1] + inv);
				pixels[i + 2] = (byte)Math.Min(255, pixels[i + 2] + inv);
				pixels[i + 3] = 255;
			}

			return pixels;
		}
	}
}
