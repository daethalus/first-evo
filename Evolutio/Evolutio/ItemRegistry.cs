using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Evolutio
{
    public class ItemRegistry
    {

        public static Item GROUND;
        public static Item BUSH;
        public static Item WATER;
        public static Item STONE;

        public static Item TREE;


        public Dictionary<string, Item> Items {get; set; } // this need to be private

        public ItemRegistry()
        {
            Items = new Dictionary<string, Item>();
        }

        public void LoadContent(ContentManager Content)
        {
            var world = Content.Load<Texture2D>("world");
            var tree = Content.Load<Texture2D>("tree");
            
            GROUND = new Item {Texture2D = world, Name = "ground", CanWalk = true, GroundItem = true, SourceRectangle = new Rectangle(0, 32, 32, 32)};
            addItem(GROUND);
            
            WATER = new Item {Texture2D = world, Name = "water", CanWalk = false, GroundItem = true, SourceRectangle = new Rectangle(0, 96, 32, 32)};
            addItem(WATER);
          
            BUSH = new Item {Texture2D = world, Name = "bush", CanWalk = false, GroundItem = false,  SourceRectangle = new Rectangle(0,0,32,32)};
            addItem(BUSH);
            
            STONE = new Item {Texture2D = world, Name = "stone", CanWalk = false, GroundItem = false,  SourceRectangle = new Rectangle(0,64,32,32)};
            addItem(STONE);
            
            TREE = new Item {Texture2D = tree, Name = "tree", CanWalk = false, GroundItem = false,  SourceRectangle = new Rectangle(0,0,128,128), origin = new Vector2(90,84)};
            addItem(TREE);
        }
        
        public Item addItem(Item item)
        {
            Items.Add(item.Name,item);
            return item;
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