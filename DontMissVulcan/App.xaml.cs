using CommunityToolkit.Mvvm.DependencyInjection;
using DontMissVulcan.ViewModels.Recruitment;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;

namespace DontMissVulcan
{
	/// <summary>
	/// アプリケーション
	/// </summary>
	public partial class App : Application
	{
		/// <summary>
		/// ウィンドウ
		/// </summary>
		private Window? _window;

		/// <summary>
		/// サービスプロバイダ
		/// </summary>
		private readonly ServiceProvider _serviceProvider;

		/// <summary>
		/// ここに書かれたコードが最初に実行されます。
		/// </summary>
		public App()
		{
			// あとで破棄するためにサービスプロバイダを保持しておく。
			_serviceProvider = new ServiceCollection()
				.AddSingleton<RecruitmentViewModel>()
				.BuildServiceProvider();

			Ioc.Default.ConfigureServices(_serviceProvider);

			// プロセス終了時にサービスプロバイダを破棄する。
			AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

			InitializeComponent();
		}

		/// <summary>
		/// アプリケーション起動時の処理を行います。
		/// </summary>
		/// <param name="args"></param>
		protected override void OnLaunched(LaunchActivatedEventArgs args)
		{
			_window = new MainWindow();
			_window.Activate();
		}

		/// <summary>
		/// プロセス終了時にサービスプロバイダを破棄します。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnProcessExit(object? sender, EventArgs e)
		{
			_serviceProvider.Dispose();
		}
	}
}
