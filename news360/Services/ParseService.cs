using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using news360.Models;

namespace news360.Services
{
	internal class ParseService
	{
		public List<Summand> Parse(string equation)
		{
			var summands = ParseEquation(equation);

			var i = 0;
			while (i < summands.Count)
			{
				if (ContainBrackets(summands[i]))
				{
					var summand = summands[i];
					summands.RemoveAt(i);
					summands.AddRange(GetSimpleSummands(summand));
				}
				else
					i++;
			}
			return summands;
		}

		protected List<Summand> GetSimpleSummands(Summand summand)
		{
			for (var i = 0; i < summand.Variables.Count; i++)
			{
				if (summand.Variables[i].StartsWith("("))
				{
					var equation = summand.Variables[i].Substring(1, summand.Variables[i].Length-2);
					summand.Variables.RemoveAt(i);
					var bracket = ParseFormula(equation);
					bracket.ForEach(x =>
					{
						x.Factor *= summand.Factor;
						x.Variables.AddRange(summand.Variables);
					});

					return bracket;
				}
			}

			return new List<Summand> {summand};
		}

		protected List<Summand> ParseEquation(string equation)
		{
			var eqPos = equation.IndexOf('=');
			var left = equation.Substring(0, eqPos);
			var right = equation.Substring(eqPos + 1);

			var rigthSummand = ParseFormula(right);
			rigthSummand.ForEach(x => x.Factor *= -1);
			return ParseFormula(left).Concat(rigthSummand).ToList();
		}

		protected List<Summand> ParseFormula(string formula)
		{
			var split = Extensions.SplitFormula(formula);
			return split.Select(ParseSummand).ToList();
		}

		protected static Regex SummandRegex =new Regex(@"^(?<factor>([+-]?\d+(\.\d+)?)|[+-])?(?<vars>(\w|\(.+?\)){1}(\^\d+)?)+?$");

		protected static Regex VariableRegex = new Regex(@"^(?<var>(\w|\(.+?\))+)(\^(?<pow>\d+))?$");
		protected static Regex NumberRegex = new Regex(@"^(?<num>([+-]?\d+(\.\d+)?)|[+-])$");

		protected Summand ParseSummand(string str)
		{
			if (NumberRegex.IsMatch(str))
				return new Summand {Factor = float.Parse(str)};

			var groups = SummandRegex.Match(str).Groups;
			var result = new Summand
			{
				Factor = !groups["factor"].Success ? 1 : GetFactor(groups["factor"].Value)
			};

			for (var i = 0; i < groups["vars"].Captures.Count; i++)
			{
				var varStr = groups["vars"].Captures[i].Value;
				var match = VariableRegex.Match(varStr);
				var pow = match.Groups["pow"].Success ? int.Parse(match.Groups["pow"].Value) : 1;
				for (var j = 0; j < pow; j++)
					result.Variables.Add(match.Groups["var"].Value);
			}

			return result;
		}

		private float GetFactor(string value)
		{
			if (value == "+")
				return 1;
			if (value == "-")
				return -1;

			return float.Parse(value);
		}

		protected bool ContainBrackets(Summand summand) => summand.Variables.Any(v => v.StartsWith("("));
	}
}