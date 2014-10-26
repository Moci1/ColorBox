using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ColorBox
{
	public class GameManager
	{
		public List<SpriteBase> Sprites { get; private set; }
		public SpriteBase Current { get; private set; }
		public event EventHandler ValidTransform;

		public GameManager(params SpriteBase[] sprites)
		{
			Sprites = new List<SpriteBase>();
			Sprites.AddRange(sprites);
			Current = Sprites.Last();
		}

		public bool HandleTransforms(SpriteBase sb)
		{
			for (int i = 0; i < Sprites.Count; i++) {
				bool kjl = !sb.Equals(Sprites[i]);
				Vector3 v1 = sb.Location - sb.Origin, v2 = Sprites[i].Location - sb.Origin;
				if (!sb.Equals(Sprites[i]) &&
					Pixels.IntersectPixels(sb.TransformMatrix, (int)sb.Size.X, (int)sb.Size.Y, sb.ColorArray,
						Sprites[i].TransformMatrix, (int)Sprites[i].Size.X, (int)Sprites[i].Size.Y, Sprites[i].ColorArray)) {
//				    Colors.IntersectPixels(new Rectangle((int)v1.X, (int)v1.Y, (int)sb.Size.X, (int)sb.Size.Y), sb.ColorArray,
//				                       new Rectangle((int)v2.X, (int)v2.Y, (int)Sprites[i].Size.X, (int)Sprites[i].Size.Y), Sprites[i].ColorArray)) {
					return false;
				}
			}
			Current = sb;
			if (ValidTransform != null)
				ValidTransform(this, EventArgs.Empty);
			return true;
		}
	}
}

