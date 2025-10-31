using DontMissVulcan.Models.Platform;
using Microsoft.UI.Xaml;
using WinRT.Interop;

namespace DontMissVulcan
{
	/// <summary>
	/// ���C���E�B���h�E
	/// </summary>
	public sealed partial class MainWindow : Window
	{
		/// <summary>
		/// ���C���E�B���h�E�����������܂��B
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();

			SetlWindowSize(440, 600);
		}

		/// <summary>
		/// �E�B���h�E�T�C�Y��ݒ肵�܂��B
		/// </summary>
		/// <param name="width">����</param>
		/// <param name="height">����</param>
		private void SetlWindowSize(int width, int height)
		{
			var hWnd = WindowNative.GetWindowHandle(this);
			var dpiScale = WindowInterop.GetDpiScaleForWindow(hWnd);
			AppWindow.ResizeClient(new((int)(width * dpiScale), (int)(height * dpiScale)));
		}
	}
}
