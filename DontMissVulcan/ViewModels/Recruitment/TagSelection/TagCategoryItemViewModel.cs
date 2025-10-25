using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DontMissVulcan.ViewModels.Recruitment.TagSelection
{
	/// <summary>
	/// カテゴリごとに分類されたタグ選択ボタンのViewModel
	/// </summary>
	/// <param name="name">カテゴリ名</param>
	/// <param name="tags">タグ</param>
	internal class TagCategoryItemViewModel(string name, IEnumerable<TagItemViewModel> tags) 
	{
		/// <summary>
		/// カテゴリ名
		/// </summary>
		public string Name { get; } = name;

		/// <summary>
		/// タグ選択ボタンのViewModel
		/// </summary>
		public ObservableCollection<TagItemViewModel> TagItems { get; } = new(tags);
	}
}
