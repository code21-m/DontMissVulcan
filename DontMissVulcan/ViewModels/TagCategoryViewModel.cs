using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DontMissVulcan.ViewModels
{
	public partial class TagCategoryViewModel(string name, IEnumerable<TagItemViewModel> tags) : ObservableObject
	{
		public string Name { get; } = name;
		public ObservableCollection<TagItemViewModel> Tags { get; } = new ObservableCollection<TagItemViewModel>(tags);
	}
}
