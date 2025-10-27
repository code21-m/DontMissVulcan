using System;
using System.Collections.Generic;
using System.Drawing;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace DontMissVulcan.Models.Platform
{
	/// <summary>
	/// ウィンドウに関するWin32 APIをラップします。
	/// </summary>
	public static class WindowInterop
	{
		/// <summary>
		/// 指定されたウィンドウのタイトルを取得します。
		/// </summary>
		/// <param name="hWnd">ウィンドウハンドル</param>
		/// <returns>ウィンドウのタイトル</returns>
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

		/// <summary>
		/// すべてのウィンドウを取得します。
		/// </summary>
		/// <returns>ウィンドウハンドルとタイトル</returns>
		public static IReadOnlyCollection<(IntPtr hWnd, string title)> GetAllWindows()
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

		/// <summary>
		/// アクティブなウィンドウを取得します。
		/// </summary>
		/// <returns>ウィンドウハンドルとタイトル</returns>
		public static (IntPtr hWnd, string title) GetForegroundWindow()
		{
			var hWnd = PInvoke.GetForegroundWindow();
			string title = GetWindowTitle(hWnd);
			return (hWnd, title);
		}

		/// <summary>
		/// 指定されたウィンドウがアクティブか判定します。
		/// </summary>
		/// <param name="hWnd">ウィンドウハンドル</param>
		/// <returns>アクティブならばTrue、そうでなければFalse</returns>
		public static bool IsForegroundWindow(IntPtr hWnd)
		{
			if (hWnd == HWND.Null)
			{
				return false;
			}
			return hWnd == PInvoke.GetForegroundWindow();
		}

		/// <summary>
		/// 指定されたウィンドウをアクティブにします。
		/// </summary>
		/// <param name="hWnd">ウィンドウハンドル</param>
		/// <returns>成功すればTrue、そうでなければFalse</returns>
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
			// ウィンドウの表示・最前面化を行い再試行する。
			PInvoke.ShowWindow(_hWnd, SHOW_WINDOW_CMD.SW_RESTORE);
			PInvoke.BringWindowToTop(_hWnd);
			PInvoke.SetForegroundWindow(_hWnd);
			return IsForegroundWindow(_hWnd);
		}

		/// <summary>
		/// 指定されたウィンドウの矩形領域を取得します。
		/// </summary>
		/// <param name="hWnd">ウィンドウハンドル</param>
		/// <returns>矩形領域</returns>
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

		/// <summary>
		/// 指定されたウィンドウのDPIを取得します。
		/// </summary>
		/// <param name="hWnd">ウィンドウハンドル</param>
		/// <returns>DPI</returns>
		public static uint GetDpiForWindow(IntPtr hWnd)
		{
			return PInvoke.GetDpiForWindow((HWND)hWnd);
		}

		/// <summary>
		/// 指定されたウィンドウのDPIスケーリング係数を取得します。
		/// </summary>
		/// <param name="hWnd">ウィンドウハンドル</param>
		/// <returns>DPIスケーリング係数</returns>
		public static double GetDpiScaleForWindow(IntPtr hWnd)
		{
			const double defaultDpi = 96;
			return GetDpiForWindow(hWnd) / defaultDpi;
		}
	}
}
