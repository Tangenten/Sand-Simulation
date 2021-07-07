using System;
using System.Threading;
using Raylib_cs;

namespace Helpers {
	public static class RandomH {
		private static ThreadLocal<Random> r = new ThreadLocal<Random>(() => new Random());

		public static float GetRandom(in float min, in float max) {
			return (float) (r.Value.NextDouble() * (max - min)) + min;
		}

		public static double GetRandom(in double min, in double max) {
			return r.Value.NextDouble() * (max - min) + min;
		}

		public static int GetRandom(in int min, in int max) {
			return r.Value.Next(min, max);
		}

		public static uint GetRandom(in uint min, in uint max) {
			return (uint) r.Value.Next((int) min, (int) max);
		}

		public static short GetRandom(in short min, in short max) {
			return (short) r.Value.Next(min, max);
		}

		public static byte GetRandom(in byte min, in byte max) {
			return (byte) r.Value.Next(min, max);
		}

		public static Color GetRandomColor() {
			return new Color((byte) GetRandom(0, 255), (byte) GetRandom(0, 255), (byte) GetRandom(0, 255), (byte) 255);
		}
	}
}