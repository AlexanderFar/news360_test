using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using news360.Models;
using news360.Services;

namespace news360.Tests.Services
{
	[TestClass]
	public class FormatServiceTests
	{
		[TestMethod]
		public void First_one()
		{
			// arrange
			var equation = new List<Summand>
			{
				new Summand {Factor = 1, Variables = new List<string>()},
				new Summand {Factor = -1, Variables = new List<string> {"x"}}
			};

			// act
			var actual = new FormatService().Format(equation);


			// assert
			actual.ShouldBeEquivalentTo("1-x=0");
		}

		[TestMethod]
		public void zero_vars()
		{
			// arrange
			var equation = new List<Summand>();

			// act
			var actual = new FormatService().Format(equation);

			// assert
			actual.ShouldBeEquivalentTo("0=0");
		}

		[TestMethod]
		public void power_vars()
		{
			// arrange
			var equation = new List<Summand>
			{
				new Summand {Factor = -1, Variables = new List<string> {"x","x","x"}}
			};

			// act
			var actual = new FormatService().Format(equation);

			// assert
			actual.ShouldBeEquivalentTo("-x^3=0");
		}

		[TestMethod]
		public void first_minus()
		{
			// arrange
			var equation = new List<Summand>
			{
				new Summand {Factor = -1, Variables = new List<string> {"x"}}
			};

			// act
			var actual = new FormatService().Format(equation);

			// assert
			actual.ShouldBeEquivalentTo("-x=0");
		}

		[TestMethod]
		public void first_minus_forConst()
		{
			// arrange
			var equation = new List<Summand>
			{
				new Summand {Factor = -10, Variables = new List<string> ()}
			};

			// act
			var actual = new FormatService().Format(equation);

			// assert
			actual.ShouldBeEquivalentTo("-10=0");
		}



		[TestMethod]
		public void first_plus_forConst()
		{
			// arrange
			var equation = new List<Summand>
			{
				new Summand {Factor = 10, Variables = new List<string> ()}
			};

			// act
			var actual = new FormatService().Format(equation);

			// assert
			actual.ShouldBeEquivalentTo("10=0");
		}

		[TestMethod]
		public void first_plus()
		{
			// arrange
			var equation = new List<Summand>
			{
				new Summand {Factor = 10, Variables = new List<string> {"x"} }
			};

			// act
			var actual = new FormatService().Format(equation);

			// assert
			actual.ShouldBeEquivalentTo("10x=0");
		}
	}
}