using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ColorBox
{
	public class MathExtras
	{
		public static Rectangle CalcBoundingRectangle(Rectangle rectangle, Matrix transform)
		{
			Vector3[] rectOrigin = new Vector3[4];
			rectOrigin[0] = new Vector3(rectangle.Width / -2, rectangle.Height / -2, 0);
			rectOrigin[1] = new Vector3(rectangle.Width / 2, rectangle.Height / -2, 0);
			rectOrigin[2] = new Vector3(rectangle.Width / -2, rectangle.Height / 2, 0);
			rectOrigin[3] = new Vector3(rectangle.Width / 2, rectangle.Height / 2, 0);

			for (int i = 0; i < rectOrigin.Length; i++) {
				rectOrigin[i] = Vector3.Transform(rectOrigin[i], transform);
			}

			Vector3 min = Vector3.Min(rectOrigin[0], rectOrigin[1]);
			Vector3 min1 =  Vector3.Min(rectOrigin[2], rectOrigin[3]);
			min = Vector3.Min(min, min1);
			Vector3 max = Vector3.Max(rectOrigin[0], rectOrigin[1]);
			Vector3 max1 = Vector3.Max(rectOrigin[2], rectOrigin[3]);
			max = Vector3.Max(max, max1);
			
			return new Rectangle((int)min.X, (int)min.Y,
			                              (int)(max.X - min.X), (int)(max.Y - min.Y));
		}
	}
}

