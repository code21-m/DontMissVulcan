using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontMissVulcan.Models
{
	internal static class IEnumerableExtensions
	{
		public static IEnumerable<IEnumerable<T>> EnumerateCombinations<T>(this IEnumerable<T> items, int r)
		{
			if (r == 0)
			{
				yield return Enumerable.Empty<T>();
			}
			else
			{
				var i = 1;
				foreach (var item in items)
				{
					var unUsedItems = items.Skip(i);
					foreach (var subCombination in EnumerateCombinations(unUsedItems, r - 1))
					{
						yield return subCombination.Prepend(item);
					}
					i++;
				}
			}
		}
	}
}
