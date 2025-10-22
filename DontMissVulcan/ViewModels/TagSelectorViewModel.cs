using CommunityToolkit.Mvvm.ComponentModel;
using DontMissVulcan.Models.Domain;
using System.Collections.ObjectModel;
using System.Linq;

namespace DontMissVulcan.ViewModels
{
	public partial class TagSelectorViewModel : ObservableObject
	{
		public ObservableCollection<TagCategoryViewModel> Categories { get; } = [];

		public TagSelectorViewModel()
		{
			Categories.Add(new TagCategoryViewModel("Qualification", TagCategories.QualificationTags.Select(t => new TagItemViewModel(t, t.ToString()))));
			Categories.Add(new TagCategoryViewModel("Class", TagCategories.ClassTags.Select(t => new TagItemViewModel(t, t.ToString()))));
			Categories.Add(new TagCategoryViewModel("Position", TagCategories.PositionTags.Select(t => new TagItemViewModel(t, t.ToString()))));
			Categories.Add(new TagCategoryViewModel("Specialization", TagCategories.SpecializationTags.Select(t => new TagItemViewModel(t, t.ToString()))));
		}
	}
}