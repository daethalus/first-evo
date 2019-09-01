using System;
using System.Windows.Forms;
using Serilog;

namespace Evolutio
{
    public partial class GenerationParams : Form
    {
        private World _world;
        private Evolutio _evolutio;
        public GenerationParams(World world, Evolutio evolutio)
        {
            _world = world;
            _evolutio = evolutio;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _world.ax = Int32.Parse(tb1.Text);
                _world.ay = Int32.Parse(tb2.Text);
                _world.OctaveCount = Int32.Parse(tbOctave.Text);
                _world.Frequency = Convert.ToDouble(tbFreq.Text);
                _world.Persistence = Convert.ToDouble(tbPersistence.Text);
                _world.Scale = Convert.ToDouble(tbScale.Text);
                _world.LowerBound = Convert.ToDouble(tbLowerBound.Text);
                _world.UpperBound = Convert.ToDouble(tbUpperBound.Text);
            }
            catch (Exception ex)
            {
                Log.Error("Error {error}", ex);
            }
        }

        private void GenerationParams_Load(object sender, EventArgs e)
        {
            tb1.Text = _world.ax.ToString();
            tb2.Text = _world.ay.ToString();
            tbOctave.Text = _world.OctaveCount.ToString();
            tbFreq.Text = _world.Frequency.ToString();
            tbPersistence.Text = _world.Persistence.ToString();
            tbScale.Text = _world.Scale.ToString();
            tbLowerBound.Text = _world.LowerBound.ToString();
            tbUpperBound.Text = _world.UpperBound.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
            _world.GenerateMap();
            _evolutio.textureMap =  _world.GenerateImageFromMap(_evolutio.GraphicsDevice);
        }
    }
}