using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperSuperWeirdProject
{
    class theCube
    {
        private Game1 g;
        private int x, y, width, height;
        private Color color;
        private Texture2D texture;
        public bool isDisplay { get; set; }

        public theCube(Game1 g, Color color, int width, int height)
        {
            this.g = g;
            this.width = width;
            this.height = height;
            this.color = color;
            cubeTexture();
        }

        private void cubeTexture()
        {
            Texture2D b = new Texture2D(g.GraphicsDevice, width, height);
            Color[] colorData = new Color[b.Width * b.Height];
            for(int i = 0; i < colorData.Length; i++)
            {
                colorData[i] = color;
            }
            b.SetData<Color>(colorData);
            texture = b;
        }

       
        public Texture2D getTexture() { return texture; }
        public int getX() { return x; }
        public int getY() { return y; }
        public void setX(int x) { this.x = x; }
        public void setY(int y) { this.y = y; }
    }

    class theHappening
    {
        private int screenWidth, screenHeight;
        private int mapWidth, mapHeight;
        private int tileWidth, tileHeight;
        private theCube[] cubeArray;

        public theHappening(Game1 g, int screenWidth, int screenHeight)
        {
            tileWidth = 10;
            tileHeight = 10;

            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            mapWidth = screenWidth / tileWidth;
            mapHeight = screenHeight / tileHeight;

            cubeArray = new theCube[mapWidth * mapHeight];
            for(int y = 0; y < mapHeight; y++)
            {
                for(int x = 0; x < mapWidth; x++)
                {
                    cubeArray[x + y * mapWidth] = new theCube(g, Black(), tileWidth, tileHeight);
                    cubeArray[x + y * mapWidth].setX(tileWidth * x);
                    cubeArray[x + y * mapWidth].setY(tileHeight * y);
                }
            }
        }

        public void Load()
        {

        }

        public void Unload()
        {

        }

        public void Update(GameTime gameTime)
        {
            DrawCube();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for(int y = 0; y < mapHeight; y++)
            {
                for(int x = 0; x < mapWidth; x++)
                {
                    if (cubeArray[x + y * mapWidth].isDisplay)
                    {
                        spriteBatch.Draw(cubeArray[x + y * mapWidth].getTexture(), new Vector2(cubeArray[x + y * mapWidth].getX(), cubeArray[x + y * mapWidth].getY()), Color.White);
                    }
                }
            }
            spriteBatch.End();
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
        private void DrawCube()
        {
            MouseState state = Mouse.GetState();

            for(int y = 0; y < mapHeight; y++)
            {
                for(int x = 0; x < mapWidth; x++)
                {
                    if(state.Position.X > cubeArray[x + y * mapWidth].getX() &&
                        state.Position.X < cubeArray[x + y * mapWidth].getX() + tileWidth &&
                        state.Position.Y > cubeArray[x + y * mapWidth].getY() &&
                        state.Position.Y < cubeArray[x + y * mapWidth].getY() + tileHeight &&
                        state.LeftButton == ButtonState.Pressed)
                    {
                        cubeArray[x + y * mapWidth].isDisplay = true;
                    }
                }
            }
        }
    }
}
