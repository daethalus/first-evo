using Evolutio.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Serilog;

namespace Evolutio
{
    public class Player : ClientBehavior
    {
        public Vector3 PlayerPosition = new Vector3(30,30, 0);
        private Texture2D characterSprite;

        private float speed = 0.1f;

        public void LoadContent(ContentManager Content)
        {
            characterSprite = Content.Load<Texture2D>("Character");
            Log.Debug("character sprite loaded successfully");
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                PlayerPosition += new Vector3(0, speed, 0);
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                PlayerPosition -= new Vector3(0, speed, 0);
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                PlayerPosition -= new Vector3(speed, 0, 0);
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                PlayerPosition += new Vector3(speed, 0, 0);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(characterSprite, new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 - 16, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 - 32),
                new Rectangle(0, 0, 16, 32),
                Color.White,
                0f, new Vector2(0, 0),
                Evolutio.SCALE,
                SpriteEffects.None,
                0f);
        }
    }
}