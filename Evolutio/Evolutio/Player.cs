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

        public Evolutio Evolutio { get; set; }

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
        public Vector3 oldPlayerPosition = new Vector3(0, 0, 0);
        private Texture2D characterSprite;
        private Direction _direction = Direction.SOUTH;
        private bool isWalking = false;
        private int currentSprite = 0;
        private MouseState oldMouseState;
        private KeyboardState oldKeybordState;
        private Vector2 mouseSelection;
        public long lastTickAction;
        private bool doingPathFinder = false;
        public ItemStack[] ButtomStacks { get; private set; }

        public Vector3 SelectedTile { get; set; }
        public Vector3 AcionedItem { get; set; }

        private float speed;

        public Player()
        {
            ButtomStacks = new ItemStack [10];
        }

        public void LoadContent(ContentManager Content)
        {
            characterSprite = Content.Load<Texture2D>("Character");
            quadrado = Content.Load<Texture2D>("quadrado");
            Log.Debug("character sprite loaded successfully");
        }

        public void Update(GameTime gameTime)
        {
            if (!FormControl.isFormActive(Evolutio.Window))
            {
                return;
            }
            oldPlayerPosition = PlayerPosition;
            var mouseState = Mouse.GetState();
            
            updateMouseSelection(mouseState.Position);
            
            if(mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                var oldSelected = SelectedTile;
                
                SelectBlock();
                
                if (_direction == Direction.SOUTH)
                {
                    AcionedItem = GetPlayerPositionIntFloor() + new Vector3(0, 1, 0);
                } else if (_direction == Direction.NORTH)
                {
                    AcionedItem = GetPlayerPositionIntFloor() - new Vector3(0, 1, 0);
                } else if (_direction == Direction.WEST)
                {
                    AcionedItem = GetPlayerPositionIntFloor() - new Vector3(1, 0, 0);
                } else if (_direction == Direction.EAST)
                {
                    AcionedItem = GetPlayerPositionIntFloor() + new Vector3(1, 0, 0);
                }

                if (AcionedItem == SelectedTile)
                {
                    lastTickAction = gameTime.TotalGameTime.Ticks;
                    Tile tile = World.GetTile(AcionedItem);
                    if (tile.Items.Count > 0)
                    {
                        var item = tile.Items[tile.Items.Count - 1];
                        var durability = item.ApplyDamage(100);
                        if (durability <= 0)
                        {
                            tile.Items.RemoveAt(tile.Items.Count - 1);
                            var pickable = item.Item.CreatePickable();
                            if (pickable != null)
                            {
                                tile.PickableItems.Add(pickable);
                            }
                        }
                    } 
                } else if (oldSelected.Equals(SelectedTile))
                { 
                    // WE SAY TO DEATH: 'NOT TODAY'
                     doingPathFinder = true;
                }

//                if (oldSelected.Equals(SelectedTile))
//                {
//                    World.PlaceGround(Evolutio.ItemRegistry.findItem("ground"), new Vector3(mouseSelection.X, mouseSelection.Y, 0));
//                }
            }
            
            if(mouseState.RightButton== ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released)
            {
                var itemStack = Evolutio.BottomMenu.GetSelectecItem();
                if (itemStack != null)
                {
                    World.PlaceItem(itemStack.Item, new Vector3(mouseSelection.X, mouseSelection.Y, 0));
                    itemStack.ChangeQuantity(-1);
                    if (itemStack.Quantity <= 0)
                    {
                        RemoveItem(itemStack);
                    }

                }
            }
            
            if (oldKeybordState.IsKeyUp(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.Space))
            {

                
            }
            
            oldMouseState = mouseState;

            speed = 0.1f;
            isWalking = false;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                speed = 0.3f;
            }

            bool moved = false;

            var newPlayerPosition = PlayerPosition;
            var newPlayerPositionWithMargin = PlayerPosition;

            if (doingPathFinder)
            {
                if (Math.Floor(SelectedTile.X) > Math.Floor(PlayerPosition.X))
                {
                    newPlayerPosition += new Vector3(speed, 0 , 0); 
                    _direction = Direction.EAST;
                    moved = true;
                }
                else if (Math.Floor(SelectedTile.X) < Math.Floor(PlayerPosition.X))
                {
                    newPlayerPosition -= new Vector3(speed, 0, 0);
                    _direction = Direction.WEST;
                    moved = true;
                }
                
                if (Math.Floor(SelectedTile.Y) > Math.Floor(PlayerPosition.Y))
                {
                    newPlayerPosition += new Vector3(0, speed, 0);
                    _direction = Direction.SOUTH;
                    moved = true;
                }
                else if (Math.Floor(SelectedTile.Y) < Math.Floor(PlayerPosition.Y))
                {
                    newPlayerPosition -= new Vector3(0, speed, 0);
                    _direction = Direction.NORTH;
                    moved = true;
                }

                if (Math.Floor(SelectedTile.X) == Math.Floor(PlayerPosition.X) && Math.Floor(SelectedTile.Y) == Math.Floor(PlayerPosition.Y))
                {
                    doingPathFinder = false;
                }

                newPlayerPositionWithMargin = newPlayerPosition;

            }

            if (oldKeybordState.IsKeyUp(Keys.R) && Keyboard.GetState().IsKeyDown(Keys.R))
            {
                World.PlaceGround(Evolutio.ItemRegistry.findItem("ground"), new Vector3(mouseSelection.X, mouseSelection.Y, 0));
            }

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
                newPlayerPositionWithMargin -= new Vector3(speed + 0.2f, 0, 0);
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
                        Evolutio.Camera.MoveCamera(newPlayerPosition);
                        if (tile.PickableItems.Count > 0)
                        {
                            foreach (var itemStack in tile.PickableItems)
                            {
                                GiveItem(itemStack);
                            }
                            tile.PickableItems.Clear();
                        }
                    }
                    else
                    {
                        doingPathFinder = false;
                    }
                }
            }
            oldKeybordState = Keyboard.GetState();
        }

        public void DrawGame(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }

        public void DrawFixed(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }

        public Vector3 GetPlayerPositionInt()
        {
            return new Vector3((int) PlayerPosition.X, (int) PlayerPosition.Y, (int) PlayerPosition.Z);
        }
        
        public Vector3 GetPlayerPositionIntFloor()
        {
            return new Vector3((int) Math.Floor(PlayerPosition.X),  (int) Math.Floor(PlayerPosition.Y), (int) Math.Floor(PlayerPosition.Z));
        }

        private void SelectBlock()
        {
            SelectedTile = new Vector3(mouseSelection.X, mouseSelection.Y, 0);
        }

        private void updateMouseSelection(Point mousePosition)
        {
            var pos = new Vector2(
                (int) Math.Floor((mousePosition.X - Evolutio.Camera.Bounds.Width * 0.5f) / Evolutio.Camera.Zoom)  + Evolutio.Camera.Position.X, 
                (int) Math.Floor((mousePosition.Y - Evolutio.Camera.Bounds.Height * 0.5f) / Evolutio.Camera.Zoom)  + Evolutio.Camera.Position.Y
            );

            mouseSelection = new Vector2((int) Math.Floor(pos.X / 16), (int) Math.Floor(pos.Y / 16));
        }


        public Vector2 GetPlayerPositionInScreen()
        {
            //return new Vector2(19 * Evolutio.SCALE * 16, 10 * Evolutio.SCALE * 16);
            return new Vector2(oldPlayerPosition.X * 16,oldPlayerPosition.Y * 16);
        }

        public void DrawPlayer(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Rectangle rect = _direction.Rectangle;
            if (isWalking)
            {
                if (gameTime.TotalGameTime.Ticks % 6 == 0)
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

            if (mouseSelection != null)
            {
                spriteBatch.Draw(
                    Evolutio.selectionSquare,
                    new Vector2((int) mouseSelection.X * 16, (int) mouseSelection.Y * 16),
                    new Rectangle(0, 0, 16, 16),
                    Color.White,
                    0f, new Vector2(0, 0),
                    Evolutio.SCALE,
                    SpriteEffects.None,
                    0f);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
           // DrawPlayer(spriteBatch,gameTime);
        }
        
        public bool GiveItem(ItemStack itemStack)
        {
            for (int i = 0; i < ButtomStacks.Length; i++)
            {
                if (ButtomStacks[i] == null)
                {
                    ButtomStacks[i] = itemStack;
                    return true;
                }
                
                if (ButtomStacks[i].Item == itemStack.Item)
                {
                    ButtomStacks[i].ChangeQuantity(itemStack.Quantity);
                    return true;
                } 
            }
            return false;
        }

        public void RemoveItem(ItemStack itemStack)
        {
            for (int i = 0; i < ButtomStacks.Length; i++)
            {
                if (ButtomStacks[i] == itemStack)
                {
                    ButtomStacks[i] = null;
                }
            }
        }

    }
}