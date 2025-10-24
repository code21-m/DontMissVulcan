using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DontMissVulcan.ViewModels.Recruitment.TagSelection
{
	internal class TagCategoryItemViewModel(string name, IEnumerable<TagItemViewModel> tags) 
	{
		public string Name { get; } = name;

		public ObservableCollection<TagItemViewModel> TagItems { get; } = new(tags);
	}
}
