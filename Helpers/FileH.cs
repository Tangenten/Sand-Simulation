using System.IO;

namespace Helpers {
	public static class FileH {
		public static bool FileExists(in string filePath) {
			if (File.Exists(filePath)) return true;

			return false;
		}

		public static void CreateFileIfNotExists(in string filePath) {
			if (!File.Exists(filePath))
				using (FileStream fileStream = File.Create(filePath)) {
				}
		}
	}
}