using System;

namespace Evolutio
{
    public class ItemStack
    {
        private int durability;
        private int quantity;

        public Item Item { get; private set; }

        public ItemStack(Item item)
        {
            Item = item;
            durability = item.TotalDurability;
            quantity = 0;
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