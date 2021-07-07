using Raylib_cs;

namespace Helpers {
	public static class TweenH {
		public static float Linear(float x, float x0, float x1, float y0, float y1) {
			return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
		}

		public static void SmoothToTarget(ref float current, float target, float scalar) {
			current += (target - current) / scalar;
		}

		public static void SmoothToTarget(ref double current, double target, double scalar) {
			current += (target - current) / scalar;
		}

		public static float SmoothToTarget(float current, float target, float scalar) {
			current += (target - current) / scalar;
			return current;
		}

		public static void ExponentialSmoothing(ref float input, float scalar) {
			input *= scalar;
		}

		public static float ExponentialSmoothing(float input, float scalar) {
			input *= scalar;
			return input;
		}

		public static void LinearSmoothing(ref float input, float scalar) {
			input += scalar;
		}

		public static float LinearSmoothing(float input, float scalar) {
			input += scalar;
			return input;
		}

		public static Color ColorLerp(float x, float x0, float x1, Color c1, Color c2) {
			Color color = new Color();
			color.r = (byte) Linear(x, x0, x1, c1.r, c2.r);
			color.g = (byte) Linear(x, x0, x1, c1.g, c2.g);
			color.b = (byte) Linear(x, x0, x1, c1.b, c2.b);
			color.a = (byte) Linear(x, x0, x1, c1.a, c2.a);
			return color;
		}
	}
}