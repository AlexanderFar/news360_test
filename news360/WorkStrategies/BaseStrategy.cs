using System;
using news360.Services;

namespace news360.WorkStrategies
{
	public abstract class BaseStrategy : IWorkStrategy
	{
		private static readonly ValidateService ValidateService = new ValidateService();
		private static readonly ParseService ParseService = new ParseService();
		private static readonly ReduceService ReduceService = new ReduceService();
		private static readonly FormatService FormatService = new FormatService();

		protected string Transform(string equation)
		{
			try
			{
				equation = equation?.Replace(" ", "");
				if (!ValidateService.Validate(equation))
					return "equation is not valid";

				var summands = ReduceService.Reduce(ParseService.Parse(equation));

				return FormatService.Format(summands);
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}

		public abstract void Run();
	}
}