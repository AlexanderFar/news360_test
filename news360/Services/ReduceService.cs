using System.Collections.Generic;
using System.Linq;
using news360.Models;

namespace news360.Services
{
	internal class ReduceService
	{
		public List<Summand> Reduce(List<Summand> list)
		{
			var orderedVarList = list.Select(GetWithOrderedVar);
			var groups = orderedVarList.GroupBy(x => x.Variables.ListToString("|"));

			return groups.Select(ReduceGroup).ToList();
		}

		private Summand ReduceGroup(IEnumerable<Summand> group)
		{
			return new Summand
			{
				Factor = group.Sum(x => x.Factor),
				Variables = group.First().Variables
			};
		}

		private Summand GetWithOrderedVar(Summand node)
		{
			return new Summand
			{
				Factor = node.Factor,
				Variables = node.Variables.OrderBy(x => x).ToList()
			};
		}
	}
}