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
	internal partial class WindowSelectorViewModel : ObservableObject
	{
		public ObservableCollection<string> WindowNames { get; } = [];

		public List<IntPtr> WindowHWnds { get; } = [];

		[ObservableProperty]
		public partial int SelectedWindowIndex { get; set; } = -1;

		public IntPtr SelectedWindowHwnd => SelectedWindowIndex != -1 ? WindowHWnds[SelectedWindowIndex] : IntPtr.Zero;

		public WindowSelectorViewModel()
		{
			UpdateWindows();
		}

		[RelayCommand]
		public void UpdateWindows()
		{
			SelectedWindowIndex = -1;
			WindowNames.Clear();
			WindowHWnds.Clear();
			var windows = WindowInterop.GetAllWindows();
			var windowNames = windows.Select(window => window.title);
			var windowHWnds = windows.Select(window => window.hWnd);
			foreach (var windowName in windowNames)
			{
				WindowNames.Add(windowName);
			}
			WindowHWnds.AddRange(windowHWnds);
		}

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
