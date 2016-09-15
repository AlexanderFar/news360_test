using System;

namespace news360.WorkStrategies
{
	public class ConsoleStrategy :BaseStrategy
	{
		public override void Run()
		{
			while (true)
			{
				var equation  = Console.ReadLine();
				Console.WriteLine(Transform(equation));
			}
		}
	}
}