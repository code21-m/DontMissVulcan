using DontMissVulcan.Models.Domain;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DontMissVulcan.ViewModels.TagSelection
{
	internal class TagSelectorViewModel
	{

		public ObservableCollection<TagCategoryViewModel> Categories { get; } = [];
		public ObservableCollection<Tag> SelectedTags { get; } = [];

		public TagSelectorViewModel()
		{
			Categories.Add(new TagCategoryViewModel("Qualification", TagCategories.QualificationTags.Select(t => new TagItemViewModel(t, t.ToString()))));
			Categories.Add(new TagCategoryViewModel("Class", TagCategories.ClassTags.Select(t => new TagItemViewModel(t, t.ToString()))));
			Categories.Add(new TagCategoryViewModel("Position", TagCategories.PositionTags.Select(t => new TagItemViewModel(t, t.ToString()))));
			Categories.Add(new TagCategoryViewModel("Specialization", TagCategories.SpecializationTags.Select(t => new TagItemViewModel(t, t.ToString()))));

			foreach (var category in Categories)
			{
				foreach (var tag in category.Tags)
				{
					tag.PropertyChanged += TagSelectionChanged;
				}
			}
		}

		private bool _isHandlingTagSelectionChange;
		private void TagSelectionChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != nameof(TagItemViewModel.IsSelected))
			{
				return;
			}
			if (_isHandlingTagSelectionChange)
			{
				return;
			}
			if (sender is not TagItemViewModel changed)
			{
				return;
			}

			if (changed.IsSelected)
			{
				if (SelectedTags.Count < 5)
				{
					SelectedTags.Add(changed.Tag);
				}
				else
				{
					_isHandlingTagSelectionChange = true;
					changed.IsSelected = false;
					_isHandlingTagSelectionChange = false;
				}
			}
			else
			{
				SelectedTags.Remove(changed.Tag);
			}
		}
	}
}