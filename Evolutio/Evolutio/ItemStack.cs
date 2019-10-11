using System;

namespace Evolutio
{
    public class ItemStack
    {
        private int durability;
        public int Quantity { get; private set; }

        public Item Item { get; private set; }

        public ItemStack(Item item)
        {
            Item = item;
            durability = item.TotalDurability;
            Quantity = 1;
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
        
        public int ChangeQuantity(int _quantity)
        {
            Quantity += _quantity;
            return Quantity;
        }
    }
}