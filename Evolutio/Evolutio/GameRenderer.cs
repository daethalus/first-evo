using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;

namespace Evolutio
{
    public class GameRenderer
    {
        public World World { get; set; }
        public Player Player { get; set; }

        public const int SpriteSize = 32;

        private bool rotationState = false;
        private bool _pickableMoviment = true;
        private int _pickablePosition = 4;

        private readonly Vector2 renderSize = new Vector2(25, 15);

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var playerIsDrawed = false;
            var transparency = 0.7f;
            List<Tile> tiles = new List<Tile>();

            var PlayerPosition = Player.GetPlayerPositionIntFloor();
            
            for (var y = PlayerPosition.Y - renderSize.Y; y <= PlayerPosition.Y + renderSize.Y; y++)
            { 
                for (var x = PlayerPosition.X - renderSize.X; x <= PlayerPosition.X + renderSize.X; x++)
                {
                    var tile = World.GetTile(new Vector3(x, y, 0));
                    tiles.Add(tile);

                    var color = Color.White;

                    if (Player.SelectedTile == new Vector3(x, y, 0))
                    {
                        color = Color.LightGray;
                    }

                    spriteBatch.Draw(tile.Ground.Item.Texture2D,
                        new Vector2(x * SpriteSize, y * SpriteSize),
                        tile.Ground.Item.GetSourceRectangle(tile.Position),
                        color,
                        0f, tile.Ground.Item.origin,
                        Evolutio.SCALE,
                        SpriteEffects.None,
                        0f);
                }
            }
//
//            Player.DrawPlayer(spriteBatch, gameTime);
//            
//            return;
            
            if (gameTime.TotalGameTime.Ticks % 10 == 0)
            {
                if (_pickableMoviment) 
                {
                    _pickablePosition++;
                }
                            
                if (!_pickableMoviment)
                {
                    _pickablePosition--;
                }

                if (_pickablePosition >= 8)
                {
                    _pickableMoviment = false;
                }

                if (_pickablePosition <= 4)
                {
                    _pickableMoviment = true;
                }
                            
            }            

            foreach (var tile in tiles)
            {
                foreach (var state in tile.Items)
                {
                    var transparent = false;
                    if (state.Item.Name == "tree")
                    {
                        var ix = tile.Position.X - PlayerPosition.X;
                        var iy = tile.Position.Y - PlayerPosition.Y;

                        if (ix > -3 && ix < 3 && iy > 0 && iy < 6)
                        {
                            transparency -= 0.1f;
                            transparent = true;
                            if (!playerIsDrawed)
                            {
                                   Player.DrawPlayer(spriteBatch, gameTime);
                                playerIsDrawed = true;
                            }
                        }
                    }

                    if (state.Item.Name == "wall")
                    {
                        var ix = tile.Position.X - PlayerPosition.X;
                        var iy = tile.Position.Y - PlayerPosition.Y;

                        if (iy == 1 && ix > -2 && ix < 2)
                        {
                            transparency = 0.5f;
                            transparent = true;
                            if (!playerIsDrawed)
                            {
                                Player.DrawPlayer(spriteBatch, gameTime);
                                playerIsDrawed = true;
                            }   
                        }
                    }
                    
                    var rotation = 0f;
                    
                    if (Player.AcionedItem.Equals(tile.Position))
                    {
                        var rotate = gameTime.TotalGameTime.Ticks - Player.lastTickAction < 120;
                        if (rotate)
                        {
                            if (rotationState)
                            {
                                rotationState = false;
                                rotation = 0.05f;
                            }
                            else
                            {
                                rotationState = true;
                                rotation = -0.05f;
                            }
                        }
                        else
                        {
                            rotation = 0f;
                        }
                    }
                    
                    var color = Color.White;
                    if (transparent)
                    {
                        color *= transparency;
                    }

                    spriteBatch.Draw(state.Item.Texture2D,
                        new Vector2(tile.Position.X * SpriteSize, tile.Position.Y * SpriteSize),
                        state.Item.GetSourceRectangle(tile.Position),
                        color,
                        rotation,
                        state.Item.origin,
                        Evolutio.SCALE,
                        SpriteEffects.None,
                        0f);
                }

                foreach (var pickableItem in tile.PickableItems)
                {
                    spriteBatch.Draw(pickableItem.Item.Texture2D,
                        new Vector2((tile.Position.X * SpriteSize) + 8, (tile.Position.Y * SpriteSize) + _pickablePosition),
                        pickableItem.Item.GetSourceRectangle(tile.Position),
                        Color.White,
                        0f,
                        pickableItem.Item.origin,
                        0.5f,
                        SpriteEffects.None,
                        0f);
                }
            }
             

            if (!playerIsDrawed)
            {
                Player.DrawPlayer(spriteBatch, gameTime);
            }
        }
    }
}