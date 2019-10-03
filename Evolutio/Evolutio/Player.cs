using System;
using Evolutio.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Serilog;
using SharpNoise.Modules;

namespace Evolutio
{
    public class Player : ClientBehavior
    {
        private Texture2D quadrado;
        public World World { get; set; }

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

        public Vector3 PlayerPosition = new Vector3(0, 0, 0);
        private Texture2D characterSprite;
        private Direction _direction = Direction.SOUTH;
        private bool isWalking = false;
        private int currentSprite = 0;
        private MouseState oldState;

        public Vector3 SelectedTile { get; set; }

        private float speed;

        public void LoadContent(ContentManager Content)
        {
            characterSprite = Content.Load<Texture2D>("Character");
            quadrado = Content.Load<Texture2D>("quadrado");
            Log.Debug("character sprite loaded successfully");
        }

        public void Update(GameTime gameTime)
        {
            MouseState newState = Mouse.GetState();
 
            if(newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                SelectBlock(newState.Position);
            }
            
            oldState = newState;

            speed = 0.07f;
            isWalking = false;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                speed = 0.3f;
            }

            bool moved = false;

            var newPlayerPosition = PlayerPosition;
            var newPlayerPositionWithMargin = PlayerPosition;

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                newPlayerPosition += new Vector3(0, speed, 0);
                newPlayerPositionWithMargin += new Vector3(0, speed + 0.2f, 0); 
                _direction = Direction.SOUTH;
                moved = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                newPlayerPosition -= new Vector3(0, speed, 0);
                newPlayerPositionWithMargin -= new Vector3(0, speed + + 0.2f, 0);
                _direction = Direction.NORTH;
                moved = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                newPlayerPosition -= new Vector3(speed, 0, 0);
                newPlayerPositionWithMargin -= new Vector3(speed + + 0.2f, 0, 0);
                _direction = Direction.WEST;
                moved = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                newPlayerPosition += new Vector3(speed, 0, 0);
                newPlayerPositionWithMargin += new Vector3(speed + + 0.2f, 0, 0);
                _direction = Direction.EAST;
                moved = true;
            }
            
            if (moved)
            {
                var tile = World.GetTile(new Vector3((int) Math.Floor(newPlayerPositionWithMargin.X), (int) Math.Floor(newPlayerPositionWithMargin.Y), (int) newPlayerPositionWithMargin.Z));
                if (tile != null)
                {
                    if (tile.CanWalk())
                    {
                        isWalking = true;
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

        public void SelectBlock(Point mousePosition)
        {
            var x = (int) Math.Floor(mousePosition.X / (16 * Evolutio.SCALE));
            var y = (int) Math.Floor(mousePosition.Y / (16 * Evolutio.SCALE));
            Log.Debug("X:{x}, Y:{Y} ",x, y);
            
         SelectedTile = new Vector3(x, y, 0) + new Vector3(PlayerPosition.X - 20, PlayerPosition.Y - 11, GetPlayerPositionIntFloor().Z);
         Log.Debug("Position: {mousePosition} tile: {selectedTile} ", mousePosition, SelectedTile);
        }


        public static Vector2 GetPlayerPositionInScreen()
        {
            return new Vector2(19 * Evolutio.SCALE * 16, 10 * Evolutio.SCALE * 16);
        }

        public void DrawPlayer(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Rectangle rect = _direction.Rectangle;
            if (isWalking)
            {
                if (gameTime.TotalGameTime.Milliseconds % 100 == 0)
                {
                    currentSprite += 16;
                    if (currentSprite > 48)
                    {
                        currentSprite = 0;
                    }   
                }
            }
            else
            {
                currentSprite = 0;
            }
            
            spriteBatch.Draw(
                characterSprite,
                GetPlayerPositionInScreen(),
                new Rectangle(currentSprite, rect.Y,rect.Width,rect.Height),
                //new Rectangle(0, 0, 16, 32),
                Color.White,
                0f, new Vector2(8, 24),
                Evolutio.SCALE,
                SpriteEffects.None,
                0f);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
           // DrawPlayer(spriteBatch,gameTime);
        }
    }
}