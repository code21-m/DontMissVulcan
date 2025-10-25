using System;
using System.Collections.Generic;
using System.Linq;

namespace DontMissVulcan.Models.Extensions
{
	/// <summary>
	/// IEnumerableの拡張メソッド
	/// </summary>
	public static class IEnumerableExtensions
	{
		/// <summary>
		/// 組み合わせを列挙します。
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="size">選ぶ数</param>
		/// <returns>組み合わせ</returns>
		public static IEnumerable<IEnumerable<T>> EnumerateCombinations<T>(this IEnumerable<T> source, int size)
		{
			ArgumentOutOfRangeException.ThrowIfNegative(size);

			if (size == 0)
			{
				yield return Enumerable.Empty<T>();
			}
			else
			{
				var i = 1;
				foreach (var item in source)
				{
					var unusedItems = source.Skip(i);
					foreach (var subCombination in EnumerateCombinations(unusedItems, size - 1))
					{
						yield return subCombination.Prepend(item);
					}
					i++;
				}
			}
		}
	}
}
