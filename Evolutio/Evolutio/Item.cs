using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evolutio
{
    public class Item
    {
        public Texture2D Texture2D { get; set; }

        public Vector2 origin { get; set; }

        public int TotalDurability { get; set; }

        public Item()
        {
            origin = Vector2.Zero;
        }

        private int spriteIndex;
        public string Name { get; set; }

        private readonly List<Rectangle> SourceRectangles = new List<Rectangle>();

        public Rectangle SourceRectangle { get; set; }

        public bool CanWalk { get; set; }

        public Int16 AnimationTime { get; set; }

        public Item addSourceRectangle(Rectangle rectangle)
        {
            SourceRectangles.Add(rectangle);
            return this;
        }
        
        public void Animate(GameTime time)
        {
            if (AnimationTime > 0)
            {
                if (time.TotalGameTime.Milliseconds % AnimationTime == 0)
                {
                    spriteIndex++;
                    if (spriteIndex >= SourceRectangles.Count)
                    {
                        spriteIndex = 0;
                    }
                }
            }
        }
        

        public Rectangle GetSourceRectangle(Vector3 position)
        {
            if (SourceRectangles.Count == 0)
            {
                return SourceRectangle;
            }

            if (AnimationTime > 0)
            { 
                return SourceRectangles[spriteIndex];
            }
            
            return SourceRectangles[0];
        }


        public ItemState createState()
        {
            return new ItemState(this);
        }
    }
}