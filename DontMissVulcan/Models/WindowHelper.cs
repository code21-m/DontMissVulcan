using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace DontMissVulcan.Models
{
	internal static partial class WindowHelper
	{
		public static IEnumerable<(HWND HWnd, string Title)> EnumerateWindows()
		{
			var windows = new List<(HWND HWnd, string Title)>();
			PInvoke.EnumWindows((hWnd, lParam) =>
			{
				if (!PInvoke.IsWindowVisible(hWnd))
				{
					return true;
				}
				string title = GetWindowTitle(hWnd);
				if (!String.IsNullOrWhiteSpace(title))
				{
					windows.Add((hWnd, title));
				}
				return true;
			}, 0);
			return windows;
		}

		public static (HWND HWnd, string Title) GetForegroundWindow()
		{
			var hWnd = PInvoke.GetForegroundWindow();
			string title = GetWindowTitle(hWnd);
			return (hWnd, title);
		}

		public static bool SetForegroundWindow(HWND hWnd)
		{
			if (hWnd == HWND.Null)
			{
				return false;
			}
			if (PInvoke.IsIconic(hWnd))
			{
				PInvoke.ShowWindow(hWnd, SHOW_WINDOW_CMD.SW_RESTORE);
			}
			PInvoke.SetForegroundWindow(hWnd);
			if (IsForegroundWindow(hWnd))
			{
				return true;
			}
			// ウィンドウの表示・最前面化を行い再試行
			PInvoke.ShowWindow(hWnd, SHOW_WINDOW_CMD.SW_RESTORE);
			PInvoke.BringWindowToTop(hWnd);
			PInvoke.SetForegroundWindow(hWnd);
			return IsForegroundWindow(hWnd);
		}

		public static Rectangle GetWindowRectangle(HWND hWnd)
		{
			if (hWnd == HWND.Null)
			{
				return Rectangle.Empty;
			}
			if (PInvoke.GetWindowRect(hWnd, out RECT rect))
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

		private static bool IsForegroundWindow(HWND hWnd)
		{
			if (hWnd == HWND.Null)
			{
				return false;
			}
			return hWnd == PInvoke.GetForegroundWindow();
		}

		private static string GetWindowTitle(HWND hWnd)
		{
			if (hWnd == HWND.Null)
			{
				return String.Empty;
			}
			var length = PInvoke.GetWindowTextLength(hWnd);
			if (length == 0)
			{
				return String.Empty;
			}
			Span<char> buffer = stackalloc char[length + 1];
			var copiedLength = PInvoke.GetWindowText(hWnd, buffer);
			if (copiedLength == 0)
			{
				return String.Empty;
			}
			return new string(buffer[..copiedLength]);
		}
	}
}
