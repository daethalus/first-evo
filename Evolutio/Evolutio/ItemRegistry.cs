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
            var ground = new Item {Name = "ground", CanWalk = true};
            ground.addSourceRectangle(new Rectangle(0, 0, 16, 16));
            ground.addSourceRectangle(new Rectangle(112, 144, 16, 16));
            ground.addSourceRectangle(new Rectangle(0, 0, 16, 16));
            ground.addSourceRectangle(new Rectangle(0, 0, 16, 16));
            addItem(ground);
            
            
            addItem(new Item {Name = "water",SourceRectangle = new Rectangle(48,112,16,16), CanWalk = false});
            
            var wave = new Item {Name = "wave", CanWalk = false, AnimationTime = 300};
            
            wave.addSourceRectangle(new Rectangle(0, 16, 16, 16));
            wave.addSourceRectangle(new Rectangle(16, 16, 16, 16));
            wave.addSourceRectangle(new Rectangle(32, 16, 16, 16));
            wave.addSourceRectangle(new Rectangle(48, 16, 16, 16));
            
            addItem(wave);
            
            addItem(new Item {Name = "other",SourceRectangle = new Rectangle(16,0,16,16), CanWalk = true});
            addItem(new Item {Name = "bush",SourceRectangle = new Rectangle(32,224,16,16), CanWalk = true});
            addItem(new Item {Name = "stone",SourceRectangle = new Rectangle(112,80,16,16), CanWalk = true});
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