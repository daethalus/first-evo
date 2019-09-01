using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;
using SharpNoise;
using SharpNoise.Builders;
using SharpNoise.Modules;
using Color = System.Drawing.Color;

namespace Evolutio
{
    public class World
    {
        public Dictionary<Vector3, Tile> Tiles = new Dictionary<Vector3, Tile>();

        public int ax = 16;
        public int ay = 16;
        public int OctaveCount = 5;
        public double Frequency = 0.03d;
        public double Persistence = 0.025;
        public double Scale = 8;
        public double Bias = 1;
        public double LowerBound = -0.1;
        public double UpperBound = 15;

        public void GenerateMap()
        {
            try
            {
                Tiles.Clear();
                var noiseSource = new Perlin
                {
                    Seed = new Random().Next(),
                    OctaveCount = OctaveCount,
                    Frequency = Frequency,
                    //Persistence = Persistence
                };

                ScaleBias mScabi = new ScaleBias();
                mScabi.Source0 = noiseSource;
                mScabi.Scale = Scale;
                mScabi.Bias = Bias;
//                
                Clamp mClamp = new Clamp();
                mClamp.LowerBound = LowerBound;
                mClamp.UpperBound = UpperBound;
                mClamp.Source0 = mScabi;


                var noiseMap = new NoiseMap();
                var noiseMapBuilder = new PlaneNoiseMapBuilder
                {
                    DestNoiseMap = noiseMap,
                    SourceModule = mClamp,
                    EnableSeamless = false,
                    //   SourceModule = noiseSource
                };

                noiseMapBuilder.SetDestSize(16, 16);
//                noiseMapBuilder.SetDestSize(1280, 720);
//                noiseMapBuilder.SetBounds(-3, 3, -2, 2);
//                noiseMapBuilder.Build();

                double minValue = Double.MaxValue;
                double maxValue = double.MinValue;


                for (var x = 0; x < 10; x++)
                {
                    for (var y = 0; y < 10; y++)
                    {
                        noiseMapBuilder.SetBounds(x * 16, x * 16 + 16, y * 16, y * 16 + 16);
                        noiseMapBuilder.Build();

                        for (var i = 0; i < 16; i++)
                        {
                            for (var j = 0; j < 16; j++)
                            {
                                double value = noiseMap.GetValue(i, j);

                                if (value < minValue)
                                {
                                    minValue = value;
                                }

                                if (value > maxValue)
                                {
                                    maxValue = value;
                                }

                                var item = "ground";

                                if (value < 0)
                                {
                                    item = "water";
                                }

                                if (i == 15 || j == 15)
                                {
                                    //   item = "other";
                                }

                                //Log.Debug("x {x}, y {y}, i {i}, j {j}",x,y,i,j);

                                addTile(new Tile
                                {
                                    Ground = Evolutio.ItemRegistry.findItem(item),
                                    Position = new Vector3(i + x * 16, j + y * 16, 0)
                                });
                            }
                        }
                    }
                }

                Log.Debug("max {max} : min {min}", maxValue, minValue);
            }
            catch (Exception ex)
            {
                Log.Error("Error", ex);
            }
        }

        public Texture2D GenerateImageFromMap(GraphicsDevice device)
        {
            using (Bitmap b = new Bitmap(160, 160))
            {
                foreach (var tile in Tiles)
                {
                    if (tile.Value.Ground.Name == "water")
                    {
                        b.SetPixel((int) tile.Key.X, (int) tile.Key.Y, Color.Blue);
                    }
                    else if (tile.Value.Ground.Name == "other")
                    {
                        b.SetPixel((int) tile.Key.X, (int) tile.Key.Y, Color.Brown);
                    }
                    else
                    {
                        b.SetPixel((int) tile.Key.X, (int) tile.Key.Y, Color.Green);
                    }
                }

                MemoryStream ms = new MemoryStream();
                b.Save(ms, ImageFormat.Bmp);
                //b.Save(@"greyscale.bmp", ImageFormat.Bmp);
                return Texture2D.FromStream(device, ms);
            }
        }

        public World()
        {
            GenerateMap();
        }

        private void addTile(Tile tile)
        {
            //Log.Debug("added tile at {position}", tile.Position);
            Tiles.Add(tile.Position, tile);
        }
    }
}