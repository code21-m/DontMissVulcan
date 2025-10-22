using DontMissVulcan.Models.Recruitment.Domain;
using System.Collections.ObjectModel;
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

			foreach (var tagCategory in TagCategories)
			{
				foreach (var tagItem in tagCategory.TagItems)
				{
					tagItem.PropertyChanged += TagSelectionChanged;
				}
			}
		}

		private void TagSelectionChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != nameof(TagItemViewModel.IsSelected))
			{
				return;
			}
			if (sender is not TagItemViewModel changedTagItem)
			{
				return;
			}

			const int maxSelectable = 5;
			if (changedTagItem.IsSelected)
			{
				SelectedTags.Add(changedTagItem.Tag);
				if (SelectedTags.Count == maxSelectable)
				{
					var unselectedTagItems = TagCategories
						.SelectMany(tagCategory => tagCategory.TagItems)
						.Where(tagItem => !tagItem.IsSelected);
					foreach (var tag in unselectedTagItems)
					{
						tag.IsSelectable = false;
					}
				}
			}
			else
			{
				SelectedTags.Remove(changedTagItem.Tag);
				if (SelectedTags.Count == maxSelectable - 1)
				{
					var otherUnselectedTagItems = TagCategories
						.SelectMany(tagCategory => tagCategory.TagItems)
						.Where(tagItem => !tagItem.IsSelected && tagItem != changedTagItem);
					foreach (var tag in otherUnselectedTagItems)
					{
						tag.IsSelectable = true;
					}
				}
			}
		}
	}
}