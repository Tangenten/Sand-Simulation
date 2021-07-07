using System;

namespace Helpers {
	public static class AudioH {
		public static float AudioRMS(in short[] samples) {
			float sum = 0f;
			for (int i = 0; i < samples.Length; i++) {
				sum += Math.Abs((int) samples[i]);
			}

			return sum / samples.Length / short.MaxValue;
		}
	}
}