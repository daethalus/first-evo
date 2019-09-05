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
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static readonly ItemRegistry ItemRegistry = new ItemRegistry();
        public static World World = new World();
        private Player player;
        private readonly List<ClientBehavior> behaviors = new List<ClientBehavior>();

        public Texture2D textureMap;
        private bool showMap = false;

        private GenerationParams gen;

        public const float SCALE = 4f;

        private KeyboardState oldState;

        private MouseState _mouseState;
        
        private SpriteFont font;

        public Evolutio()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            FormControl.Maximize(Window);
            
            player = new Player{World = World};
            behaviors.Add(new MapRenderer{World = World, Player = player});
            behaviors.Add(player);
            
            gen = new GenerationParams(World, this);
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
            
            font = Content.Load<SpriteFont>("Font");
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
            KeyboardState newState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && showMap)
            {
                showMap = false;
            } else if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                //  Exit();
            }

            if (oldState.IsKeyUp(Keys.G) && newState.IsKeyDown(Keys.G))
            {
                World.GenerateMap();
                textureMap = World.GenerateImageFromMap(graphics.GraphicsDevice);
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                showMap = true;
                textureMap = World.GenerateImageFromMap(graphics.GraphicsDevice);
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                gen.Show();
            }


            foreach (var behavior in behaviors)
            {
                behavior.Update(gameTime);
            }

            oldState = newState;
            _mouseState = Mouse.GetState();
            
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            ItemRegistry.Animate(gameTime);
            
            var framerate = Math.Floor(1 / gameTime.ElapsedGameTime.TotalSeconds);
            
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach (var behavior in behaviors)
            {
                behavior.Draw(spriteBatch, gameTime);
            }

            spriteBatch.DrawString(font, string.Format("Player position : {0}", player.PlayerPosition), new Vector2(10,10), Color.White);
            spriteBatch.DrawString(font, string.Format("Real player position : {0}", player.GetPlayerPositionInt()), new Vector2(10,40), Color.White);
            spriteBatch.DrawString(font, string.Format("Mouse position : {0}", _mouseState.Position), new Vector2(10,60), Color.White);
            spriteBatch.DrawString(font, string.Format("FPS : {0}", framerate), new Vector2(10,80), Color.White);

            if (showMap && textureMap != null)
            {
                spriteBatch.Draw(textureMap, 
                    new Vector2(500,100),
                    new Rectangle(0,0,160,160),
                    Color.White,
                    0f, 
                    new Vector2(0, 0),
                    5,
                    SpriteEffects.None,
                    0f);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
