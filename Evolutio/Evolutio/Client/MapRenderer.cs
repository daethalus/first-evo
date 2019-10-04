using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Evolutio.Client
{
    public class MapRenderer : ClientBehavior
    {
        private class RenderItem
        {
            public Item item;
            public Vector2 screenPosition;
            public Tile tile;
            public Color color;
            public bool transparent;
        }
        
        private Texture2D quadrado;
        
        private readonly Vector2 renderSize = new Vector2(25,15);
        
        public World World { get; set; }
        public Player Player { get; set; }
        public void LoadContent(ContentManager Content)
        {
            quadrado = Content.Load<Texture2D>("quadrado");
          //  throw new System.NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var transparency = 0.7f;
            List<RenderItem> itensToRender = new List<RenderItem>();
            
            var PlayerPosition = Player.GetPlayerPositionIntFloor();
            
            for (var y = PlayerPosition.Y - renderSize.Y; y <= PlayerPosition.Y + renderSize.Y; y++)
            {
                for (var x = PlayerPosition.X - renderSize.X; x <= PlayerPosition.X + renderSize.X; x++)
                {
                    var tile = World.GetTile(new Vector3(x, y, 0));
                    
                    var p2 = new Vector2(x - Player.PlayerPosition.X, y - Player.PlayerPosition.Y);
                    var position = new Vector2(p2.X * 16 * Evolutio.SCALE, p2.Y * 16 * Evolutio.SCALE);
                    position += Player.GetPlayerPositionInScreen();

                    var color = Color.White;

                    spriteBatch.Draw(tile.Ground.Texture2D, 
                        position,
                        tile.Ground.GetSourceRectangle(tile.Position),
                        color,
                        0f, tile.Ground.origin,
                        Evolutio.SCALE,
                        SpriteEffects.None,
                        0f);


                    foreach (var item in tile.Items)
                    {
                        var transparent = false;
                        if (item.Name == "tree")
                        {
                            var ix = tile.Position.X - PlayerPosition.X;
                            var iy = tile.Position.Y - PlayerPosition.Y;
                            
                            if (ix > -3 && ix < 3 && iy > 0 && iy < 6)
                            {
                                transparency -= 0.1f;
                                transparent = true;
                            }
                        }
                        itensToRender.Add(new RenderItem{item = item,screenPosition = position,tile = tile, color = color, transparent = transparent});
                    }
                }
            }
            
            Player.DrawPlayer(spriteBatch,gameTime);

            foreach (var renderItem in itensToRender)
            {
                var color = renderItem.color;
                if (renderItem.transparent)
                {
                    color *= transparency;
                }
                spriteBatch.Draw(renderItem.item.Texture2D, 
                    renderItem.screenPosition,
                    renderItem.item.GetSourceRectangle(renderItem.tile.Position),
                    color,
                    0f, 
                    renderItem.item.origin,
                    Evolutio.SCALE,
                    SpriteEffects.None,
                    0f);
            }
        }
    }
}