using System;
using Helpers;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

namespace Sand {
	internal enum ParticleType {
		EMPTY,
		SAND,
		WATER,
		STONE
	}

	internal struct Particle {
		public ParticleType Type;
		public bool Updated;

		public Particle(ParticleType type) {
			this.Type = type;
			this.Updated = false;
		}
	}

	internal static class ParticleFunctions {
		public static void UpdateSand(ref Particle[,] particles, in int heightIndex, in int widthIndex) {
			Span<(int, int)> positions = stackalloc (int, int)[] {
				GetUnder(ref particles, heightIndex, widthIndex),
				GetUnderLeft(ref particles, heightIndex, widthIndex),
				GetUnderRight(ref particles, heightIndex, widthIndex)
			};

			for (int i = 0; i < positions.Length; i++) {
				if (
					particles[positions[i].Item1, positions[i].Item2].Type == ParticleType.EMPTY ||
					particles[positions[i].Item1, positions[i].Item2].Type == ParticleType.WATER &&
					particles[positions[i].Item1, positions[i].Item2].Updated == false
				) {
					particles[heightIndex, widthIndex].Type = particles[positions[i].Item1, positions[i].Item2].Type;

					particles[positions[i].Item1, positions[i].Item2].Type = ParticleType.SAND;
					particles[positions[i].Item1, positions[i].Item2].Updated = true;

					return;
				}
			}
		}

		public static void UpdateWater(ref Particle[,] particles, in int heightIndex, in int widthIndex) {
			Span<(int, int)> positions = stackalloc (int, int)[] {
				GetUnder(ref particles, heightIndex, widthIndex),
				GetUnderLeft(ref particles, heightIndex, widthIndex),
				GetUnderRight(ref particles, heightIndex, widthIndex),
				GetLeft(ref particles, heightIndex, widthIndex),
				GetRight(ref particles, heightIndex, widthIndex)
			};

			for (int i = 0; i < positions.Length; i++) {
				if (particles[positions[i].Item1, positions[i].Item2].Type == ParticleType.EMPTY && particles[positions[i].Item1, positions[i].Item2].Updated == false) {
					particles[positions[i].Item1, positions[i].Item2].Type = ParticleType.WATER;
					particles[positions[i].Item1, positions[i].Item2].Updated = true;

					particles[heightIndex, widthIndex].Type = ParticleType.EMPTY;

					return;
				}
			}
		}

		public static void SpawnType(ref Particle[,] particles, ParticleType type, int screenWidth, int screenHeight) {
			int xPos = (int) TweenH.Linear(GetMousePosition().X, 0, screenWidth, 0, particles.GetLength(1) - 1);
			int yPos = (int) TweenH.Linear(GetMousePosition().Y, 0, screenHeight, 0, particles.GetLength(0) - 1);

			for (int i = 0; i < 32; i++) {
				int x = Math.Clamp(xPos + RandomH.GetRandom(-4, 4), 0, particles.GetLength(1) - 1);
				int y = Math.Clamp(yPos + RandomH.GetRandom(-4, 4), 0, particles.GetLength(0) - 1);

				particles[y, x].Type = type;
			}
		}

		public static void PlaceType(ref Particle[,] particles, ParticleType type, int screenWidth, int screenHeight) {
			int xPos = (int) TweenH.Linear(GetMousePosition().X, 0, screenWidth, 0, particles.GetLength(1) - 1);
			int yPos = (int) TweenH.Linear(GetMousePosition().Y, 0, screenHeight, 0, particles.GetLength(0) - 1);

			for (int i = 0; i < 6; i++) {
				int x = Math.Clamp(xPos + i, 0, particles.GetLength(1) - 1);
				for (int j = 0; j < 6; j++) {
					int y = Math.Clamp(yPos + j, 0, particles.GetLength(0) - 1);

					particles[y, x].Type = type;
				}
			}
		}

		private static (int, int) GetUnder(ref Particle[,] particles, in int heightIndex, in int widthIndex) {
			return (MathH.ModRangeClamp(heightIndex + 1, 0, particles.GetLength(0) - 1), widthIndex);
		}

		private static (int, int) GetUnderLeft(ref Particle[,] particles, in int heightIndex, in int widthIndex) {
			return (MathH.ModRangeClamp(heightIndex + 1, 0, particles.GetLength(0) - 1), MathH.ModRangeClamp(widthIndex - 1, 0, particles.GetLength(1) - 1));
		}

		private static (int, int) GetUnderRight(ref Particle[,] particles, in int heightIndex, in int widthIndex) {
			return (MathH.ModRangeClamp(heightIndex + 1, 0, particles.GetLength(0) - 1), MathH.ModRangeClamp(widthIndex + 1, 0, particles.GetLength(1) - 1));
		}

		private static (int, int) GetOver(ref Particle[,] particles, in int heightIndex, in int widthIndex) {
			return (MathH.ModRangeClamp(heightIndex - 1, 0, particles.GetLength(0) - 1), widthIndex);
		}

		private static (int, int) GetOverLeft(ref Particle[,] particles, in int heightIndex, in int widthIndex) {
			return (MathH.ModRangeClamp(heightIndex - 1, 0, particles.GetLength(0) - 1), MathH.ModRangeClamp(widthIndex - 1, 0, particles.GetLength(1) - 1));
		}

		private static (int, int) GetOverRight(ref Particle[,] particles, in int heightIndex, in int widthIndex) {
			return (MathH.ModRangeClamp(heightIndex - 1, 0, particles.GetLength(0) - 1), MathH.ModRangeClamp(widthIndex + 1, 0, particles.GetLength(1) - 1));
		}

		private static (int, int) GetRight(ref Particle[,] particles, in int heightIndex, in int widthIndex) {
			return (heightIndex, MathH.ModRangeClamp(widthIndex + 1, 0, particles.GetLength(1) - 1));
		}

		private static (int, int) GetLeft(ref Particle[,] particles, in int heightIndex, in int widthIndex) {
			return (heightIndex, MathH.ModRangeClamp(widthIndex - 1, 0, particles.GetLength(1) - 1));
		}
	}

	internal class Program {
		private static void Main(string[] args) {
			int screenWidth = 1920;
			int screenHeight = 1080;

			SetConfigFlags(ConfigFlag.FLAG_WINDOW_RESIZABLE);
			InitWindow(screenWidth, screenHeight, "Smooth");

			int particleWidth = screenWidth / 6;
			int particleHeight = screenHeight / 6;

			float particleScaleWidth = screenWidth / particleWidth;
			float particleScaleHeight = screenHeight / particleHeight;

			Particle[,] particles = new Particle[particleHeight, particleWidth];

			for (int i = 0; i < particleHeight; i++) {
				for (int j = 0; j < particleWidth; j++) {
					particles[i, j] = new Particle(ParticleType.EMPTY);
				}
			}

			int frameCount = 0;
			while (!WindowShouldClose()) {
				float deltaTime = GetFrameTime();
				frameCount++;

				if (IsWindowResized()) {
					screenWidth = GetScreenWidth();
					screenHeight = GetScreenHeight();
				}

				if (IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON)) ParticleFunctions.SpawnType(ref particles, ParticleType.SAND, screenWidth, screenHeight);

				if (IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON)) ParticleFunctions.SpawnType(ref particles, ParticleType.WATER, screenWidth, screenHeight);

				if (IsMouseButtonDown(MouseButton.MOUSE_MIDDLE_BUTTON)) ParticleFunctions.PlaceType(ref particles, ParticleType.STONE, screenWidth, screenHeight);

				if (frameCount % 1 == 0) particles[particleHeight / 2, particleWidth / 2].Type = ParticleType.SAND;

				for (int i = particleHeight - 1; i >= 0; i--) {
					for (int j = particleWidth - 1; j >= 0; j--) {
						if (!particles[i, j].Updated)
							switch (particles[i, j].Type) {
								case ParticleType.EMPTY:
									break;
								case ParticleType.SAND:
									ParticleFunctions.UpdateSand(ref particles, i, j);
									break;
								case ParticleType.WATER:
									ParticleFunctions.UpdateWater(ref particles, i, j);
									break;
								case ParticleType.STONE:
									break;
							}
					}
				}

				/*Parallel.ForEach(Partitioner.Create(0, particleHeight-1, 8), range => {
					for (int i = range.Item2-1; i >= range.Item1; i--) {
						for (int j = particleWidth-1; j >= 0; j--) {
							if (!particles[i, j].Updated) {
								switch (particles[i, j].Type) {
									case ParticleType.EMPTY:
										break;
									case ParticleType.SAND:
										ParticleFunctions.UpdateSand(ref particles, i, j);
										break;
									case ParticleType.WATER:
										ParticleFunctions.UpdateWater(ref particles, i, j);
										break;
									case ParticleType.STONE:
										break;
								}
							}
						}
					}
				});*/

				BeginDrawing();
				ClearBackground(BLACK);

				for (int i = 0; i < particleHeight; i++) {
					for (int j = 0; j < particleWidth; j++) {
						particles[i, j].Updated = false;

						switch (particles[i, j].Type) {
							case ParticleType.EMPTY:
								break;
							case ParticleType.SAND:
								DrawRectangle((int) (j * particleScaleWidth), (int) (i * particleScaleHeight), (int) particleScaleWidth, (int) particleScaleHeight, new Color(223, 247, 92, 255));
								break;
							case ParticleType.WATER:
								DrawRectangle((int) (j * particleScaleWidth), (int) (i * particleScaleHeight), (int) particleScaleWidth, (int) particleScaleHeight, new Color(116, 177, 247, 255));
								break;
							case ParticleType.STONE:
								DrawRectangle((int) (j * particleScaleWidth), (int) (i * particleScaleHeight), (int) particleScaleWidth, (int) particleScaleHeight, new Color(38, 32, 51, 255));
								break;
						}
					}
				}

				DrawFPS(10, 10);
				EndDrawing();
			}

			CloseWindow();
		}
	}
}
