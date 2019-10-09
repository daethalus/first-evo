using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Evolutio.Client
{
    public interface ClientBehavior
    {
        void LoadContent(ContentManager Content);
        
        void Update(GameTime gameTime);

        void DrawGame(SpriteBatch spriteBatch, GameTime gameTime);
        
        void DrawFixed(SpriteBatch spriteBatch, GameTime gameTime);
    }
}