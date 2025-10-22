using CommunityToolkit.Mvvm.ComponentModel;
using DontMissVulcan.Models.Domain;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;

namespace DontMissVulcan.ViewModels
{
	public partial class TagSelectorViewModel : ObservableObject
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
				var totalSelected = Categories.SelectMany(c => c.Tags).Count(t => t.IsSelected);
				if (totalSelected > 5)
				{
					_isHandlingTagSelectionChange = true;
					changed.IsSelected = false;
					_isHandlingTagSelectionChange = false;
					return;
				}
				SelectedTags.Add(changed.Tag);
			}
			else
			{
				SelectedTags.Remove(changed.Tag);
			}
		}
	}
}