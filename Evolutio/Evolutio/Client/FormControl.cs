using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evolutio.Client
{
    public class FormControl
    {
        public Evolutio Evolutio { get; set; }

        public void AllowMaximizeForm(GameWindow window)
        {
            window.AllowUserResizing = true;
            window.ClientSizeChanged += OnResize;
        }
        
        public void Maximize(GameWindow gameWindow)
        {
            var form = (Form)Form.FromHandle(gameWindow.Handle);
            form.WindowState = FormWindowState.Maximized;
        }

        public bool isFormActive (GameWindow gameWindow)
        {
            var form = (Form)Form.FromHandle(gameWindow.Handle);
            return Form.ActiveForm == form;
        }
        
        public void OnResize(Object sender, EventArgs e)
        {
            Evolutio.graphics.PreferredBackBufferWidth = Evolutio.Window.ClientBounds.Width;
            Evolutio.graphics.PreferredBackBufferHeight = Evolutio.Window.ClientBounds.Height;
            Evolutio.graphics.ApplyChanges();
            Evolutio.Camera.Bounds = Evolutio.graphics.GraphicsDevice.Viewport.Bounds;

        }
    }
}