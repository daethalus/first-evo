using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Evolutio.Client
{
    public class MapRenderer : ClientBehavior
    {
        public World World { get; set; }
        public Player Player { get; set; }
        public void LoadContent(ContentManager Content)
        {
          //  throw new System.NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var PlayerPosition = Player.GetPlayerPositionIntFloor();
            var itemToRender = new List<KeyValuePair<Item,KeyValuePair<Vector2,Tile>>>();

            List<KeyValuePair<Item,KeyValuePair<Vector2,Tile>>> itemaddPlayer = null;
            
            for (var y = PlayerPosition.Y - 25; y < PlayerPosition.Y + 25; y++)
            {
                for (var x = PlayerPosition.X - 40; x < PlayerPosition.X + 40; x++)
                {
                    var tile = World.GetTile(new Vector3(x, y, 0));
                    if (tile == null) continue;

                    var color = Color.White;

                    if (Player.SelectedTile != null && tile.Position.Equals(Player.SelectedTile))
                    {
                        color = Color.Violet;
                    }

//                    var ax = x - Player.PlayerPosition.X + 15;
//                    var ay = y - Player.PlayerPosition.Y + 7;
//                    var position = new Vector2(ax * (16 * Evolutio.SCALE), ay * (16 * Evolutio.SCALE));

                    var ax = x - Player.PlayerPosition.X + 20;
                    var ay = y - Player.PlayerPosition.Y + 11;

                    var position = new Vector2(ax * (16 * Evolutio.SCALE), ay * (16 * Evolutio.SCALE));


                    var drawplayer = Player.GetPlayerPositionIntFloor().Equals(tile.Position);

                    if (drawplayer)
                    {
                        color = Color.Red;
                    }
                    
                    
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
                        var itemadd = new KeyValuePair<Item, KeyValuePair<Vector2, Tile>>(item,new KeyValuePair<Vector2, Tile>(position,tile));
                        
                        itemToRender.Add(itemadd);

                    }
                    
                    if (drawplayer)
                    {
                        Player.DrawPlayer(spriteBatch,gameTime);
                    }
                }
            }

            foreach (var item in itemToRender)
            {
                spriteBatch.Draw(item.Key.Texture2D, 
                    item.Value.Key,
                    item.Key.GetSourceRectangle(item.Value.Value.Position),
                    Color.White,
                    0f, 
                    item.Key.origin,
                    Evolutio.SCALE,
                    SpriteEffects.None,
                    0f);           
            }
        }
    }
}