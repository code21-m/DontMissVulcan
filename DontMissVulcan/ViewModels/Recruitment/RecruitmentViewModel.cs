using DontMissVulcan.Models.Recruitment.Data;
using DontMissVulcan.Models.Recruitment.Matching;
using DontMissVulcan.ViewModels.Recruitment.Matching;
using DontMissVulcan.ViewModels.Recruitment.TagSelection;
using DontMissVulcan.ViewModels.Recruitment.WindowSelection;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace DontMissVulcan.ViewModels.Recruitment
{
	internal class RecruitmentViewModel
	{
		public WindowSelectorViewModel WindowSelector { get; }

		public TagSelectorViewModel TagSelector { get; }

		public MatchResultsViewModel MatchResults { get; }

		private readonly MatchFinder _matchFinder;

		public RecruitmentViewModel()
		{
			var assetsDir = Path.Combine(AppContext.BaseDirectory, "Assets");
			var gameData = GameDataLoader.Load(Path.Combine(assetsDir, "TagToDisplayName.json"), Path.Combine(assetsDir, "Operators.json"));

			_matchFinder = new(gameData);

			WindowSelector = new();
			TagSelector = new(gameData);
			MatchResults = new(gameData);

			foreach (var tagItem in TagSelector.TagItems)
			{
				tagItem.PropertyChanged += TagItemIsSelectedChanged;
			}
		}

		private void TagItemIsSelectedChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != nameof(TagItemViewModel.IsSelected))
			{
				return;
			}

			var selectedTags = TagSelector.TagItems.Where(tagItem => tagItem.IsSelected).Select(tagItem => tagItem.Tag);
			var matches = _matchFinder.FindAllMathes(selectedTags);
			var matchClassification = MatchClassifier.ClassifyMatches(matches);
			MatchResults.SetResults(matchClassification);
		}
	}
}
