using System;
using System.Collections.Generic;
using Evolutio.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Serilog;

namespace Evolutio
{
   
    public class Evolutio : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static readonly ItemRegistry ItemRegistry = new ItemRegistry();
        public static World World = new World();
        private Player player;
        private readonly List<ClientBehavior> behaviors = new List<ClientBehavior>();

        private Texture2D overworld;


        public const float SCALE = 4f;

        public Evolutio()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            FormControl.Maximize(Window);
            
            player = new Player();
            behaviors.Add(player);
        }
        protected override void Initialize()
        {
            Log.Debug("Initialize");
            IsMouseVisible = true;
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Log.Debug("LoadContent");
            overworld = Content.Load<Texture2D>("Overworld");
            foreach (var behavior in behaviors)
            {
                behavior.LoadContent(Content);
            }
        }
        
        protected override void UnloadContent()
        {
            Log.Debug("UnloadContent");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var behavior in behaviors)
            {
                behavior.Update(gameTime);
            }
            
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);


            for (var x = (int) player.PlayerPosition.X - 10; x < (int) player.PlayerPosition.X + 22; x++)
            {
                for (var y = (int) player.PlayerPosition.Y - 10; y < (int) player.PlayerPosition.Y + 10; y++)
                { 
                    //Log.Debug(x + " - " + y);
                    Tile tile;
                    if (!World.Tiles.TryGetValue(new Vector3(x, y, 0), out tile)) continue;
                    var ax = x - player.PlayerPosition.X + 10;
                    var ay = y - player.PlayerPosition.Y + 10;

                    var position = new Vector2(ax * (16 * SCALE), ay * (16 * SCALE));
                    
                    //Log.Debug("position {position} ", position);
                    
                        spriteBatch.Draw(overworld, 
                            position,
                            tile.Ground.SourceRectangle,
                            Color.White,
                            0f, new Vector2(0, 0),
                            SCALE,
                            SpriteEffects.None,
                            0f);

                }

            }
            foreach (var behavior in behaviors)
            {
                behavior.Draw(spriteBatch, gameTime);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
