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
        public Texture2D tree;
        public Texture2D tree2;
        private bool showMap = false;
        private MapRenderer _renderer;

        public const float SCALE = 3f;

        private KeyboardState oldState;

        private MouseState _mouseState;
        
        private SpriteFont font;

        private bool showStats;

        public Evolutio()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            FormControl.Maximize(Window);
            
            player = new Player{World = World};
            _renderer = new MapRenderer {World = World, Player = player};
            behaviors.Add(_renderer);
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
            
            font = Content.Load<SpriteFont>("Font");
            tree = Content.Load<Texture2D>("Sprite-0003");
            
            foreach (var behavior in behaviors)
            {
                behavior.LoadContent(Content);
            }
            ItemRegistry.LoadContent(Content);
        }
        
        protected override void UnloadContent()
        {
            Log.Debug("UnloadContent");
        }

        protected override void Update(GameTime gameTime)
        {
            var newState = Keyboard.GetState();

            if (oldState.IsKeyUp(Keys.Escape) && newState.IsKeyDown(Keys.Escape) && showMap)
            {
                if (showMap)
                {
                    showMap = false;   
                }
                else
                {
                    Exit();
                }
            }
            
            if (oldState.IsKeyUp(Keys.F3) && newState.IsKeyDown(Keys.F3))
            {
                showStats = !showStats;
            }

            if (oldState.IsKeyUp(Keys.G) && newState.IsKeyDown(Keys.G))
            {
                World.GenerateMap();
                textureMap = World.GenerateImageFromMap(graphics.GraphicsDevice);
            }
            
            if (oldState.IsKeyUp(Keys.F11) && newState.IsKeyDown(Keys.F11))
            {
                graphics.ToggleFullScreen();
                
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                showMap = true;
            }

            foreach (var behavior in behaviors)
            {
                behavior.Update(gameTime);
            }

            oldState = newState;
            _mouseState = Mouse.GetState();

            if (showMap && gameTime.TotalGameTime.Seconds % 5 == 0)
            {
                textureMap = World.GenerateImageFromMap(graphics.GraphicsDevice);
            }
            
            base.Update(gameTime);
        }
        
        bool gamb = false;
        
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

            if (showStats)
            {
                spriteBatch.DrawString(font, string.Format("Player position : {0}", player.PlayerPosition), new Vector2(10,10), Color.White);
                spriteBatch.DrawString(font, string.Format("Mouse position : {0}", _mouseState.Position), new Vector2(10,50), Color.White);
                spriteBatch.DrawString(font, string.Format("FPS : {0}", framerate), new Vector2(10,90), Color.White);    
                spriteBatch.DrawString(font, string.Format("Selected tile : {0}", player.SelectedTile), new Vector2(10,120), Color.White);
            }

            

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
