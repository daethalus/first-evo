using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Evolutio
{
    public class Item
    {
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

        public Rectangle GetSourceRectangle(GameTime time, Vector3 position)
        {
            if (SourceRectangles.Count == 0)
            {
                return SourceRectangle;
            }

            if (AnimationTime > 0)
            {
                if ((int) time.TotalGameTime.TotalMilliseconds % AnimationTime == 0)
                {
                    spriteIndex++;
                    if (spriteIndex >= SourceRectangles.Count)
                    {
                        spriteIndex = 0;
                    }
                }
            }
            return SourceRectangles[spriteIndex];
        }
    }
}