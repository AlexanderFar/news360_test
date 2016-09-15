using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace news360
{
	internal static class Extensions
	{
		public static List<string> SplitFormula(string formula)
		{
			if (string.IsNullOrEmpty(formula))
				return new List<string>();

			var list = new List<string>();
			var last = 0;
			var brackets = 0;
			for (var i = 1; i < formula.Length; i++)
			{
				if (formula[i] == ')')
					brackets--;
				if (brackets > 0) continue;

				if (formula[i] == '(') brackets++;

				if (formula[i] == '+' || formula[i] == '-')
				{
					list.Add(formula.Substring(last, i - last));
					last = i;
				}
			}
			list.Add(formula.Substring(last));
			return list;
		}

		public static string ToStringEx<T>(this IEnumerable<T> list, string separator = "", string perfIfNotNull = "")
		{
			var enumerable = list as T[] ?? list.ToArray();
			if (list == null || !enumerable.Any())
				return string.Empty;

			if (separator == null) separator = string.Empty;

			var r =
				enumerable.Aggregate(new StringBuilder(), (s, i) => s.Append(separator + i)).Remove(0, separator.Length).ToString();

			return string.IsNullOrEmpty(perfIfNotNull) ? r : perfIfNotNull + r;
		}
	}
}