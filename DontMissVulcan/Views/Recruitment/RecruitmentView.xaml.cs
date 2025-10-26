using CommunityToolkit.Mvvm.DependencyInjection;
using DontMissVulcan.ViewModels.Recruitment;
using Microsoft.UI.Xaml.Controls;

namespace DontMissVulcan.Views.Recruitment
{
	/// <summary>
	/// 公開求人ツールのView
	/// </summary>
	public sealed partial class RecruitmentView : Page
	{
		/// <summary>
		/// 公開求人ツールのViewModel
		/// </summary>
		internal RecruitmentViewModel ViewModel { get; }

		/// <summary>
		/// Viewの初期化を行います。
		/// </summary>
		public RecruitmentView()
		{
			// DIコンテナから公開求人ツールのViewModelを取得する。
			ViewModel = Ioc.Default.GetRequiredService<RecruitmentViewModel>();

			InitializeComponent();
		}
	}
}
