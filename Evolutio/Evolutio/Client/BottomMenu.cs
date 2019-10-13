using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Serilog;

namespace Evolutio.Client
{
    public class BottomMenu : ClientBehavior
    
    {
        private Texture2D bottomMenu;
        private Texture2D itemSelection;
        private int selectedSlot = 0;
        private KeyboardState oldKeybordState;
        private MouseState oldMouseState;

        public Evolutio Evolutio { private get; set; }
        private Player Player { get { return Evolutio.Player;}}

        public void LoadContent(ContentManager Content)
        {
            bottomMenu = Content.Load<Texture2D>("botton-menu");
            itemSelection = Content.Load<Texture2D>("item-selection");
        }

        public void Update(GameTime gameTime)
        {
            for (var x = 1; x <= 10; x++)
            {
                var ax = x;
                if (ax == 10) ax = 0;
                var key = (Keys) System.Enum.Parse(typeof(Keys), "D" + ax);
                if (oldKeybordState.IsKeyUp(key) && Keyboard.GetState().IsKeyDown(key))
                {
                    SelectSlot(x -1);
                }                
            }

            if (oldMouseState.ScrollWheelValue != Mouse.GetState().ScrollWheelValue)
            {
                var diff = oldMouseState.ScrollWheelValue - Mouse.GetState().ScrollWheelValue;
                if (diff > 0)
                {
                    SelectSlot(selectedSlot + 1);
                }
                else
                {
                    SelectSlot(selectedSlot - 1);
                }
            }
            
            var stack = ItemRegistry.CHESS_TILE.createItemStack();
            stack.ChangeQuantity(64);
            Player.GiveItem(stack);
            
            stack = ItemRegistry.Items["wood-tile"].createItemStack();
            stack.ChangeQuantity(64);
            Player.GiveItem(stack);
            
            stack = ItemRegistry.Items["wall"].createItemStack();
            stack.ChangeQuantity(64);
            Player.GiveItem(stack);
            
            stack = ItemRegistry.Items["wall2"].createItemStack();
            stack.ChangeQuantity(64);
            Player.GiveItem(stack);
            
            stack = ItemRegistry.Items["wall3"].createItemStack();
            stack.ChangeQuantity(64);
            Player.GiveItem(stack);
            
            stack = ItemRegistry.Items["wall4"].createItemStack();
            stack.ChangeQuantity(64);
            Player.GiveItem(stack);
            
            stack = ItemRegistry.Items["wall5"].createItemStack();
            stack.ChangeQuantity(64);
            Player.GiveItem(stack);
            
            stack = ItemRegistry.Items["door1"].createItemStack();
            stack.ChangeQuantity(64);
            Player.GiveItem(stack);
            
            stack = ItemRegistry.Items["plant"].createItemStack();
            stack.ChangeQuantity(64);
            Player.GiveItem(stack);
            
            
            oldKeybordState = Keyboard.GetState();
            oldMouseState = Mouse.GetState();
        }

        public void DrawGame(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        private Vector2 GetMenuPosition()
        {
            return new Vector2((Evolutio.graphics.PreferredBackBufferWidth * 0.5f) - (90 * 3),
                Evolutio.graphics.PreferredBackBufferHeight - (20 * 3));
        }

        public void DrawFixed(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var zoom = 3f;
            spriteBatch.Draw(bottomMenu, 
                GetMenuPosition(),
                new Rectangle(0,0,180,18),
                Color.White,
                0f, 
                new Vector2(0, 0),
                zoom,
                SpriteEffects.None,
                0f);


            for (var x = 0; x < 10; x++)
            {
                var stack = Player.ButtomStacks[x];
                if (stack != null)
                {
                    var itemPosition = GetMenuPosition();
                    var ax = (x * (16 * (int)zoom)) + (x * (2 * (int) zoom)) + (1 * zoom);
                    itemPosition += new Vector2(ax,3);

                    var rect = stack.Item.GetSourceRectangle(new Vector3(0, 0, 0));

                    var xx = rect.Height >> 4;

                    var azoom = (zoom / xx);
                    
                    spriteBatch.Draw(stack.Item.Texture2D,
                        itemPosition,
                        rect,
                        Color.White,
                        0f, 
                        stack.Item.origin,
                        azoom,
                        SpriteEffects.None,
                        0f);

                    if (stack.Quantity > 1)
                    {
                        itemPosition += new Vector2(10 * zoom, 10* zoom);
                        spriteBatch.DrawString(Evolutio.font, string.Format("{0:D2}", stack.Quantity), itemPosition, Color.White,0f,new Vector2(0,0),0.8f,SpriteEffects.None,0f);    
                    }
                }
            }

            var selectionPosition = GetMenuPosition();

            var axx =(selectedSlot * (16 * (int)zoom)) + (selectedSlot * (2 * (int) zoom));
            
            selectionPosition += new Vector2(axx, 0);
            
            spriteBatch.Draw(itemSelection, 
                selectionPosition,
                new Rectangle(0,0,18,18),
                Color.White,
                0f, 
                new Vector2(0, 0),
                zoom,
                SpriteEffects.None,
                0f);
            
            
        }

        public void SelectSlot(int slot)
        {
            selectedSlot = slot;
            if (selectedSlot > 9)
            {
                selectedSlot = 0;
            } else if (selectedSlot < 0)
            {
                selectedSlot = 9;
            }
        }
        public void PlaceItem(int slot, ItemStack itemStack)
        {
            Player.ButtomStacks[slot] = itemStack;
        }
        
        public ItemStack GetSelectecItem()
        {
            return Player.ButtomStacks[selectedSlot];
        }
    }
}