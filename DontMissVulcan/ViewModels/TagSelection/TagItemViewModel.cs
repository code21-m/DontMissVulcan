using CommunityToolkit.Mvvm.ComponentModel;
using DontMissVulcan.Models.Domain;

namespace DontMissVulcan.ViewModels.TagSelection
{
	internal partial class TagItemViewModel(Tag tag, string displayName) : ObservableObject
	{
		public Tag Tag { get; } = tag;
		public string DisplayName { get; } = displayName;
		[ObservableProperty]
		public partial bool IsSelected { get; set; }
		[ObservableProperty]
		public partial bool IsSelectable { get; set; } = true;
	}
}