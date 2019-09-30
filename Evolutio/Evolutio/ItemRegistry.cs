using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Evolutio
{
    public class ItemRegistry
    {
        private Dictionary<string, Item> Items {get; set; }

        public void LoadContent(ContentManager Content)
        {
            var overworld = Content.Load<Texture2D>("Overworld");
            
            Items = new Dictionary<string, Item>();
            var ground = new Item {Texture2D = overworld, Name = "ground", CanWalk = true};
            ground.addSourceRectangle(new Rectangle(0, 0, 16, 16));
            ground.addSourceRectangle(new Rectangle(112, 144, 16, 16));
            ground.addSourceRectangle(new Rectangle(0, 0, 16, 16));
            ground.addSourceRectangle(new Rectangle(0, 0, 16, 16));
            addItem(ground);
            
            
            addItem(new Item {Texture2D = overworld, Name = "water",SourceRectangle = new Rectangle(48,112,16,16), CanWalk = false});
            
            var wave = new Item {Texture2D = overworld, Name = "wave", CanWalk = false, AnimationTime = 300};
            
            wave.addSourceRectangle(new Rectangle(0, 16, 16, 16));
            wave.addSourceRectangle(new Rectangle(16, 16, 16, 16));
            wave.addSourceRectangle(new Rectangle(32, 16, 16, 16));
            wave.addSourceRectangle(new Rectangle(48, 16, 16, 16));
            
            addItem(wave);
            
            addItem(new Item {Texture2D = overworld, Name = "other",SourceRectangle = new Rectangle(16,0,16,16), CanWalk = true});
            addItem(new Item {Texture2D = overworld, Name = "bush",SourceRectangle = new Rectangle(32,224,16,16), CanWalk = false});
            addItem(new Item {Texture2D = overworld, Name = "stone",SourceRectangle = new Rectangle(112,80,16,16), CanWalk = false});
            
            addItem(new Item {Texture2D = Content.Load<Texture2D>("tree-pt2-2-test"), Name = "tree",SourceRectangle = new Rectangle(0,0,80,96), CanWalk = false, origin = new Vector2(32,80)});
        }
        
        public void addItem(Item item)
        {
            Items.Add(item.Name,item);
        }

        public Item findItem(string name)
        {
            return Items[name];
        }

        public void Animate(GameTime time)
        {
            foreach (var item in Items.Values)
            {
                item.Animate(time);
            }
        }
    }
}