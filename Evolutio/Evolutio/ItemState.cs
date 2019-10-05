using System;

namespace Evolutio
{
    public class ItemState
    {
        private int durability;

        public Item Item { get; private set; }

        public ItemState(Item item)
        {
            Item = item;
            durability = item.TotalDurability;
        }

        public bool CanWalk()
        {
            return Item.CanWalk;
        }

        public int ApplyDamage(int damage)
        {
            if (Item.TotalDurability == 0)
            {
                return int.MaxValue;
            }
            
            return durability -= damage;
        }
    }
}