using System;
using System.IO;

namespace news360.WorkStrategies
{
	public class FileStrategy : BaseStrategy
	{
		private readonly string _filePath;

		public FileStrategy(string filePath)
		{
			_filePath = filePath;
		}

		public override void Run()
		{
			if (!File.Exists(_filePath))
			{
				Console.WriteLine("File not found");
				return;
			}

			var equations = File.ReadAllLines(_filePath);
			var answers = new string[equations.Length];
			for (var i = 0; i < equations.Length; i++)
				answers[i] = Transform(equations[i]);

			File.WriteAllLines(_filePath + ".out", answers);
		}
	}
}