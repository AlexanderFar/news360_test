using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using news360.Models;
using news360.Services;

namespace news360.Tests.Services
{
	[TestClass]
	public class ReduceServiceTests
	{
		[TestMethod]
		public void one_var_tow_time()
		{
			// arrange
			var summands = new List<Summand>
			{
				new Summand {Factor = -1, Variables = new List<string> {"x"}},
				new Summand {Factor = 1, Variables = new List<string> {"x"}}
			};
			var expected = new List<Summand>
			{
				new Summand {Factor = 0, Variables = new List<string> {"x"}}
			};

			// act
			var actual = new ReduceService().Reduce(summands);


			// assert
			actual.ShouldAllBeEquivalentTo(expected);
		}

		[TestMethod]
		public void tow_var_time()
		{
			// arrange
			var summands = new List<Summand>
			{
				new Summand {Factor = -1, Variables = new List<string> {"x"}},
				new Summand {Factor = 1, Variables = new List<string> {"y"}}
			};
			var expected = new List<Summand>
			{
				new Summand {Factor = -1, Variables = new List<string> {"x"}},
				new Summand {Factor = 1, Variables = new List<string> {"y"}}
			};

			// act
			var actual = new ReduceService().Reduce(summands);


			// assert
			actual.ShouldAllBeEquivalentTo(expected);
		}

		[TestMethod]
		public void towMix_var_time()
		{
			// arrange
			var summands = new List<Summand>
			{
				new Summand {Factor = -1, Variables = new List<string> {"x"}},
				new Summand {Factor = 7, Variables = new List<string> {"y"}},
				new Summand {Factor = 1, Variables = new List<string> {"xxy"}},
				new Summand {Factor = 2, Variables = new List<string> {"xxy"}}
			};
			var expected = new List<Summand>
			{
				new Summand {Factor = -1, Variables = new List<string> {"x"}},
				new Summand {Factor = 7, Variables = new List<string> {"y"}},
				new Summand {Factor = 3, Variables = new List<string> {"xxy"}}
			};

			// act
			var actual = new ReduceService().Reduce(summands);


			// assert
			actual.ShouldAllBeEquivalentTo(expected);
		}
	}
}