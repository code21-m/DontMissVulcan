using CommunityToolkit.Mvvm.Input;
using DontMissVulcan.Models.Platform;
using DontMissVulcan.Models.Recruitment.Data;
using DontMissVulcan.Models.Recruitment.Matching;
using DontMissVulcan.Models.Recruitment.TagResolution;
using DontMissVulcan.ViewModels.Recruitment.Matching;
using DontMissVulcan.ViewModels.Recruitment.TagSelection;
using DontMissVulcan.ViewModels.Recruitment.WindowSelection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace DontMissVulcan.ViewModels.Recruitment
{
	/// <summary>
	/// 公開求人ツールのViewModel
	/// </summary>
	internal partial class RecruitmentViewModel : IDisposable
	{
		/// <summary>
		/// ウィンドウ選択機能のViewModel
		/// </summary>
		public WindowSelectorViewModel WindowSelector { get; }

		/// <summary>
		/// タグ選択機能のViewModel
		/// </summary>
		public TagSelectorViewModel TagSelector { get; }

		/// <summary>
		/// マッチング結果表示のViewModel
		/// </summary>
		public MatchResultsViewModel MatchResults { get; }

		/// <summary>
		/// OCRテキスト認識器
		/// </summary>
		private readonly OcrTextRecognizer _ocrTextRecognizer;

		/// <summary>
		/// タグ解決器
		/// </summary>
		private readonly TagResolver _tagResolver;

		/// <summary>
		/// マッチ検索器
		/// </summary>
		private readonly MatchFinder _matchFinder;

		private bool _disposed;

		/// <summary>
		/// 公開求人ツールの初期化を行います。
		/// </summary>
		public RecruitmentViewModel()
		{
			var assetsDir = Path.Combine(AppContext.BaseDirectory, "Assets");
			var gameData = GameDataLoader.Load(Path.Combine(assetsDir, "TagToDisplayName.json"), Path.Combine(assetsDir, "Operators.json"));

			_ocrTextRecognizer = new();
			_tagResolver = new(gameData);
			_matchFinder = new(gameData);

			WindowSelector = new();
			TagSelector = new(gameData);
			MatchResults = new(gameData);

			foreach (var tagItem in TagSelector.TagItems)
			{
				tagItem.PropertyChanged += TagItemSelectionChanged;
			}
		}

		/// <summary>
		/// タグ選択が変更されたときにマッチ検索を行います。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TagItemSelectionChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != nameof(TagItemViewModel.IsSelected))
			{
				return;
			}

			FindMatches();
		}

		/// <summary>
		/// 選択されているタグからマッチを検索します。
		/// </summary>
		private void FindMatches()
		{
			var matches = _matchFinder.FindAllMathes(TagSelector.SelectedTags);
			var matchClassification = MatchClassifier.ClassifyMatches(matches);
			MatchResults.SetResults(matchClassification);
		}

		/// <summary>
		/// 選択されたウィンドウに対してタグの認識を試みます。
		/// </summary>
		/// <returns></returns>
		[RelayCommand]
		public async Task RecognizeTags()
		{
			var hWnd = WindowSelector.SelectedWindowHwnd;
			if (hWnd == IntPtr.Zero)
			{
				return;
			}

			// ウィンドウをアクティブにしてからキャプチャする。
			WindowInterop.SetForegroundWindow(hWnd);
			IReadOnlyList<string> texts;
			try
			{
				using var bitmap = ScreenCapturer.CaptureWindow(hWnd);
				texts = await _ocrTextRecognizer.RecognizeAsync(bitmap);
			}
			catch
			{
				// キャプチャに失敗した場合はなにもしない。
				return;
			}
			var tags = _tagResolver.ResolveTags(texts);
			TagSelector.SetTags(tags);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					// TODO: マネージド状態を破棄します (マネージド オブジェクト)
					_ocrTextRecognizer.Dispose();
				}

				// TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
				// TODO: 大きなフィールドを null に設定します
				_disposed = true;
			}
		}

		// // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
		// ~RecruitmentViewModel()
		// {
		//     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
