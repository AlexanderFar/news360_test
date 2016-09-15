using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using news360.Services;

namespace news360.Tests.Services
{
	[TestClass]
	public class ValidateServiceTests
	{
		[TestMethod]
		public void valid_equation()
		{
			// arrange
			var equations = new[]
			{
				"6=-0",
				"x=0",
				"-x=0",
				"-3x=-7",
				"1x+2y+3xy-4yx+5x^2+6xy^2+100=7x^2y-8x^2y^3-101",
				"1x+2y+3xyz=z",
				"2y(3x-1)=0",
				"3x(y-z)=0",
				"1x+2y(3x-1)+3x(y-z)=4(2x^2)z",
				"2x(3y-z)(5z-x)=4(2x^2)z^2",
				"2x(3y-z(5z-x))=-4(2x^2(-z^2))",
				"2a(b)(3c^2)(d^2)^2(e+1)f=0",
				"1x+2xy^3=0"
			};
			var expected = new bool[equations.Length];
			for (int i = 0; i < expected.Length; i++)
				expected[i] = true;

			var sut = new ValidateService();
			

			// act
			var result = equations.Select(sut.Validate);

			// assert
			result.ShouldAllBeEquivalentTo(expected);
		}

		[TestMethod]
		public void not_valid_equation()
		{
			// arrange
			var equations = new[]
			{
				null,
				"",
				"=",
				"6!=-0",
				"x=>0",
				"-x=^0",
				"-3,2x=-7",
				"323z",
				"1x+2y+3xyz=",
				"=0",
				"3-=0",
				"1x=9-",
				"1x-3y",
				"2x(3y-z!(5z-x))=-4(2x^2(-z^2))",
				"2x(3y-z!(5z-x-))=-4(2x^2(-z^2))",
				"2x(x)(3y-z(5z-x-(^)))=-4(2x^2(-z^2))",
				"2a(b)(3c^2-)(d^2)^2(e+1)f=0"
			};
			var expected = new bool[equations.Length];
			var sut = new ValidateService();


			// act
			var result = equations.Select(sut.Validate);

			// assert
			result.ShouldAllBeEquivalentTo(expected);
		}

	}
}