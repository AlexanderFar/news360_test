using System.Collections.Generic;

namespace news360.Models
{
	internal class Summand
	{
		public float Factor { get; set; }
		public List<string> Variables { get; set; } = new List<string>();
	}
}