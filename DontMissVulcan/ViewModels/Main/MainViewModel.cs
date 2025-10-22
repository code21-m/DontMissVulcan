using DontMissVulcan.ViewModels.TagSelection;

namespace DontMissVulcan.ViewModels.Main
{
	internal class MainViewModel
	{
		public TagSelectorViewModel TagSelector { get; } = new();
	}
}
