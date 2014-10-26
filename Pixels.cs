using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ColorBox
{
	public static class Pixels
	{
		public static Color BackColor { get; private set; }

		static Pixels() {
			BackColor = Color.Green;
		}
		public static Color[] GetColors(Texture2D bmp) {
			int i = 0;
			Color[] c = new Color[bmp.Width * bmp.Height];
			for (int x = 0; x < bmp.Width; x++) {
				for (int y = 0; y < bmp.Height; y++) {
					c[i] = bmp.GetPixel(x, y);
					i++;
				}
			}
			return c;
		}
		public static bool IntersectPixels(Rectangle RectA, Color[] ColorA, Rectangle RectB, Color[] ColorB)
		{
			// Find the bounds of the rectangle intersection
			int top = (int)Math.Max(RectA.Top, RectB.Top);
			int bottom = (int)Math.Min(RectA.Bottom, RectB.Bottom);
			int left = (int)Math.Max(RectA.Left, RectB.Left);
			int right = (int)Math.Min(RectA.Right, RectB.Right);

			// Check every point within the intersection bounds
			for (int y = top; y < bottom; y++)
			{
				for (int x = left; x < right; x++)
				{
					// Get the color of both pixels at this point
					Color colorA = ColorA[(x - (int)RectA.Left) +
						(y - (int)RectA.Top) * (int)RectA.Width];
					Color colorB = ColorB[(x - (int)RectB.Left) +
						(y - (int)RectB.Top) * (int)RectB.Width];

					// If both pixels are not completely transparent,
					if (colorA.A != 0 && colorB.A != 0)
					{
						// then an intersection has been found
						BackColor = Color.Red;
						return true;
					}
				}
			}

			// No intersection found
			BackColor = Color.Green;
			return false;
		}
		public static bool IntersectPixels(
			Matrix transformA, int widthA, int heightA, Color[] dataA,
			Matrix transformB, int widthB, int heightB, Color[] dataB)
		{
			// Calculate a matrix which transforms from A's local space into
			// world space and then into B's local space
			Matrix transformAToB = transformA * Matrix.Invert(transformB);

			// When a point moves in A's local space, it moves in B's local space with a
			// fixed direction and distance proportional to the movement in A.
			// This algorithm steps through A one pixel at a time along A's X and Y axes
			// Calculate the analogous steps in B:
			Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
			Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

			// Calculate the top left corner of A in B's local space
			// This variable will be reused to keep track of the start of each row
			Vector2 yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

			// For each row of pixels in A
			for (int yA = 0; yA < heightA; yA++)
			{
				// Start at the beginning of the row
				Vector2 posInB = yPosInB;

				// For each pixel in this row
				for (int xA = 0; xA < widthA; xA++)
				{
					// Round to the nearest pixel
					int xB = (int)Math.Round(posInB.X);
					int yB = (int)Math.Round(posInB.Y);

					// If the pixel lies within the bounds of B
					if (0 <= xB && xB < widthB &&
					    0 <= yB && yB < heightB)
					{
						// Get the colors of the overlapping pixels
						Color colorA = dataA[xA + yA * widthA];
						Color colorB = dataB[xB + yB * widthB];

						// If both pixels are not completely transparent,
						if (colorA.A != 0 && colorB.A != 0)
						{
							// then an intersection has been found
							Console.WriteLine("---------COLLISION----------");
							BackColor = Color.Red;
							return true;
						}
					}

					// Move to the next pixel in the row
					posInB += stepX;
				}

				// Move to the next row
				yPosInB += stepY;
			}

			// No intersection found
			Console.WriteLine("--------NOT COLLISION-------");
			BackColor = Color.CornflowerBlue;
			return false;
		}
	}

}

