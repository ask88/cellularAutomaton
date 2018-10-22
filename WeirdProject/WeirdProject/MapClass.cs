using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WeirdProject
{
    public class Block
    {
        public bool isVisible { get; set; }
        private int tw, th;
        private Game1 g;
        private Texture2D filledBlock;
        private Color blockColor;

        public Block(Game1 g, Color blockColor, int tw, int th)
        {
            this.g = g;
            this.blockColor = blockColor;
            this.tw = tw;
            this.th = th;
            block();
        }

        public void setColor(Color blockColor) { this.blockColor = blockColor; }
        public int getTileWidth() { return tw; }
        public int getTileHeight() { return th; }
        public Texture2D getBlock() { return filledBlock; }

        private void block()
        {
            Texture2D b = new Texture2D(g.GraphicsDevice, tw, th);
            Color[] colorData = new Color[tw * th];
            for(int i = 0; i < colorData.Length; i++)
            {
                colorData[i] = blockColor;
            }
            b.SetData<Color>(colorData);
            filledBlock = b;
        }

    }

    public class MapClass
    {
        public Game1 g;
        private int ScreenWidth;
        private int ScreenHeight;
        private int MapWidth;
        private int MapHeight;
        private int TileWidth;
        private int TileHeight;
        private Block[] block;

      public MapClass(Game1 g, int TileWidth, int TileHeight)
        {
            ScreenWidth = g.getWidth();
            ScreenHeight = g.getHeight();
            MapWidth = ScreenWidth / TileWidth;
            MapHeight = ScreenHeight / TileHeight;

            block = new Block[MapWidth * MapHeight];
            for(int y = 0; y < MapHeight; y++)
            {
                for(int x = 0; x < MapWidth; x++)
                {
                    block[x + y * MapWidth] = null;
                }
            }

            this.TileWidth = TileWidth;
            this.TileHeight = TileHeight;
        }

        public void Load()
        {

        }

        public void Unload()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for(int y = 0; y < MapHeight; y++)
            {
                for(int x = 0; x < MapWidth; x++)
                {
                    spriteBatch.Draw(block[x + y * MapWidth].getBlock(), new Vector2(x * TileWidth, y * TileHeight), Color.White);
                }
            }           
            spriteBatch.End();
        }

        private void DrawBlock()
        {
            MouseState state = Mouse.GetState();

            for(int y = 0; y < MapHeight; y++)
            {
                for(int x = 0; x < MapWidth; x++)
                {
                    if (state.LeftButton == ButtonState.Pressed &&
                state.Position.X < )
                    {

                    }
                }
            }  
        }

        private Color Black()
        {
            int r = 0;
            int g = 0;
            int b = 0;
            int a = 255;

            return new Color(r, g, b, a);
        }
        private Color Red()
        {
            int r = 255;
            int g = 0;
            int b = 0;
            int a = 255;

            return new Color(r, g, b, a);
        }
        private Color Green()
        {
            int r = 0;
            int g = 255;
            int b = 0;
            int a = 255;

            return new Color(r, g, b, a);
        }
        private Color Blue()
        {
            int r = 0;
            int g = 0;
            int b = 255;
            int a = 255;

            return new Color(r, g, b, a);
        }
    }
}
