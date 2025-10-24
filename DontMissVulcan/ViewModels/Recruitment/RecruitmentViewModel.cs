using CommunityToolkit.Mvvm.Input;
using DontMissVulcan.Models.Extensions;
using DontMissVulcan.Models.Platform;
using DontMissVulcan.Models.Recruitment.Data;
using DontMissVulcan.Models.Recruitment.Matching;
using DontMissVulcan.Models.Recruitment.TagResolution;
using DontMissVulcan.ViewModels.Recruitment.Matching;
using DontMissVulcan.ViewModels.Recruitment.TagSelection;
using DontMissVulcan.ViewModels.Recruitment.WindowSelection;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DontMissVulcan.ViewModels.Recruitment
{
	internal partial class RecruitmentViewModel
	{
		public WindowSelectorViewModel WindowSelector { get; }

		public TagSelectorViewModel TagSelector { get; }

		public MatchResultsViewModel MatchResults { get; }

		private readonly OcrTextRecognizer _ocrTextRecognizer;

		private readonly TagResolver _tagResolver;

		private readonly MatchFinder _matchFinder;

		public RecruitmentViewModel()
		{
			var assetsDir = Path.Combine(AppContext.BaseDirectory, "Assets");
			var gameData = GameDataLoader.Load(Path.Combine(assetsDir, "TagToDisplayName.json"), Path.Combine(assetsDir, "Operators.json"));

			_ocrTextRecognizer = new(new("ja-JP"));
			_tagResolver = new(gameData);
			_matchFinder = new(gameData);

			WindowSelector = new();
			TagSelector = new(gameData);
			MatchResults = new(gameData);

			foreach (var tagItem in TagSelector.TagItems)
			{
				tagItem.PropertyChanged += TagItemIsSelectedChanged;
			}
		}

		[RelayCommand]
		public async Task RecognizeTags()
		{
			var hWnd = WindowSelector.SelectedWindowHwnd;
			if (hWnd == IntPtr.Zero)
			{
				return;
			}

			using var bitmap = ScreenCapturer.CaptureWindow(hWnd);
			using var softwareBitmap = bitmap.ToSoftwareBitmap();
			var texts = await _ocrTextRecognizer.RecognizeAsync(softwareBitmap);
			var tags = _tagResolver.ResolveTags(texts);
			TagSelector.SetTags(tags);
		}

		private void TagItemIsSelectedChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != nameof(TagItemViewModel.IsSelected))
			{
				return;
			}

			var matches = _matchFinder.FindAllMathes(TagSelector.SelectedTags);
			var matchClassification = MatchClassifier.ClassifyMatches(matches);
			MatchResults.SetResults(matchClassification);
		}
	}
}
