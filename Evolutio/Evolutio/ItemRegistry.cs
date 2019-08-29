using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Evolutio
{
    public class ItemRegistry
    {
        private Dictionary<string, Item> Items {get; set; }

        public ItemRegistry()
        {
            Items = new Dictionary<string, Item>();
            addItem(new Item {Name = "ground",SourceRectangle = new Rectangle(0,0,16,16)});
            addItem(new Item {Name = "water",SourceRectangle = new Rectangle(0,16,16,16)});
        }

        public void addItem(Item item)
        {
            Items.Add(item.Name,item);
        }

        public Item findItem(string name)
        {
            return Items[name];
        }
    }
}