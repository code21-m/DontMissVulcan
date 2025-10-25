using CommunityToolkit.Mvvm.ComponentModel;
using DontMissVulcan.Models.Recruitment.Domain;

namespace DontMissVulcan.ViewModels.Recruitment.TagSelection
{
	/// <summary>
	/// タグ選択ボタンのViewModel
	/// </summary>
	/// <param name="tag">タグ</param>
	/// <param name="displayName">表示名</param>
	internal partial class TagItemViewModel(Tag tag, string displayName) : ObservableObject
	{
		/// <summary>
		/// タグ
		/// </summary>
		public Tag Tag { get; } = tag;

		/// <summary>
		/// 表示名
		/// </summary>
		public string DisplayName { get; } = displayName;

		/// <summary>
		/// 選択されているか
		/// </summary>
		[ObservableProperty]
		public partial bool IsSelected { get; set; }

		/// <summary>
		/// 選択可能か
		/// </summary>
		[ObservableProperty]
		public partial bool IsSelectable { get; set; } = true;
	}
}