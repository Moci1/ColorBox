using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ColorBox
{
	public delegate void ValidHandler(object sender, ValidEventArgs e);
	public class SpriteBase
	{
		Texture2D texture;
		public Color[] ColorArray { get; private set; }
		public Vector2 Size { get; private set; }
		public event ValidHandler SetTransform;
		internal bool CheckDraw { get; set; }
		public Vector3 Origin;
		Vector3 location;
		public Vector3 Location {
			get {
				return location;
			}
			set {
				location = value;
				TransformMatrix = Matrix.Identity;
				TransformMatrix *= Matrix.CreateRotationZ(rotation);
				TransformMatrix *= Matrix.CreateTranslation(location);

				//TransformMatrix.Translate(-Origin.X, -Origin.Y);
			}
		}
		float rotation;
		public float Rotation {
			get {
				return rotation;
			}
			set {
				rotation = value;
				TransformMatrix = Matrix.Identity;
				TransformMatrix *= Matrix.CreateRotationZ(rotation);// * Matrix.CreateRotationY(rotation) * Matrix.CreateRotationZ(rotation);
				TransformMatrix *= Matrix.CreateTranslation(location);
			}
		}
		public Matrix TransformMatrix { get; set; }
		public static bool kj;

		public SpriteBase(Texture2D texture, Vector3 loc, Vector2 size, float rotate)
		{
			this.TransformMatrix = new Matrix();
			this.Location = loc;
			this.Size = size;
			this.Rotation = rotate;
			this.texture = texture;
			this.Origin = new Vector3(size.X / 2f, size.Y / 2f, 0f);
			ColorArray = Pixels.GetColors(texture);
		}

		public void Draw(SpriteBatch g) {
//			g.TranslateTransform(location.X, location.Y);
//			g.RotateTransform(rotation);
			g.Draw(texture, new Vector2(location.X, location.Y), null, Color.White, rotation, new Vector2(Origin.X, Origin.Y), new Vector2(1,1), SpriteEffects.None, 1);
//			       new Vector2(-Origin.X, -Origin.Y), Color.White);
//			g.FillEllipse(Brushes.Red, new Rectangle(g.RenderingOrigin.X - 5, g.RenderingOrigin.Y - 5, 10, 10));
//			g.ResetTransform();
			CheckDraw = false;
		}
//		public override bool Equals(object obj)
//		{
//			SpriteBase sb = (SpriteBase)obj;
//			if (sb.location != this.location)
//				return false;
//			else
//				return true;
//
//			return base.Equals(obj);
//		}
//		public override int GetHashCode()
//		{
//			return location.GetHashCode();
//		}
	}
}

