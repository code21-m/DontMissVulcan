using DontMissVulcan.Models.Data;
using DontMissVulcan.ViewModels.TagSelection;

namespace DontMissVulcan.ViewModels.Main
{
	internal class MainViewModel
	{
		public TagSelectorViewModel TagSelector { get; }

		public MainViewModel()
		{
			var gameData = GameDataLoader.Load(
				@"C:\Users\raspb\Desktop\Work Space\DontMissVulcan\DontMissVulcan\Assets\TagToDisplayName.json",
				@"C:\Users\raspb\Desktop\Work Space\DontMissVulcan\DontMissVulcan\Assets\Operators.json");
			TagSelector = new TagSelectorViewModel(gameData);
		}
	}
}
