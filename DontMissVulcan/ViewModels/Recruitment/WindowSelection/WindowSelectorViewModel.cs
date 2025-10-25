using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DontMissVulcan.Models.Platform;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DontMissVulcan.ViewModels.Recruitment.WindowSelection
{
	/// <summary>
	/// ウィンドウ選択機能のViewModel
	/// </summary>
	internal partial class WindowSelectorViewModel : ObservableObject
	{
		/// <summary>
		/// ウィンドウタイトルの一覧
		/// </summary>
		public ObservableCollection<string> WindowTitles { get; } = [];

		/// <summary>
		/// ウィンドウハンドルの一覧
		/// </summary>
		public List<IntPtr> WindowHWnds { get; } = [];

		/// <summary>
		/// 選択されたウィンドウのインデックス
		/// </summary>
		[ObservableProperty]
		public partial int SelectedWindowIndex { get; set; } = -1;

		/// <summary>
		/// 選択されたウィンドウのハンドル
		/// </summary>
		public IntPtr SelectedWindowHwnd => SelectedWindowIndex != -1 ? WindowHWnds[SelectedWindowIndex] : IntPtr.Zero;

		public WindowSelectorViewModel()
		{
			UpdateWindows();
		}

		/// <summary>
		/// ウィンドウの一覧を更新します。
		/// </summary>
		[RelayCommand]
		public void UpdateWindows()
		{
			SelectedWindowIndex = -1;
			WindowTitles.Clear();
			WindowHWnds.Clear();
			var windows = WindowInterop.GetAllWindows();
			var windowNames = windows.Select(window => window.title);
			var windowHWnds = windows.Select(window => window.hWnd);
			foreach (var windowName in windowNames)
			{
				WindowTitles.Add(windowName);
			}
			WindowHWnds.AddRange(windowHWnds);
		}

		/// <summary>
		/// 3秒後にアクティブなウィンドウを取得し選択します。
		/// </summary>
		/// <returns></returns>
		[RelayCommand]
		public async Task GetForegroundWindowAfter3s()
		{
			await Task.Delay(3000);
			UpdateWindows();
			var (hWnd, _) = WindowInterop.GetForegroundWindow();
			SelectedWindowIndex = WindowHWnds.IndexOf(hWnd);
		}
	}
}
