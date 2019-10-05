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

        private bool rotationState = false;

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
                        new Vector2(x * 16, y * 16),
                        tile.Ground.Item.GetSourceRectangle(tile.Position),
                        color,
                        0f, tile.Ground.Item.origin,
                        Evolutio.SCALE,
                        SpriteEffects.None,
                        0f);
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
                        new Vector2(tile.Position.X * 16, tile.Position.Y * 16),
                        state.Item.GetSourceRectangle(tile.Position),
                        color,
                        rotation,
                        state.Item.origin,
                        Evolutio.SCALE,
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