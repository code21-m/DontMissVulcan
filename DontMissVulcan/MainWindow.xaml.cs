using DontMissVulcan.Models.Platform;
using Microsoft.UI.Xaml;
using WinRT.Interop;

namespace DontMissVulcan
{
	/// <summary>
	/// メインウィンドウ
	/// </summary>
	public sealed partial class MainWindow : Window
	{
		/// <summary>
		/// メインウィンドウを初期化します。
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();

			SetlWindowSize(440, 600);
		}

		/// <summary>
		/// ウィンドウサイズを設定します。
		/// </summary>
		/// <param name="width">横幅</param>
		/// <param name="height">高さ</param>
		private void SetlWindowSize(int width, int height)
		{
			var hWnd = WindowNative.GetWindowHandle(this);
			var dpiScale = WindowInterop.GetDpiScaleForWindow(hWnd);
			AppWindow.ResizeClient(new((int)(width * dpiScale), (int)(height * dpiScale)));
		}
	}
}
