using DontMissVulcan.Models.Recruitment.Domain;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace DontMissVulcan.ViewModels.Recruitment.TagSelection
{
	internal class TagSelectorViewModel
	{
		public ObservableCollection<TagCategoryViewModel> TagCategories { get; } = [];

		public ObservableCollection<Tag> SelectedTags { get; } = [];

		public TagSelectorViewModel(GameData gameData)
		{
			TagCategories.Add(new TagCategoryViewModel("レア", Models.Recruitment.Domain.TagCategories.QualificationTags.Select(tag => new TagItemViewModel(tag, gameData.TagToDisplayName[tag]))));
			TagCategories.Add(new TagCategoryViewModel("職業", Models.Recruitment.Domain.TagCategories.ClassTags.Select(tag => new TagItemViewModel(tag, gameData.TagToDisplayName[tag]))));
			TagCategories.Add(new TagCategoryViewModel("ポジション", Models.Recruitment.Domain.TagCategories.PositionTags.Select(tag => new TagItemViewModel(tag, gameData.TagToDisplayName[tag]))));
			TagCategories.Add(new TagCategoryViewModel("専門", Models.Recruitment.Domain.TagCategories.SpecializationTags.Select(tag => new TagItemViewModel(tag, gameData.TagToDisplayName[tag]))));

			foreach (var tagItem in TagCategories.SelectMany(tagCategory => tagCategory.TagItems))
			{
				tagItem.PropertyChanged += TagItemIsSelectedChanged;
			}
			SelectedTags.CollectionChanged += SelectedTagsChanged;
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

			if (changedTagItem.IsSelected && !SelectedTags.Contains(changedTagItem.Tag))
			{
				SelectedTags.Add(changedTagItem.Tag);
			}
			else
			{
				SelectedTags.Remove(changedTagItem.Tag);
			}
		}

		private void SelectedTagsChanged(object? sender, NotifyCollectionChangedEventArgs e)
		{
			var tagItems = TagCategories.SelectMany(tagCategory => tagCategory.TagItems);

			foreach (var tagItem in tagItems.Where(tagItem => SelectedTags.Contains(tagItem.Tag)))
			{
				tagItem.IsSelected = true;
			}

			const int maxSelectable = 5;
			if (SelectedTags.Count >= maxSelectable)
			{
				var unselectedTagItems = tagItems.Where(tagItem => !tagItem.IsSelected);
				foreach (var tag in unselectedTagItems)
				{
					tag.IsSelectable = false;
				}
			}
			else
			{
				foreach (var tag in tagItems)
				{
					tag.IsSelectable = true;
				}
			}
		}
	}
}