using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Serilog;

namespace Evolutio.Client
{
    public class FormControl
    {
        private const int defaultScreenWidth = 1920;
        private const int defaultScreenHeight = 1080;

        private GameWindow _window;
        
        public Evolutio Evolutio { get; set; }

        public void AllowMaximizeForm(GameWindow window)
        {
            window.AllowUserResizing = true;
            _window = window;
            window.ClientSizeChanged += OnResize;
        }
        
        public void Maximize(GameWindow gameWindow)
        {
            var form = (Form)Form.FromHandle(gameWindow.Handle);
            form.WindowState = FormWindowState.Maximized;
        }

        public static bool isFormActive (GameWindow gameWindow)
        {
            var form = (Form)Form.FromHandle(gameWindow.Handle);
            return Form.ActiveForm == form;
        }
        
        public void OnResize(Object sender, EventArgs e)
        {
            _window.ClientSizeChanged -= OnResize;
            Evolutio.graphics.PreferredBackBufferWidth = Evolutio.Window.ClientBounds.Width;
            var height = Evolutio.Window.ClientBounds.Height;
            if (height % 2 != 0)
            {
                height++;
            }
            Evolutio.graphics.PreferredBackBufferHeight = height;
            Evolutio.graphics.ApplyChanges();
            Evolutio.Camera.Bounds = Evolutio.graphics.GraphicsDevice.Viewport.Bounds;
            _window.ClientSizeChanged += OnResize;
        }
    }
}