using DontMissVulcan.Models.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DontMissVulcan.ViewModels.Recruitment.TagSelection
{
	/// <summary>
	/// タグ選択機能のViewModel
	/// </summary>
	internal class TagSelectorViewModel
	{
		/// <summary>
		/// タグ選択ボタンのViewModel
		/// </summary>
		public ObservableCollection<TagItemViewModel> TagItems { get; } = [];

		/// <summary>
		/// カテゴリごとに分類されたタグ選択ボタンのViewModel
		/// </summary>
		public ObservableCollection<TagCategoryItemViewModel> TagCategoryItems { get; } = [];

		/// <summary>
		/// 選択されているタグ
		/// </summary>
		public IReadOnlySet<Tag> SelectedTags => TagItems.Where(tagItem => tagItem.IsSelected).Select(tagItem => tagItem.Tag).ToHashSet();

		/// <summary>
		/// タグ選択数上限
		/// </summary>
		private const int maxSelectable = 5;

		/// <summary>
		/// タグ選択機能の初期化を行います。
		/// </summary>
		/// <param name="gameData">ゲームデータ</param>
		public TagSelectorViewModel(GameData gameData)
		{
			foreach (Tag tag in Enum.GetValues(typeof(Tag)))
			{
				var tagItem = new TagItemViewModel(tag, gameData.TagToDisplayName[tag]);
				tagItem.PropertyChanged += TagItemSelectionChanged;
				TagItems.Add(tagItem);
			}
			TagCategoryItems.Add(new(TagItems.Where(tagItem => TagCategories.QualificationTags.Contains(tagItem.Tag))));
			TagCategoryItems.Add(new(TagItems.Where(tagItem => TagCategories.ClassTags.Contains(tagItem.Tag))));
			TagCategoryItems.Add(new(TagItems.Where(tagItem => TagCategories.PositionTags.Contains(tagItem.Tag))));
			TagCategoryItems.Add(new(TagItems.Where(tagItem => TagCategories.SpecializationTags.Contains(tagItem.Tag))));
		}

		/// <summary>
		/// タグ選択をまとめて設定します。
		/// </summary>
		/// <param name="tags">タグ</param>
		public void SetTags(IEnumerable<Tag> tags)
		{
			var _tags = tags.ToHashSet().Take(maxSelectable);
			foreach (var tagItem in TagItems)
			{
				tagItem.IsSelected = _tags.Contains(tagItem.Tag);
			}
		}

		/// <summary>
		/// タグ選択が変更されたときに選択ボタンの有効・無効設定を更新します。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TagItemSelectionChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != nameof(TagItemViewModel.IsSelected))
			{
				return;
			}

			UpdateSelectableStatus();
		}

		/// <summary>
		/// タグ選択数が上限に達しているかどうかによって選択ボタンの有効・無効を切り替えます。
		/// </summary>
		private void UpdateSelectableStatus()
		{
			foreach (var tagItem in TagItems)
			{
				// タグ選択数が上限未満なら有効、そうでなければ選択されているもの以外無効
				if (SelectedTags.Count < maxSelectable)
				{
					tagItem.IsSelectable = true;
				}
				else
				{
					tagItem.IsSelectable = tagItem.IsSelected;
				}
			}
		}
	}
}