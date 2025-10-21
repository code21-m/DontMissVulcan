using System;
using System.Collections.Generic;
using System.Drawing;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace DontMissVulcan.Models.Platform
{
	public static class WindowInterop
	{
		public static string GetWindowTitle(IntPtr hWnd)
		{
			var _hWnd = (HWND)hWnd;
			if (_hWnd == HWND.Null)
			{
				return string.Empty;
			}
			var length = PInvoke.GetWindowTextLength(_hWnd);
			if (length == 0)
			{
				return string.Empty;
			}
			Span<char> buffer = stackalloc char[length + 1];
			var copiedLength = PInvoke.GetWindowText(_hWnd, buffer);
			if (copiedLength == 0)
			{
				return string.Empty;
			}
			return new string(buffer[..copiedLength]);
		}

		public static IEnumerable<(IntPtr hWnd, string title)> EnumerateWindows()
		{
			var windows = new List<(IntPtr, string)>();
			PInvoke.EnumWindows((hWnd, lParam) =>
			{
				if (!PInvoke.IsWindowVisible(hWnd))
				{
					return true;
				}
				string title = GetWindowTitle(hWnd);
				if (!string.IsNullOrWhiteSpace(title))
				{
					windows.Add((hWnd, title));
				}
				return true;
			}, 0);
			return windows;
		}

		public static (IntPtr hWnd, string title) GetForegroundWindow()
		{
			var hWnd = PInvoke.GetForegroundWindow();
			string title = GetWindowTitle(hWnd);
			return (hWnd, title);
		}

		public static bool IsForegroundWindow(IntPtr hWnd)
		{
			if (hWnd == HWND.Null)
			{
				return false;
			}
			return hWnd == PInvoke.GetForegroundWindow();
		}

		public static bool SetForegroundWindow(IntPtr hWnd)
		{
			var _hWnd = (HWND)hWnd;
			if (_hWnd == HWND.Null)
			{
				return false;
			}
			if (PInvoke.IsIconic(_hWnd))
			{
				PInvoke.ShowWindow(_hWnd, SHOW_WINDOW_CMD.SW_RESTORE);
			}
			PInvoke.SetForegroundWindow(_hWnd);
			if (IsForegroundWindow(_hWnd))
			{
				return true;
			}
			// ウィンドウの表示・最前面化を行い再試行
			PInvoke.ShowWindow(_hWnd, SHOW_WINDOW_CMD.SW_RESTORE);
			PInvoke.BringWindowToTop(_hWnd);
			PInvoke.SetForegroundWindow(_hWnd);
			return IsForegroundWindow(_hWnd);
		}

		public static Rectangle GetWindowRectangle(IntPtr hWnd)
		{
			var _hWnd = (HWND)hWnd;
			if (_hWnd == HWND.Null)
			{
				return Rectangle.Empty;
			}
			if (PInvoke.GetWindowRect(_hWnd, out RECT rect))
			{
				int width = rect.right - rect.left;
				int height = rect.bottom - rect.top;
				return new Rectangle(rect.left, rect.top, width, height);
			}
			else
			{
				return Rectangle.Empty;
			}
		}
	}
}
