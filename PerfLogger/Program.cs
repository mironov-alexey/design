using System;
using System.Diagnostics;
using System.Linq;

namespace PerfLogger
{
    public class PerfomanceMeasurer : IDisposable
    {
        public Stopwatch Watch;
        private Action<long> printElapsedTime;
        public PerfomanceMeasurer(Action<long> inner)
        {
            printElapsedTime = inner;
            Watch = new Stopwatch();
            Watch.Start();
        }
        public void Dispose()
        {
            Watch.Stop();
            printElapsedTime(Watch.ElapsedMilliseconds);
        }
    }
    public class PerfLogger
    {
        public static IDisposable Measure(Action<long> printElapsedTime)
        {
            return new PerfomanceMeasurer(printElapsedTime);
        }
    }
	class Program
	{
		static void Main(string[] args)
		{
			var sum = 0.0;
			using (PerfLogger.Measure(t => Console.WriteLine("for: {0}", t)))
				for (var i = 0; i < 100000000; i++) sum += i;
			using (PerfLogger.Measure(t => Console.WriteLine("linq: {0}", t)))
				sum -= Enumerable.Range(0, 100000000).Sum(i => (double)i);
			Console.WriteLine(sum);
		}
	}
}
