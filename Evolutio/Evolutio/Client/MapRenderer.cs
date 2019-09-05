using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Evolutio.Client
{
    public class MapRenderer : ClientBehavior
    {
        public World World { get; set; }
        public Player Player { get; set; }
        
        private Texture2D _overworld;

        public void LoadContent(ContentManager Content)
        {
            _overworld = Content.Load<Texture2D>("Overworld");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var PlayerPosition = Player.GetPlayerPositionIntFloor();
            
            for (var x = PlayerPosition.X - 16; x < PlayerPosition.X + 17; x++)
            {
                for (var y = PlayerPosition.Y - 10; y < PlayerPosition.Y + 10; y++)
                {
                    var tile = World.GetTile(new Vector3(x, y, 0));
                    if (tile == null) continue;

                    var color = Color.White;

                    if (Player.SelectedTile != null && tile.Position.Equals(Player.SelectedTile))
                    {
                        color = Color.Violet;
                    }
//                    if (Player.GetPlayerPositionIntFloor().Equals(tile.Position))
//                    {
//                        color = Color.Red;
//                    }

//                    var ax = x - Player.PlayerPosition.X + 15;
//                    var ay = y - Player.PlayerPosition.Y + 7;
//                    var position = new Vector2(ax * (16 * Evolutio.SCALE), ay * (16 * Evolutio.SCALE));

                    var ax = x - Player.PlayerPosition.X + 14;
                    var ay = y - Player.PlayerPosition.Y + 7;

                    var position = new Vector2(ax * (16 * Evolutio.SCALE), ay * (16 * Evolutio.SCALE));
                    
                    
                    spriteBatch.Draw(_overworld, 
                        position,
                        tile.Ground.GetSourceRectangle(tile.Position),
                        color,
                        0f, new Vector2(0, 0),
                        Evolutio.SCALE,
                        SpriteEffects.None,
                        0f);

                    foreach (var item in tile.Items)
                    {
                        spriteBatch.Draw(_overworld, 
                            position,
                            item.GetSourceRectangle(tile.Position),
                            color,
                            0f, new Vector2(0, 0),
                            Evolutio.SCALE,
                            SpriteEffects.None,
                            0f);                        
                    }
                }
            }
        }
    }
}