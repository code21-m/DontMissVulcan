using DontMissVulcan.ViewModels.Main;
using Microsoft.UI.Xaml.Controls;

namespace DontMissVulcan.Views
{
	public sealed partial class MainView : UserControl
	{
		internal MainViewModel ViewModel { get; } = new();

		public MainView()
		{
			InitializeComponent();
		}
	}
}
