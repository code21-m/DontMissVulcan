using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DontMissVulcan.ViewModels.TagSelection
{
	internal class TagCategoryViewModel(string name, IEnumerable<TagItemViewModel> tags) 
	{
		public string Name { get; } = name;

		public ObservableCollection<TagItemViewModel> TagItems { get; } = new ObservableCollection<TagItemViewModel>(tags);
	}
}
