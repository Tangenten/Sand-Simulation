namespace Helpers {
	public static class MathH {
		public static int Mod(in int x, in int m) {
			int r = x % m;
			return r < 0 ? r + m : r;
		}

		public static float Mod(float x, float m) {
			float r = x % m;
			return r < 0 ? r + m : r;
		}

		public static int ModRange(int x, int min, int max) {
			return ((x - min) % (max - min) + (max - min)) % (max - min) + min;
		}

		public static float ModRange(in float x, float min, float max) {
			return ((x - min) % (max - min) + (max - min)) % (max - min) + min;
		}

		public static int ModRangeClamp(int x, int min, int max) {
			if (x <= min) return min;

			if (x >= max) return max;

			return ModRange(x, min, max);
		}
	}
}