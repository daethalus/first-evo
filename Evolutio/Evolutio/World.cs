using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;
using SharpNoise;
using SharpNoise.Builders;
using SharpNoise.Modules;

namespace Evolutio
{
    public class World
    {

        public int ax = 16;
        public int ay = 16;
        public int OctaveCount = 5;
        public double Frequency = 0.03d;
        public double Persistence = 0.025;
        public double Scale = 8;
        public double Bias = 1;
        public double LowerBound = -0.1;
        public double UpperBound = 15;

        private int _seed;
        private Random _random;
        
        private Perlin _perlin;
        private ScaleBias _scaleBias;
        private Clamp _clamp;
        private PlaneNoiseMapBuilder _builder;

        private Dictionary<Vector2, Chunk> ChunkMap = new Dictionary<Vector2, Chunk>();

        public World()
        {
            _seed = new Random().Next();
            _random = new Random(_seed);
            
            _perlin = new Perlin
            {
                Seed = _seed,
                OctaveCount = OctaveCount,
                Frequency = Frequency,
                //Persistence = Persistence
            };

            _scaleBias = new ScaleBias
            {
                Source0 = _perlin, 
                Scale = Scale, 
                Bias = Bias
            };

            _clamp = new Clamp
            {
                LowerBound = LowerBound, 
                UpperBound = UpperBound, 
                Source0 = _scaleBias
            };
            
            _builder =  new PlaneNoiseMapBuilder
            {
                DestNoiseMap = new NoiseMap(),
                SourceModule = _clamp,
                EnableSeamless = false
            };
            _builder.SetDestSize(Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE);
        }

        public void GenerateMap()
        {
            
        }

        public Tile GetTile(Vector3 vector3)
        {
            var chunk = GetChunkForTilePosition(vector3);
            Tile tile;
            chunk.TileMap.TryGetValue(vector3, out tile);
            return tile;
        }

        public Chunk GetChunkForTilePosition(Vector3 vector3)
        {
            return GetChunkForPosition(new Vector2((int) vector3.X >> 4, (int) vector3.Y >> 4));
        }
        
        public Chunk GetChunkForPosition(Vector2 chunkPosition)
        {
            Chunk chunk;
            if (ChunkMap.TryGetValue(chunkPosition, out chunk))
            {
                return chunk;
            }
            chunk = new Chunk
            {
                ChunkPosition = chunkPosition
            };
            chunk.GenerateChunk(_builder, _random);
            ChunkMap.Add(chunkPosition, chunk);
            return chunk;
        }
        
        

        public Texture2D GenerateImageFromMap(GraphicsDevice device)
        {
            return null;
//            using (Bitmap b = new Bitmap(160, 160))
//            {
//                foreach (var tile in Tiles)
//                {
//                    if (tile.Value.Ground.Name == "water")
//                    {
//                        b.SetPixel((int) tile.Key.X, (int) tile.Key.Y, Color.Blue);
//                    }
//                    else if (tile.Value.Ground.Name == "other")
//                    {
//                        b.SetPixel((int) tile.Key.X, (int) tile.Key.Y, Color.Brown);
//                    }
//                    else
//                    {
//                        b.SetPixel((int) tile.Key.X, (int) tile.Key.Y, Color.Green);
//                    }
//                }
//
//                MemoryStream ms = new MemoryStream();
//                b.Save(ms, ImageFormat.Bmp);
//                //b.Save(@"greyscale.bmp", ImageFormat.Bmp);
//                return Texture2D.FromStream(device, ms);
//            }
        }

        public void PlaceItem(Item item, Vector3 vector3)
        {
            var tile = GetTile(vector3);
            tile.Items.Add(item.createState());
        }

        public void PlaceGround(Item item, Vector3 vector3)
        {
            var tile = GetTile(vector3);
            tile.Ground = item.createState();
        }
    }
}