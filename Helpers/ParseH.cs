using System.Globalization;

namespace Helpers {
	public static class ParseH {
		public static bool? StringToBool(in string text) {
			if (text.ToUpper() == "TRUE" || text == "1") return true;

			if (text.ToUpper() == "FALSE" || text == "0") return false;

			return null;
		}

		public static int? StringToInt(in string text) {
			int i;
			if (int.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out i)) return i;

			return null;
		}

		public static float? StringToFloat(in string text) {
			float i;
			if (float.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out i)) return i;

			return null;
		}

		public static double? StringToDouble(in string text) {
			double i;
			if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out i)) return i;

			return null;
		}
	}
}