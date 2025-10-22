using DontMissVulcan.Models.Domain;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace DontMissVulcan.ViewModels.TagSelection
{
	internal class TagSelectorViewModel
	{

		public ObservableCollection<TagCategoryViewModel> TagCategories { get; } = [];
		public ObservableCollection<Tag> SelectedTags { get; } = [];

		public TagSelectorViewModel()
		{
			TagCategories.Add(new TagCategoryViewModel("Qualification", Models.Domain.TagCategories.QualificationTags.Select(t => new TagItemViewModel(t, t.ToString()))));
			TagCategories.Add(new TagCategoryViewModel("Class", Models.Domain.TagCategories.ClassTags.Select(t => new TagItemViewModel(t, t.ToString()))));
			TagCategories.Add(new TagCategoryViewModel("Position", Models.Domain.TagCategories.PositionTags.Select(t => new TagItemViewModel(t, t.ToString()))));
			TagCategories.Add(new TagCategoryViewModel("Specialization", Models.Domain.TagCategories.SpecializationTags.Select(t => new TagItemViewModel(t, t.ToString()))));

			foreach (var category in TagCategories)
			{
				foreach (var tag in category.TagItems)
				{
					tag.PropertyChanged += TagSelectionChanged;
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
					Debug.WriteLine(unselectedTagItems.Count());
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
					Debug.WriteLine(otherUnselectedTagItems.Count());
					foreach (var tag in otherUnselectedTagItems)
					{
						tag.IsSelectable = true;
					}
				}
			}
		}
	}
}