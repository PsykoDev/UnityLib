using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LibUnity
{
	public static class SortingList
	{

		public static void Shuffle<T>(this System.Random rng, List<T> array) 															 
		{
			int n = array.Count;
			while (n > 1)
			{
				int k = rng.Next(n--);
				(array[k], array[n]) = (array[n], array[k]);
			}
		}

		public static List<T> RemoveDuplicates<T>(this List<T> items)
		{
			if (items.Count == 1)
				return items;

			var result = new List<T>();
			var set = new HashSet<T>();
			for (int i = 0; i < items.Count; i++)
			{
				if (!set.Contains(items[i]))
				{
					result.Add(items[i]);
					set.Add(items[i]);
				}
			}
			return result;
		}

		public static string ToCSV(this IList list)
		{
			var sb = new StringBuilder();
			foreach (var val in list)
			{
				sb.Append(val);
				if (list.IndexOf(val) < list.Count - 1) sb.Append(',');
			}
			return sb.ToString();
		}
	}
}