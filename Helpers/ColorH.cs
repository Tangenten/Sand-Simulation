using Raylib_cs;

namespace Helpers {
	public static class ColorH {
		public static Color HSLToRGB(double h, double s, double l, byte a) {
			byte r = 0;
			byte g = 0;
			byte b = 0;

			if (s == 0) {
				r = g = b = (byte) (l * 255.0);
			} else {
				float v1, v2;
				float hue = (float) ((float) h / 360.0);

				v2 = (float) (l < 0.5 ? l * (1.0 + s) : l + s - l * s);
				v1 = (float) (2.0 * l - v2);

				r = (byte) (255 * HueToRGB(v1, v2, (float) (hue + 1.0f / 3.0)));
				g = (byte) (255 * HueToRGB(v1, v2, hue));
				b = (byte) (255 * HueToRGB(v1, v2, (float) (hue - 1.0f / 3.0)));
			}

			return new Color(r, g, b, a);
		}

		public static float RGBToLuma(uint red, uint green, uint blue) {
			return (float) (red / 255.0 * 0.3 + green / 255.0 * 0.59 + blue / 255.0 * 0.11);
		}

		private static float HueToRGB(float v1, float v2, float vH) {
			if (vH < 0)
				vH += 1;

			if (vH > 1)
				vH -= 1;

			if (6 * vH < 1)
				return v1 + (v2 - v1) * 6 * vH;

			if (2 * vH < 1)
				return v2;

			if (3 * vH < 2)
				return v1 + (v2 - v1) * (2.0f / 3 - vH) * 6;

			return v1;
		}
	}
}