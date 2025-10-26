using CommunityToolkit.Mvvm.DependencyInjection;
using DontMissVulcan.ViewModels.Recruitment;
using Microsoft.UI.Xaml.Controls;

namespace DontMissVulcan.Views.Recruitment
{
	public sealed partial class RecruitmentView : Page
	{
		internal RecruitmentViewModel ViewModel { get; }

		public RecruitmentView()
		{
			ViewModel = Ioc.Default.GetRequiredService<RecruitmentViewModel>();

			InitializeComponent();
		}
	}
}
