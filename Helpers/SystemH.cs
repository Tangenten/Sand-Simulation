using System;
using System.Runtime;
using System.Threading;

namespace Helpers {
	public static class SystemH {
		private static Mutex GlobalLock;

		public static void ForceGarbageCollection() {
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		public static long GetGarbageSize() {
			return GC.GetTotalAllocatedBytes();
		}

		public static long GetMemorySize() {
			return GC.GetTotalMemory(false);
		}

		public static void SetGCLowLatency() {
			GCSettings.LatencyMode = GCLatencyMode.LowLatency;
		}

		public static void SetGCInteractive() {
			GCSettings.LatencyMode = GCLatencyMode.Interactive;
		}

		public static bool AppAlreadyRunning(in string GUID) {
			GlobalLock = new Mutex(false, GUID);
			return !GlobalLock.WaitOne(0, false);
		}
	}
}
