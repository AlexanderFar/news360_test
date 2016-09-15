using System.Text.RegularExpressions;

namespace news360.Services
{
	internal class ValidateService
	{
		protected static Regex SplitByEqSymbolRegex = new Regex(@"^(?<left>([+-^\(\)]|\w)+)=(?<right>([+-^\(\)]|\w)+)$");
		protected static Regex NodeRegex = new Regex(@"^(([+-]?\d+(\.\d+)?)|[+-])?((\w|(?<br>\(.+?\))){1}(\^\d+)?)+?$");

		public bool Validate(string equation)
		{
			if (string.IsNullOrWhiteSpace(equation) || !SplitByEqSymbolRegex.IsMatch(equation))
				return false;

			var formulas = SplitByEqSymbolRegex.Match(equation).Groups;
			return ValidateFormula(formulas["left"].Value) && ValidateFormula(formulas["right"].Value);
		}

		private bool ValidateFormula(string formula)
		{
			var split = Extensions.SplitFormula(formula);
			foreach (var node in split)
			{
				if (!NodeRegex.IsMatch(node))
					return false;
				var br = NodeRegex.Match(node).Groups["br"];
				for (var i = 0; i < br.Captures.Count; i++)
				{
					var f = br.Captures[i].Value;
					if (!ValidateFormula(f.Substring(1, f.Length - 2)))
						return false;
				}
			}
			return true;
		}
	}
}