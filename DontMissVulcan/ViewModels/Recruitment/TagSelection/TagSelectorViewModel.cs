using DontMissVulcan.Models.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DontMissVulcan.ViewModels.Recruitment.TagSelection
{
	internal class TagSelectorViewModel
	{
		public ObservableCollection<TagItemViewModel> TagItems { get; } = [];

		public ObservableCollection<TagCategoryItemViewModel> TagCategoryItems { get; } = [];

		public IReadOnlySet<Tag> SelectedTags => TagItems.Where(tagItem => tagItem.IsSelected).Select(tagItem => tagItem.Tag).ToHashSet();

		private const int maxSelectable = 5;

		public TagSelectorViewModel(GameData gameData)
		{
			foreach (Tag tag in Enum.GetValues(typeof(Tag)))
			{
				var tagItem = new TagItemViewModel(tag, gameData.TagToDisplayName[tag]);
				tagItem.PropertyChanged += TagItemIsSelectedChanged;
				TagItems.Add(tagItem);
			}
			TagCategoryItems.Add(new("レア", TagItems.Where(tagItem => TagCategories.QualificationTags.Contains(tagItem.Tag))));
			TagCategoryItems.Add(new("職業", TagItems.Where(tagItem => TagCategories.ClassTags.Contains(tagItem.Tag))));
			TagCategoryItems.Add(new("ポジション", TagItems.Where(tagItem => TagCategories.PositionTags.Contains(tagItem.Tag))));
			TagCategoryItems.Add(new("専門", TagItems.Where(tagItem => TagCategories.SpecializationTags.Contains(tagItem.Tag))));
		}

		public void SetTags(IEnumerable<Tag> tags)
		{
			var _tags = tags.ToHashSet().Take(maxSelectable);
			foreach (var tagItem in TagItems)
			{
				var isSelected = _tags.Contains(tagItem.Tag);
				if (tagItem.IsSelected != isSelected)
				{
					tagItem.IsSelected = isSelected;
				}
			}
		}

		private void TagItemIsSelectedChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != nameof(TagItemViewModel.IsSelected))
			{
				return;
			}
			if (sender is not TagItemViewModel changedTagItem)
			{
				return;
			}

			if (SelectedTags.Count < maxSelectable)
			{
				foreach (var tagItem in TagItems)
				{
					if (!tagItem.IsSelectable)
					{
						tagItem.IsSelectable = true;
					}
				}
			}
			else
			{
				var selectedTagItems = TagItems.Where(tagItem => tagItem.IsSelected);
				var unselectedTagItems = TagItems.Where(tagItem => !tagItem.IsSelected);
				foreach (var tagItem in TagItems)
				{
					if (tagItem.IsSelectable != tagItem.IsSelected)
					{
						tagItem.IsSelectable = tagItem.IsSelected;
					}
				}
			}
		}
	}
}