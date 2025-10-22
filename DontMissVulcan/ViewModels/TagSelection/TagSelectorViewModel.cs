using DontMissVulcan.Models.Domain;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DontMissVulcan.ViewModels.TagSelection
{
	internal class TagSelectorViewModel
	{
		public ObservableCollection<TagCategoryViewModel> TagCategories { get; } = [];

		public ObservableCollection<Tag> SelectedTags { get; } = [];

		public TagSelectorViewModel()
		{
			TagCategories.Add(new TagCategoryViewModel("Qualification", Models.Domain.TagCategories.QualificationTags.Select(tag => new TagItemViewModel(tag, tag.ToString()))));
			TagCategories.Add(new TagCategoryViewModel("Class", Models.Domain.TagCategories.ClassTags.Select(tag => new TagItemViewModel(tag, tag.ToString()))));
			TagCategories.Add(new TagCategoryViewModel("Position", Models.Domain.TagCategories.PositionTags.Select(tag => new TagItemViewModel(tag, tag.ToString()))));
			TagCategories.Add(new TagCategoryViewModel("Specialization", Models.Domain.TagCategories.SpecializationTags.Select(tag => new TagItemViewModel(tag, tag.ToString()))));

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