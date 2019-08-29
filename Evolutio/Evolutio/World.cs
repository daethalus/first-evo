using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Evolutio
{
    public class World
    {
        public Dictionary<Vector3, Tile> Tiles = new Dictionary<Vector3, Tile>();
        
        public World()
        {
            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    var item = "ground";
                    if (j % 2 == 0 && i % 3 == 0)
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