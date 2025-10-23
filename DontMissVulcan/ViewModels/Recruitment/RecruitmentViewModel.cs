using DontMissVulcan.Models.Recruitment.Data;
using DontMissVulcan.Models.Recruitment.Matching;
using DontMissVulcan.ViewModels.Recruitment.Matching;
using DontMissVulcan.ViewModels.Recruitment.TagSelection;
using System;
using System.Collections.Specialized;
using System.IO;

namespace DontMissVulcan.ViewModels.Recruitment
{
	internal class RecruitmentViewModel
	{
		public TagSelectorViewModel TagSelector { get; }

		public MatchResultsViewModel MatchResults { get; }

		private readonly MatchFinder _matchFinder;

		public RecruitmentViewModel()
		{
			var assetsDir = Path.Combine(AppContext.BaseDirectory, "Assets");
			var gameData = GameDataLoader.Load(Path.Combine(assetsDir, "TagToDisplayName.json"), Path.Combine(assetsDir, "Operators.json"));

			_matchFinder = new MatchFinder(gameData);

			TagSelector = new TagSelectorViewModel(gameData);
			MatchResults = new MatchResultsViewModel(gameData);

			TagSelector.SelectedTags.CollectionChanged += SelectedTags_CollectionChanged;
		}

		private void SelectedTags_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
		{
			var matches = _matchFinder.FindAllMathes(TagSelector.SelectedTags);
			var matchClassification = MatchClassifier.ClassifyMatches(matches);
			MatchResults.SetResults(matchClassification);
		}
	}
}
