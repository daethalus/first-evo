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
        class Direction
        {
            public Rectangle Rectangle;
            private Direction(Rectangle rectangle)
            {
                Rectangle = rectangle;
            }

            public static Direction SOUTH = new Direction(new Rectangle(0, 0, 16, 32));
            public static Direction EAST = new Direction(new Rectangle(0, 32, 16, 32));
            public static Direction NORTH = new Direction(new Rectangle(0, 64, 16, 32));
            public static Direction WEST = new Direction(new Rectangle(0, 96, 16, 32));
        }
        
        public Vector3 PlayerPosition = new Vector3(0,0, 0);
        private Texture2D characterSprite;
        private Direction _direction = Direction.SOUTH;

        private float speed = 0.1f;

        public void LoadContent(ContentManager Content)
        {
            characterSprite = Content.Load<Texture2D>("Character");
            Log.Debug("character sprite loaded successfully");
        }

        public void Update(GameTime gameTime)
        {

            speed = 0.1f;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                speed = 1f;
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                PlayerPosition += new Vector3(0, speed, 0);
                _direction = Direction.SOUTH;
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                PlayerPosition -= new Vector3(0, speed, 0);
                _direction = Direction.NORTH;
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.A))    
            {
                PlayerPosition -= new Vector3(speed, 0, 0);
                _direction = Direction.WEST;
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                PlayerPosition += new Vector3(speed, 0, 0);
                _direction = Direction.EAST;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(characterSprite, new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 - 16, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 - 32),
                _direction.Rectangle,
                Color.White,
                0f, new Vector2(0, 0),
                Evolutio.SCALE,
                SpriteEffects.None,
                0f);
        }
    }
}