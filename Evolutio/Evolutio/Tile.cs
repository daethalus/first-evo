using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Evolutio
{
    public class Tile
    {
        public Vector3 Position { get; set; }
        public Item Ground { get; set; }
        
        public List<Item> Items { get; set; }
    }
}