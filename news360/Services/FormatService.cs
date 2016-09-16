using System;
using System.Collections.Generic;
using System.Linq;
using news360.Models;

namespace news360.Services
{
	internal class FormatService
	{
		public string Format(List<Summand> list)
		{
			var groupList = list.Select(GetVariableWithPower).ToList();

			var f = groupList
				.OrderBy(x => x.Variables.ListToString())
				.Select(Format)
				.ListToString();

			if (f.StartsWith("+"))
				f = f.Substring(1);
			if (f.Length == 0)
				f = "0";

			return f + "=0";
		}

		private Summand GetVariableWithPower(Summand node)
		{
			var variables = node.Variables
				.GroupBy(x => x)
				.Select(g => g.Key + (g.Count() > 1 ? $"^{g.Count()}" : ""))
				.OrderBy(x => x).ToList();

			return new Summand
			{
				Factor = node.Factor,
				Variables = variables
			};
		}

		protected string Format(Summand v)
		{
			if (v.Factor == 0)
				return "";

			var f = Math.Abs(v.Factor);
			var showFactor = f != 1 || v.Variables.Count == 0;
			var sign = v.Factor < 0 ? "-" : "+";

			return sign + (showFactor ? f.ToString() : "") + v.Variables.ListToString();
		}
	}
}