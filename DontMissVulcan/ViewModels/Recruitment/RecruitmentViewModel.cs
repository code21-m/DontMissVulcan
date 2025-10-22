using DontMissVulcan.Models.Data;
using DontMissVulcan.ViewModels.TagSelection;
using System;
using System.IO;

namespace DontMissVulcan.ViewModels.Main
{
	internal class RecruitmentViewModel
	{
		public TagSelectorViewModel TagSelector { get; }

		public RecruitmentViewModel()
		{
			var assetsDir = Path.Combine(AppContext.BaseDirectory, "Assets");
			var gameData = GameDataLoader.Load(Path.Combine(assetsDir, "TagToDisplayName.json"), Path.Combine(assetsDir, "Operators.json"));
			TagSelector = new TagSelectorViewModel(gameData);
		}
	}
}
