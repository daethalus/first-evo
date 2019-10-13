using System;
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

        public void GenerateChunk(PlaneNoiseMapBuilder mapBuilder, Random random)
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
                    var position = new Vector3(ChunkPosition.X * 16 + x, ChunkPosition.Y * 16 + y, 0);

                    var item = "ground";
                    List<ItemStack> Items = new List<ItemStack>();

                    if (value < 0)
                    {
                        item = "water";
//                        if (random.Next(30) == 0)
//                        {
//                            item = "wave";
//                        }
//                        else
//                        {
//                            
//                        }
                    }
                    else
                    {
//                        int rand = random.Next(60);
//                        var added = false;
//                        if ( rand == 0)
//                        {
//                            Items.Add(Evolutio.ItemRegistry.findItem("bush").createItemStack());
//                            added = true;
//                        }
//                        
//                        if (rand == 1)
//                        {
//                            Items.Add(Evolutio.ItemRegistry.findItem("stone").createItemStack());
//                            added = true;
//                        }
//
//                        if (!added)
//                        {
//                            rand = random.Next(100); 
//
//                            if (rand == 2)
//                            {
//                                Items.Add(Evolutio.ItemRegistry.findItem("tree").createItemStack());
//                            }
//                        }
                    }

                    if (x == 15 || y == 15)
                    {
                        //   item = "other";
                    }

                    AddTile(new Tile
                    {
                        Ground = Evolutio.ItemRegistry.findItem(item).createItemStack(),
                        Position = position,
                        Items = Items,
                        PickableItems = new List<ItemStack>()
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