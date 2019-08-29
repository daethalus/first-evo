using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace Evolutio.Client
{
    public class FormControl
    {
        public static void Maximize(GameWindow gameWindow)
        {
            var form = (Form)Form.FromHandle(gameWindow.Handle);
            form.WindowState = FormWindowState.Maximized;
        }

        public static bool isFormActive (GameWindow gameWindow)
        {
            var form = (Form)Form.FromHandle(gameWindow.Handle);
            return Form.ActiveForm == form;
        }
    }
}