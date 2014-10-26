using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ColorBox
{
	public enum Transforms {
		Rotate,
		Scale,
		Translate,
		Skew,
		Custom
	}
	public class Movement
	{
		GameManager gm;
		public Movement(GameManager gm)
		{
			this.gm = gm;
		}

		public Vector3 GoRotatedDirection(Vector3 location, Vector2 size, float rotated, float speed) {
			Vector3 v = new Vector3(0f, -1f, 0f);
			v = Vector3.Transform(v, Matrix.CreateRotationZ(rotated)); Vector3 jkhk = (location + v); jkhk *= 1;
			if (Math.Sign(speed) == 1)
				v = (location + v);
			else if (Math.Sign(speed) == -1)
				v = (location - v);// * ((float)Math.Abs(speed));
			else
				return Vector3.Zero;
			return v;
		}
		public void ValidTrasform(SpriteBase sb, Transforms mode, params object[] args) {
			if (mode == Transforms.Rotate) {
				float newRot = (float)args[0];
				if (sb.Rotation != newRot) {
					float oldRot = sb.Rotation;
					sb.Rotation = newRot;
					if (!gm.HandleTransforms(sb))
						sb.Rotation = oldRot;
				}
			} else if (mode == Transforms.Translate) {
				Vector3 location = (Vector3)args[0];
				if (sb.Location.X != location.X || sb.Location.Y != location.Y) {
					Vector3 oldLoc = sb.Location;
					sb.Location = location;
					if (!gm.HandleTransforms(sb))
						sb.Location = oldLoc;
				}
			}
		}
	}
}

