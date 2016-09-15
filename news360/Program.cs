using System;
using System.Windows.Forms;
using news360.Services;
using news360.WorkStrategies;

namespace news360
{
	internal class Program
	{

		private static void Main(string[] args)
		{
			var strategy = GetStrategies(args);
			strategy?.Run();
		}

		private static IWorkStrategy GetStrategies(string[] args)
		{
			if (args.Length > 0 && args[0] == "-c")
				return new ConsoleStrategy();
			else if (args.Length > 1 && args[0] == "-f")
				return new FileStrategy(args[1]);
			else
			{
				PrintHelpAndExit();
				return null;
			}
		}

		private static void PrintHelpAndExit()
		{
			const string help = @"usage: news360 -c | -f <filePath>";
			Console.Write(help);
		}
	}
}