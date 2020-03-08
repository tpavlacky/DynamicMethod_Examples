using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMethod
{
	public class PerformanceResult
	{
		public int MillisecondsA { get; set; }
		public int MillisecondsB { get; set; }

		public PerformanceResult(int msA, int msB)
		{
			MillisecondsA = msA;
			MillisecondsB = msB;
		}
	}


	public class PerformanceTest
	{
		private const int DEFAULT_REPETITIONS = 10;
		public string Name { get; set; }
		public string Description { get; set; }
		public int Iterations { get; set; }


		protected virtual bool MeasureTestA()
		{
			return false;
		}

		protected virtual bool MeasureTestB()
		{
			return false;
		}

		protected virtual bool MeasureTestC()
		{
			return false;
		}

		public PerformanceTest(string name, string description, int iterations)
		{
			Name = name;
			Description = description;
			Iterations = iterations;
		}

		public Tuple<int, int, int> Measure()
		{
			long totalA = 0, totalB = 0, totalC = 0;
			var stopWatch = new Stopwatch();

			for (long i = 0; i < DEFAULT_REPETITIONS; i++)
			{
				stopWatch.Restart();
				var impl = MeasureTestA();
				stopWatch.Stop();
				if (impl)
				{
					totalA += stopWatch.ElapsedMilliseconds;
				}
			}

			for (long i = 0; i < DEFAULT_REPETITIONS; i++)
			{
				stopWatch.Restart();
				var impl = MeasureTestB();
				stopWatch.Stop();
				if (impl)
				{
					totalB+= stopWatch.ElapsedMilliseconds;
				}
			}

			for (long i = 0; i < DEFAULT_REPETITIONS; i++)
			{
				stopWatch.Restart();
				var impl = MeasureTestC();
				stopWatch.Stop();
				if (impl)
				{
					totalC += stopWatch.ElapsedMilliseconds;
				}
			}

			return Tuple.Create<int, int, int>((int)totalA / DEFAULT_REPETITIONS, (int)totalB / DEFAULT_REPETITIONS, (int)totalC / DEFAULT_REPETITIONS);
		}


	}
}
