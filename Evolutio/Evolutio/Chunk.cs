using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SharpNoise.Builders;

namespace Evolutio
{
    public class Chunk
    {
        public const int CHUNK_SIZE = 16;

        public Vector2 ChunkPosition { get; set; }
        
        public Dictionary<Vector3, Tile> TileMap = new Dictionary<Vector3, Tile>();

        public void GenerateChunk(PlaneNoiseMapBuilder mapBuilder)
        {
            mapBuilder.SetBounds(
                ChunkPosition.X * 16, 
                ChunkPosition.X * 16 + 16, 
                ChunkPosition.Y * 16, 
                ChunkPosition.Y * 16 + 16);
            
            
            mapBuilder.Build();
            
            for (var x = 0; x < CHUNK_SIZE; x++)
            {
                for (var y = 0; y < CHUNK_SIZE; y++)
                {
                    
                    double value = mapBuilder.DestNoiseMap.GetValue(x, y);

                    var item = "ground";

                    if (value < 0)
                    {
                        item = "water";
                    }

                    if (x == 15 || y == 15)
                    {
                        //   item = "other";
                    }

                    AddTile(new Tile
                    {
                        Ground = Evolutio.ItemRegistry.findItem(item),
                        Position = new Vector3(ChunkPosition.X * 16 + x, ChunkPosition.Y * 16 + y, 0),
                    });
                }
            }
        }
        
        private void AddTile(Tile tile)
        {
            TileMap.Add(tile.Position, tile);
        }
    }
}