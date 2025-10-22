using DontMissVulcan.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace DontMissVulcan.Views
{
	public sealed partial class TagSelectorView : UserControl
	{
		public TagSelectorViewModel ViewModel { get; } = new();

		public TagSelectorView()
		{
			InitializeComponent();
		}
	}
}
