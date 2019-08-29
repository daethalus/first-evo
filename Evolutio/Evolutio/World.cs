using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SharpNoise;
using SharpNoise.Builders;
using SharpNoise.Modules;

namespace Evolutio
{
    public class World
    {
        public Dictionary<Vector3, Tile> Tiles = new Dictionary<Vector3, Tile>();

       
        
        public World()
        {
            var noiseSource = new Perlin
            {
                Seed = new Random().Next()
            };
            
            
            var noiseMap = new NoiseMap();
            var noiseMapBuilder = new PlaneNoiseMapBuilder
            {
                DestNoiseMap = noiseMap,
                SourceModule = noiseSource
            };
            
            noiseMapBuilder.SetDestSize(100, 100);
            
            noiseMapBuilder.SetBounds(-3, 3, -2, 2);
            
            noiseMapBuilder.Build();
            
            
            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {


                    double value = noiseMap.GetValue(i, j);

                    var item = "ground";
                    if (value < 0)
                    {
                        item = "water";
                    }
                    
              
                    
                    addTile(new Tile {Ground = Evolutio.ItemRegistry.findItem(item), Position = new Vector3(i, j, 0)});
                }
            }
        }

        private void addTile(Tile tile)
        {
            Tiles.Add(tile.Position,tile);
        }
    }
}