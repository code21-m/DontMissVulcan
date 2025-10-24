using DontMissVulcan.ViewModels.Recruitment;
using Microsoft.UI.Xaml.Controls;

namespace DontMissVulcan.Views.Recruitment
{
	public sealed partial class RecruitmentView : Page
	{
		internal RecruitmentViewModel ViewModel { get; } = new();

		public RecruitmentView()
		{
			InitializeComponent();
		}
	}
}
