using CommunityToolkit.Mvvm.ComponentModel;
using DontMissVulcan.Models.Recruitment.Domain;

namespace DontMissVulcan.ViewModels.Recruitment.TagSelection
{
	/// <summary>
	/// �^�O�I���{�^����ViewModel
	/// </summary>
	/// <param name="tag">�^�O</param>
	/// <param name="displayName">�\����</param>
	internal partial class TagItemViewModel(Tag tag, string displayName) : ObservableObject
	{
		/// <summary>
		/// �^�O
		/// </summary>
		public Tag Tag { get; } = tag;

		/// <summary>
		/// �\����
		/// </summary>
		public string DisplayName { get; } = displayName;

		/// <summary>
		/// �I������Ă��邩
		/// </summary>
		[ObservableProperty]
		public partial bool IsSelected { get; set; }

		/// <summary>
		/// �I���\��
		/// </summary>
		[ObservableProperty]
		public partial bool IsSelectable { get; set; } = true;
	}
}