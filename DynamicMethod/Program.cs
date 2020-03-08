using DynamicMethod.Tests;
using System;

namespace DynamicMethod
{
	class Program
	{
		static void Main(string[] args)
		{
			var test = new InstantiationTest();
			Console.WriteLine("Press key to start tests");
			Console.ReadKey();
			var res = test.Measure();

			Console.WriteLine("MethodA: " + res.Item1 + "ms");
			Console.WriteLine("MethodB: " + res.Item2 + "ms");
			Console.WriteLine("MethodC: " + res.Item3 + "ms");

			Console.ReadKey();
		}
	}


}
