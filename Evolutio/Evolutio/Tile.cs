using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Evolutio
{
    public class Tile
    {
        public Vector3 Position { get; set; }
        public ItemState Ground { get; set; }
        
        public List<ItemState> Items { get; set; }

        private bool canWalk = true;

        public bool CanWalk()
        {
            if (!canWalk)
            {
                return false;
            }

            if (!Ground.CanWalk())
            {
                return false;
            }

            foreach (var item in Items)
            {
                if (!item.CanWalk())
                {
                    return false;
                }
            }

            return true;
        }
    }
}