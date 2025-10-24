using DontMissVulcan.Models.Platform;
using Microsoft.UI.Xaml;
using WinRT.Interop;

namespace DontMissVulcan
{
	public sealed partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			SetlWindowSize(1000, 1000);
		}

		private void SetlWindowSize(int width, int height)
		{
			var hWnd = WindowNative.GetWindowHandle(this);
			var dpiScale = WindowInterop.GetDpiScaleForWindow(hWnd);
			AppWindow.ResizeClient(new((int)(width * dpiScale), (int)(height * dpiScale)));
		}
	}
}
