#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace ColorBox
{
	/// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        ExtendedSpriteBatch spriteBatch;
		SpriteFont font;

		GameManager gm;
		bool init;
		Movement lets;
		Random rnd = new Random();
		Vector2 mPoint;
		Texture2D tank1Bmp;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";	            
			graphics.IsFullScreen = false;		
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
				
        }
        protected override void LoadContent()
        {
			spriteBatch = new ExtendedSpriteBatch(GraphicsDevice);

			tank1Bmp = Content.Load<Texture2D>("tank1");

			gm = new GameManager(
				new SpriteBase(tank1Bmp, new Vector3(rnd.Next(100, 151), rnd.Next(100, 201), 0f), new Vector2(tank1Bmp.Width, tank1Bmp.Height), 0f),
				new SpriteBase(tank1Bmp, new Vector3(rnd.Next(160, 251), rnd.Next(100, 201), 0f), new Vector2(tank1Bmp.Width, tank1Bmp.Height), 0f));
			gm.ValidTransform += HandleValidTransform;
			lets = new Movement(gm);
        }
		void HandleValidTransform(object sender, EventArgs e)
		{
//			Point p = new Point((int)(gm.Current.Location.X - gm.Current.Origin.X), (int)(gm.Current.Location.Y - gm.Current.Origin.Y));
//			Size s = new Size((int)gm.Current.Size.Width*2, (int)gm.Current.Size.Height*2);
//			Invalidate(new Rectangle(p, s));
//			Invalidate();
		}
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
			{
				Exit();
			}

			SpriteBase sb = gm.Sprites[0];
			Vector3 p = Vector3.Zero;
			KeyboardState keyState = Keyboard.GetState(PlayerIndex.One);

			if (keyState.IsKeyDown(Keys.Up)) {
				p = lets.GoRotatedDirection(sb.Location, sb.Size, sb.Rotation, 1f);
				lets.ValidTrasform(sb, Transforms.Translate, p);
			} else if (keyState.IsKeyDown(Keys.Down)) {
				p = lets.GoRotatedDirection(sb.Location, sb.Size, sb.Rotation, -1f);
				lets.ValidTrasform(sb, Transforms.Translate, p);
			}
			if (keyState.IsKeyDown(Keys.Left)) {
				lets.ValidTrasform(sb, Transforms.Rotate, sb.Rotation - .057f);
			} else if (keyState.IsKeyDown(Keys.Right)) {
				lets.ValidTrasform(sb, Transforms.Rotate, sb.Rotation + 0.057f);
			}

            // TODO: Add your update logic here			
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
           	graphics.GraphicsDevice.Clear(Pixels.BackColor);

			spriteBatch.Begin();
			if (init) {
//				for (int i = 0; i < gm.Sprites.Count; i++) {
//					gm.Sprites[i].Draw(spriteBatch);
//					Vector3 pp = gm.Sprites[i].Location - gm.Sprites[i].Origin;
//					Rectangle r = new Rectangle((int)pp.X, (int)pp.Y, (int)gm.Sprites[i].Size.X, (int)gm.Sprites[i].Size.Y);
//					spriteBatch.DrawRectangle(MathExtras.CalcBoundingRectangle(r, gm.Sprites[i].TransformMatrix), Color.Red);
//				}
			}
			else {
				//g.DrawLine(p, gm.Current.Location, mPoint);
				gm.Current.Draw(spriteBatch);
				for (int i = 0; i < gm.Sprites.Count; i++) {
					gm.Sprites[i].Draw(spriteBatch);
					Vector3 pp = gm.Sprites[i].Location - gm.Sprites[i].Origin;
					Rectangle r = new Rectangle((int)pp.X, (int)pp.Y, (int)gm.Sprites[i].Size.X, (int)gm.Sprites[i].Size.Y);
					Rectangle rr = MathExtras.CalcBoundingRectangle(r, gm.Sprites[i].TransformMatrix);
					spriteBatch.DrawRectangle(rr, Color.Blue);
//					g.DrawString(mPoint.ToString(), new Font(FontFamily.Families[80], 10f), Brushes.Green, new PointF(0, 20)); 
				}
//				g.DrawString(SpriteBase.kj.ToString(), new Font(FontFamily.Families[80], 10f), Brushes.Green, PointF.Empty); 
//				g.DrawString(mPoint.ToString(), new Font(FontFamily.Families[80], 10f), Brushes.Green, new PointF(0, 20)); 
				gm.Current.CheckDraw = true;
			}
			init = false;
			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

