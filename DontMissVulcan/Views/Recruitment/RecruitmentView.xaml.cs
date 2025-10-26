using CommunityToolkit.Mvvm.DependencyInjection;
using DontMissVulcan.ViewModels.Recruitment;
using Microsoft.UI.Xaml.Controls;

namespace DontMissVulcan.Views.Recruitment
{
	/// <summary>
	/// ���J���l�c�[����View
	/// </summary>
	public sealed partial class RecruitmentView : Page
	{
		/// <summary>
		/// ���J���l�c�[����ViewModel
		/// </summary>
		internal RecruitmentViewModel ViewModel { get; }

		/// <summary>
		/// View�̏��������s���܂��B
		/// </summary>
		public RecruitmentView()
		{
			// DI�R���e�i������J���l�c�[����ViewModel���擾����B
			ViewModel = Ioc.Default.GetRequiredService<RecruitmentViewModel>();

			InitializeComponent();
		}
	}
}
