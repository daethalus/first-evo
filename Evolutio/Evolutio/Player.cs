using System;
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
        public World World { get; set; }

        class Direction
        {
            public Rectangle Rectangle;

            private Direction(Rectangle rectangle)
            {
                Rectangle = rectangle;
            }

//            public static Direction SOUTH = new Direction(new Rectangle(0, 0, 16, 32));
//            public static Direction EAST = new Direction(new Rectangle(0, 32, 16, 32));
//            public static Direction NORTH = new Direction(new Rectangle(0, 64, 16, 32));
//            public static Direction WEST = new Direction(new Rectangle(0, 96, 16, 32));


            public static Direction SOUTH = new Direction(new Rectangle(0, 0, 16, 32));
            public static Direction EAST = new Direction(new Rectangle(0, 32, 16, 32));
            public static Direction NORTH = new Direction(new Rectangle(0, 64, 16, 32));
            public static Direction WEST = new Direction(new Rectangle(0, 96, 16, 32));
        }

        public Vector3 PlayerPosition = new Vector3(0, 0, 0);
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

            bool moved = false;

            var newPlayerPosition = PlayerPosition;

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                newPlayerPosition += new Vector3(0, speed, 0);
                _direction = Direction.SOUTH;
                moved = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                newPlayerPosition -= new Vector3(0, speed, 0);
                _direction = Direction.NORTH;
                moved = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                newPlayerPosition -= new Vector3(speed, 0, 0);
                _direction = Direction.WEST;
                moved = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                newPlayerPosition += new Vector3(speed, 0, 0);
                _direction = Direction.EAST;
                moved = true;
            }
            
           // PlayerPosition = newPlayerPosition;
            
            if (moved)
            {
                var tile = World.GetTile(new Vector3((int) Math.Floor(newPlayerPosition.X), (int) Math.Floor(newPlayerPosition.Y), (int) newPlayerPosition.Z));
                if (tile != null)
                {
                    bool canWalk = true;
                    foreach (var item in tile.Items)
                    {
                        if (!item.CanWalk)
                        {
                            canWalk = false;
                            break;
                        }
                    }
                    if (!tile.Ground.CanWalk)
                    {
                        canWalk = false;
                    }

                    if (canWalk)
                    {
                        PlayerPosition = newPlayerPosition;
                    }
                }
            }
        }

        public Vector3 GetPlayerPositionInt()
        {
            return new Vector3((int) PlayerPosition.X, (int) PlayerPosition.Y, (int) PlayerPosition.Z);
        }
        
        public Vector3 GetPlayerPositionIntFloor()
        {
            return new Vector3((int) Math.Floor(PlayerPosition.X),  (int) Math.Floor(PlayerPosition.Y), (int) Math.Floor(PlayerPosition.Z));
        }



        public static Vector2 GetPlayerPositionInScreen()
        {
            return new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 - 94,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 - 186);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(characterSprite,
                GetPlayerPositionInScreen(),
                _direction.Rectangle,
                Color.White,
                0f, new Vector2(0, 0),
                Evolutio.SCALE,
                SpriteEffects.None,
                0f);
        }
    }
}