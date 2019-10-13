using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Evolutio
{
    public class ItemRegistry
    {
        public static Item STONE;
        public static Item GROUND;
        public static Item DETAIL;
        public static Item BUSH;
        public static Item CHESS_TILE;
        
        
        public static Dictionary<string, Item> Items {get; set; } // this need to be private

        public void LoadContent(ContentManager Content)
        {
            var overworld = Content.Load<Texture2D>("Overworld");
            var testTexture = Content.Load<Texture2D>("test-texture");
            var testTexture2 = Content.Load<Texture2D>("test-texture2");
            var inner = Content.Load<Texture2D>("Inner");
            var wall = Content.Load<Texture2D>("walls");
            var spriteTest = Content.Load<Texture2D>("sprite-test");
            var draggy = Content.Load<Texture2D>("draggy");
            
            Items = new Dictionary<string, Item>();
            var ground = new Item {Texture2D = testTexture, Name = "ground", CanWalk = true, GroundItem = true};
            ground.addSourceRectangle(new Rectangle(0, 0, 32, 32));
            GROUND = addItem(ground);
            
            
            addItem(new Item {Texture2D = testTexture, Name = "water",SourceRectangle = new Rectangle(32,0,32, 32), CanWalk = false});
            
            var wave = new Item {Texture2D = overworld, Name = "wave", CanWalk = false, AnimationTime = 300};
            
            wave.addSourceRectangle(new Rectangle(0, 16, 16, 16));
            wave.addSourceRectangle(new Rectangle(16, 16, 16, 16));
            wave.addSourceRectangle(new Rectangle(32, 16, 16, 16));
            wave.addSourceRectangle(new Rectangle(48, 16, 16, 16));
            
            addItem(wave);
            
            DETAIL = addItem(new Item {Texture2D = overworld, Name = "other",SourceRectangle = new Rectangle(16,0,16,16), CanWalk = true, TotalDurability = 50});
            BUSH = addItem(new Item {Texture2D = overworld, Name = "bush",SourceRectangle = new Rectangle(32,224,16,16), CanWalk = false, TotalDurability = 200});
            STONE = addItem(new Item {Texture2D = overworld, Name = "stone",SourceRectangle = new Rectangle(112,80,16,16), CanWalk = false, TotalDurability = 300});
            
            addItem(new Item {Texture2D = Content.Load<Texture2D>("tree-pt2-2-test"), Name = "tree",SourceRectangle = new Rectangle(0,0,80,96), CanWalk = false, origin = new Vector2(34,80),TotalDurability = 500, CanPick = false});


            CHESS_TILE = new Item {Texture2D = inner, Name = "chess-tile", SourceRectangle = new Rectangle(0,0,16,16),GroundItem = true, CanWalk = true};
            addItem(CHESS_TILE);
            
            addItem(new Item {Texture2D = inner, Name = "wood-tile", SourceRectangle = new Rectangle(0,16,16,16),GroundItem = true, CanWalk = true});            
            
            addItem(new Item {Texture2D = spriteTest, Name = "wall", SourceRectangle = new Rectangle(0,0,64,64),GroundItem = false, CanWalk = false, origin = new Vector2(32, 32)});
            
            
            
            addItem(new Item {Texture2D = wall, Name = "wall2", SourceRectangle = new Rectangle(0,16,16,16),GroundItem = false, CanWalk = false});
            addItem(new Item {Texture2D = wall, Name = "wall3", SourceRectangle = new Rectangle(128,48,16,16),GroundItem = false, CanWalk = true});
            addItem(new Item {Texture2D = wall, Name = "wall4", SourceRectangle = new Rectangle(32,0,16,16),GroundItem = false, CanWalk = false});
            addItem(new Item {Texture2D = wall, Name = "wall5", SourceRectangle = new Rectangle(32,16,16,16),GroundItem = false, CanWalk = false});
            addItem(new Item {Texture2D = wall, Name = "door1", SourceRectangle = new Rectangle(246,16,16,16),GroundItem = false, CanWalk = true});
            
            
            addItem(new Item {Texture2D = draggy, Name = "draggy", SourceRectangle = new Rectangle(256,320,64,64),GroundItem = false, CanWalk = false});
            
            addItem(new Item {Texture2D = inner, Name = "plant", SourceRectangle = new Rectangle(128,194,16,32),GroundItem = false, CanWalk = false, origin = new Vector2(0, 16)});
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