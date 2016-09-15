using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using news360.Models;
using news360.Services;

namespace news360.Tests.Services
{
	[TestClass]
	public class ParseServiceTests
	{
		[TestMethod]
		public void zero_var()
		{
			// arrange
			var equation = "6=-0";
			var expected = new List<Summand>
			{
				new Summand {Factor = 6, Variables = new List<string>()},
				new Summand {Factor = 0, Variables = new List<string>()}
			};

			// act
			var actual = new ParseService().Parse(equation);


			// assert
			actual.ShouldAllBeEquivalentTo(expected);
		}

		[TestMethod]
		public void one_var()
		{
			// arrange
			var equation = "-x=-1";
			var expected = new List<Summand>
			{
				new Summand {Factor = 1, Variables = new List<string>()},
				new Summand {Factor = -1, Variables = new List<string> {"x"}}
			};

			// act
			var actual = new ParseService().Parse(equation);


			// assert
			actual.ShouldAllBeEquivalentTo(expected);
		}

		[TestMethod]
		public void one_var_tow_time()
		{
			// arrange
			var equation = "x-x=+3x";
			var expected = new List<Summand>
			{
				new Summand {Factor = -1, Variables = new List<string> {"x"}},
				new Summand {Factor = -3, Variables = new List<string> {"x"}},
				new Summand {Factor = 1, Variables = new List<string> {"x"}}
			};

			// act
			var actual = new ParseService().Parse(equation);


			// assert
			actual.ShouldAllBeEquivalentTo(expected);
		}


		[TestMethod]
		public void minus_one_var()
		{
			// arrange
			var equation = "-x=0";
			var expected = new List<Summand>
			{
				new Summand {Factor = 0, Variables = new List<string>()},
				new Summand {Factor = -1, Variables = new List<string> {"x"}}
			};

			// act
			var actual = new ParseService().Parse(equation);


			// assert
			actual.ShouldAllBeEquivalentTo(expected);
		}

		[TestMethod]
		public void start_from_minus()
		{
			// arrange
			var equation = "-3x=-7";
			var expected = new List<Summand>
			{
				new Summand {Factor = 7, Variables = new List<string>()},
				new Summand {Factor = -3, Variables = new List<string> {"x"}}
			};

			// act
			var actual = new ParseService().Parse(equation);


			// assert
			actual.ShouldAllBeEquivalentTo(expected);
		}

		[TestMethod]
		public void tow_vars_equation()
		{
			// arrange
			var equation = "1x+2y+3xy-4yx+5x^2+6xy^2+100=7x^2y-8x^2y^3-101";
			var expected = new List<Summand>
			{
				new Summand {Factor = 100, Variables = new List<string>()},
				new Summand {Factor = 101, Variables = new List<string>()},
				new Summand {Factor = 1, Variables = new List<string> {"x"}},
				new Summand {Factor = 2, Variables = new List<string> {"y"}},
				new Summand {Factor = 3, Variables = new List<string> {"x", "y"}},
				new Summand {Factor = -4, Variables = new List<string> {"x", "y"}},
				new Summand {Factor = 5, Variables = new List<string> {"x", "x"}},
				new Summand {Factor = 6, Variables = new List<string> {"x", "y", "y"}},
				new Summand {Factor = -7, Variables = new List<string> {"x", "x", "y"}},
				new Summand {Factor = 8, Variables = new List<string> {"x", "x", "y", "y", "y"}}
			};

			// act
			var actual = new ParseService().Parse(equation);

			// assert
			actual.ShouldAllBeEquivalentTo(expected);
		}

		[TestMethod]
		public void three_vars_equation()
		{
			// arrange
			var equation = "1x+2y+3xyz=z";
			//var equation = "100=-101";
			var expected = new List<Summand>
			{
				new Summand {Factor = 1, Variables = new List<string> {"x"}},
				new Summand {Factor = 2, Variables = new List<string> {"y"}},
				new Summand {Factor = 3, Variables = new List<string> {"x", "y", "z"}},
				new Summand {Factor = -1, Variables = new List<string> {"z"}}
			};

			// act
			var actual = new ParseService().Parse(equation);

			// assert
			actual.ShouldAllBeEquivalentTo(expected);
		}


		[TestMethod]
		public void brackets_vars_equation()
		{
			// arrange
			var equation = "2y(3x-1)=0";
			var expected = new List<Summand>
			{
				new Summand {Factor = 0, Variables = new List<string>()},
				new Summand {Factor = -2, Variables = new List<string> {"y"}},
				new Summand {Factor = 6, Variables = new List<string> {"y", "x"}}
			};

			// act
			var actual = new ParseService().Parse(equation);

			// assert
			actual.ShouldAllBeEquivalentTo(expected);
		}

		[TestMethod]
		public void brackets2_vars_equation()
		{
			// arrange
			var equation = "3x(y-z)=0";
			var expected = new List<Summand>
			{
				new Summand {Factor = 0, Variables = new List<string>()},
				new Summand {Factor = 3, Variables = new List<string> {"x", "y"}},
				new Summand {Factor = -3, Variables = new List<string> {"x", "z"}}
			};

			// act
			var actual = new ParseService().Parse(equation);

			// assert
			actual.ShouldAllBeEquivalentTo(expected);
		}

		[TestMethod]
		public void brackets3_vars_equation()
		{
			// arrange
			var equation = "1x+2y(3x-1)+3x(y-z)=4(2x^2)z";
			var expected = new List<Summand>
			{
				new Summand {Factor = 1, Variables = new List<string> {"x"}},
				new Summand {Factor = -2, Variables = new List<string> {"y"}},
				new Summand {Factor = 6, Variables = new List<string> {"y", "x"}},
				new Summand {Factor = 3, Variables = new List<string> {"x", "y"}},
				new Summand {Factor = -3, Variables = new List<string> {"x", "z"}},
				new Summand {Factor = -8, Variables = new List<string> {"x", "x", "z"}}
			};

			// act
			var actual = new ParseService().Parse(equation);

			// assert
			actual.ShouldAllBeEquivalentTo(expected);
		}

		[TestMethod]
		public void double_brackets_vars_equation()
		{
			// arrange
			var equation = "2x(3y-z)(5z-x)=4(2x^2)z^2";
			var expected = new List<Summand>
			{
				new Summand {Factor = 30, Variables = new List<string> {"x", "y", "z"}},
				new Summand {Factor = -10, Variables = new List<string> {"x", "z", "z"}},
				new Summand {Factor = -6, Variables = new List<string> {"x", "y", "x"}},
				new Summand {Factor = 2, Variables = new List<string> {"x", "x", "z"}},
				new Summand {Factor = -8, Variables = new List<string> {"x", "x", "z", "z"}}
			};

			// act
			var actual = new ParseService().Parse(equation);

			// assert
			actual.ShouldAllBeEquivalentTo(expected);
		}


		[TestMethod]
		public void nested_brackets_vars_equation()
		{
			// arrange
			var equation = "2x(3y-z(5z-x))=-4(2x^2(-z^2))";
			var expected = new List<Summand>
			{
				new Summand {Factor = 6, Variables = new List<string> {"x", "y"}},
				new Summand {Factor = -10, Variables = new List<string> {"x", "z", "z"}},
				new Summand {Factor = 2, Variables = new List<string> {"x", "x", "z"}},
				new Summand {Factor = -8, Variables = new List<string> {"x", "x", "z", "z"}}
			};

			// act
			var actual = new ParseService().Parse(equation);

			// assert
			actual.ShouldAllBeEquivalentTo(expected);
		}

		[TestMethod]
		public void bracketsWithPower_vars_equation()
		{
			// arrange
			var equation = "2a(b)(3c^2)(d^2)^2(e+1)f=0";
			var expected = new List<Summand>
			{
				new Summand {Factor = 0, Variables = new List<string>()},
				new Summand {Factor = 6, Variables = new List<string> {"a", "b", "c", "c", "d", "d", "d", "d", "e", "f"}},
				new Summand {Factor = 6, Variables = new List<string> {"a", "b", "c", "c", "d", "d", "d", "d", "f"}}
			};

			// act
			var actual = new ParseService().Parse(equation);

			// assert
			actual.ShouldAllBeEquivalentTo(expected);
		}
	}
}