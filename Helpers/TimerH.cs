using System;
using System.Diagnostics;
using System.Threading;

namespace Helpers {
	public static class TimerH {
		private static ThreadLocal<Stopwatch> Stopwatch = new ThreadLocal<Stopwatch>(() => new Stopwatch());

		public static void StartTimer() {
			Stopwatch.Value.Start();
		}

		public static void PrintTimer() {
			if (Stopwatch.Value.IsRunning)
				Console.WriteLine(Stopwatch.Value.Elapsed.TotalSeconds);
			else
				Console.WriteLine("Stopwatch not running");
		}

		public static void RestartTimer() {
			if (Stopwatch.Value.IsRunning) {
				Stopwatch.Value.Stop();
				Console.WriteLine(Stopwatch.Value.Elapsed.TotalSeconds);
				Stopwatch.Value.Restart();
			} else {
				Console.WriteLine("Stopwatch not running");
			}
		}

		public static void StopTimer() {
			if (Stopwatch.Value.IsRunning) {
				Stopwatch.Value.Stop();
				Console.WriteLine(Stopwatch.Value.Elapsed.TotalSeconds);
				Stopwatch.Value.Reset();
			} else {
				Console.WriteLine("Stopwatch not running");
			}
		}
	}
}