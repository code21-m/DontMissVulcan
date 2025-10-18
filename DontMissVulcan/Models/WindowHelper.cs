using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DontMissVulcan.Models
{
	internal static partial class WindowHelper
	{
		public static IEnumerable<(nint hWnd, string title)> EnumerateWindows()
		{
			var windows = new List<(nint hWnd, string title)>();

			EnumWindowsProc callback = (hWnd, lParam) =>
			{
				try
				{
					if (!IsWindowVisible(hWnd))
					{
						return true;
					}

					string title = GetWindowText(hWnd);
					if (!string.IsNullOrWhiteSpace(title))
					{
						windows.Add((hWnd, title));
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine($"EnumerateWindows: hWnd=0x{hWnd:X}, ex={ex.GetType().Name}: {ex.Message}");
				}

				return true;
			};

			EnumWindows(callback, nint.Zero);
			GC.KeepAlive(callback);

			return windows;
		}

		public static (nint hWnd, string title) GetForegroundWindow()
		{
			nint hWnd;
			try
			{
				hWnd = GetForegroundWindowNative();
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"GetForegroundWindow: native call failed, ex={ex.GetType().Name}: {ex.Message}");
				return (nint.Zero, string.Empty);
			}

			if (hWnd == nint.Zero)
			{
				return (nint.Zero, string.Empty);
			}

			string title = GetWindowText(hWnd);
			return (hWnd, title);
		}

		public static bool SetForegroundWindow(nint hWnd)
		{
			if (hWnd == nint.Zero)
			{
				return false;
			}

			try
			{
				try
				{
					if (IsIconic(hWnd))
					{
						ShowWindow(hWnd, SW_RESTORE);
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine($"SetForegroundWindow: restore check failed for hWnd=0x{hWnd:X}, ex={ex.GetType().Name}: {ex.Message}");
				}

				bool ok = SetForegroundWindowNative(hWnd);
				if (ok && IsWindowForeground(hWnd))
				{
					return true;
				}

				try
				{
					ShowWindow(hWnd, SW_RESTORE);
					BringWindowToTop(hWnd);
					SetForegroundWindowNative(hWnd);
				}
				catch (Exception ex)
				{
					Debug.WriteLine($"SetForegroundWindow: fallback actions failed for hWnd=0x{hWnd:X}, ex={ex.GetType().Name}: {ex.Message}");
				}

				return IsWindowForeground(hWnd);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"SetForegroundWindow: hWnd=0x{hWnd:X}, ex={ex.GetType().Name}: {ex.Message}");
				return false;
			}
		}

		public static Rectangle GetWindowRectangle(nint hWnd)
		{
			if (hWnd == nint.Zero)
			{
				return Rectangle.Empty;
			}

			if (GetWindowRect(hWnd, out RECT rect))
			{
				int width = rect.Right - rect.Left;
				int height = rect.Bottom - rect.Top;
				return new Rectangle(rect.Left, rect.Top, width, height);
			}

			return Rectangle.Empty;
		}

		private static bool IsWindowForeground(nint hWnd)
		{
			try
			{
				var fg = GetForegroundWindowNative();
				return fg == hWnd;
			}
			catch
			{
				return false;
			}
		}

		private static string GetWindowText(nint hWnd)
		{
			if (hWnd == nint.Zero)
				return string.Empty;

			try
			{
				int length = GetWindowTextLength(hWnd);
				if (length <= 0)
					return string.Empty;

				const int MaxWindowTitleLength = 4096;
				const int StackAllocLimit = 512;
				int safeLen = Math.Min(length, MaxWindowTitleLength);

				if (safeLen <= StackAllocLimit)
				{
					unsafe
					{
						char* buffer = stackalloc char[safeLen + 1];
						int n = GetWindowTextNative(hWnd, buffer, safeLen + 1);
						if (n > 0)
						{
							return new string(buffer, 0, n);
						}
					}
				}
				else
				{
					char[] bufferArray = new char[safeLen + 1];
					unsafe
					{
						fixed (char* buffer = bufferArray)
						{
							int n = GetWindowTextNative(hWnd, buffer, safeLen + 1);
							if (n > 0)
							{
								return new string(buffer, 0, n);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"GetWindowText: hWnd=0x{hWnd:X}, ex={ex.GetType().Name}: {ex.Message}");
			}

			return string.Empty;
		}

		[UnmanagedFunctionPointer(CallingConvention.Winapi)]
		private delegate bool EnumWindowsProc(nint hWnd, nint lParam);

		[LibraryImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static partial bool EnumWindows(EnumWindowsProc lpEnumFunc, nint lParam);

		[LibraryImport("user32.dll", EntryPoint = "GetWindowTextW", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
		private static unsafe partial int GetWindowTextNative(nint hWnd, char* lpString, int nMaxCount);

		[LibraryImport("user32.dll", EntryPoint = "GetWindowTextLengthW", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
		private static partial int GetWindowTextLength(nint hWnd);

		[LibraryImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static partial bool IsWindowVisible(nint hWnd);

		[LibraryImport("user32.dll", EntryPoint = "GetForegroundWindow", SetLastError = true)]
		private static partial nint GetForegroundWindowNative();

		[LibraryImport("user32.dll", EntryPoint = "SetForegroundWindow", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static partial bool SetForegroundWindowNative(nint hWnd);

		[LibraryImport("user32.dll", EntryPoint = "IsIconic", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static partial bool IsIconic(nint hWnd);

		[LibraryImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static partial bool ShowWindow(nint hWnd, int nCmdShow);

		private const int SW_RESTORE = 9;

		[LibraryImport("user32.dll", EntryPoint = "BringWindowToTop", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static partial bool BringWindowToTop(nint hWnd);

		[StructLayout(LayoutKind.Sequential)]
		private struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		[LibraryImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static partial bool GetWindowRect(nint hWnd, out RECT lpRect);
	}
}
