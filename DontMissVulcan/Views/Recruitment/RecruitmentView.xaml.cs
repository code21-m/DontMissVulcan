using DontMissVulcan.ViewModels.Main;
using Microsoft.UI.Xaml.Controls;

namespace DontMissVulcan.Views.Recruitment
{
	public sealed partial class RecruitmentView : UserControl
	{
		internal RecruitmentViewModel ViewModel { get; } = new();

		public RecruitmentView()
		{
			InitializeComponent();
		}
	}
}
