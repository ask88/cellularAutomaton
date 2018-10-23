using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SuperSuperWeirdProject
{
    struct mapData
    {
        public int x;
        public int y;
    }

    class theCube
    {
        private Game1 g;
        private int x, y, width, height;
        private Color color;
        private Texture2D texture;
        public bool hasBred{ get; set; }
        public bool isMovingUp { get; set; }
        public bool isMovingDown { get; set; }
        public bool isMovingLeft { get; set; }
        public bool isMovingRight { get; set; }

        public theCube(Game1 g, Color color, int width, int height, int x, int y)
        {
            this.g = g;
            this.width = width;
            this.height = height;
            this.x = x;
            this.y = y;
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
        public Color getCubeColor() { return color; }
        public void setX(int x) { this.x = x; }
        public void setY(int y) { this.y = y; }
    }

    class theHappening
    {
        private Game1 g;
        private int screenWidth, screenHeight;
        private int mapWidth, mapHeight;
        private int tileWidth, tileHeight;
        private float firstTime = 0;
        private float lastTime = 0;
        private Random rand = new Random();

        private List<theCube> cubeList;
        private mapData[] mapArray;

        public theHappening(Game1 g, int screenWidth, int screenHeight)
        {
            this.g = g;
            tileWidth = 10;
            tileHeight = 10;

            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            mapWidth = screenWidth / tileWidth;
            mapHeight = screenHeight / tileHeight;

            cubeList = new List<theCube>();
            mapArray = new mapData[mapWidth * mapHeight];

            for(int y = 0; y < mapHeight; y++)
            {
                for(int x = 0; x < mapWidth; x++)
                {
                    mapArray[x + y * mapWidth] = new mapData();
                    mapArray[x + y * mapWidth].x = (tileWidth * x);
                    mapArray[x + y * mapWidth].y = (tileHeight * y);
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
            ManipulateCube();
            firstTime = gameTime.TotalGameTime.Seconds;
            if(firstTime!=lastTime)
            {
                MoveCubes();
                BreedingCubes();
                lastTime = firstTime;
                if(cubeList.Count > 0)
                    Console.WriteLine(cubeList[0].getX());
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for(int i = 0; i < cubeList.Count; i++)
            {
                spriteBatch.Draw(cubeList[i].getTexture(), new Vector2(cubeList[i].getX(), cubeList[i].getY()), Color.White);
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
        private Color Yellow()
        {
            int r = 255;
            int g = 255;
            int b = 0;
            int a = 255;

            return new Color(r, g, b, a);
        }
        private Color Teal()
        {
            int r = 0;
            int g = 255;
            int b = 255;
            int a = 255;

            return new Color(r, g, b, a);
        }
        private Color Purple()
        {
            int r = 255;
            int g = 0;
            int b = 255;
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
        private void ManipulateCube()
        {
            MouseState state = Mouse.GetState();
            
            KeyboardState kState = Keyboard.GetState();
            Color selectedColor = Black();

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (state.Position.X > mapArray[x + y * mapWidth].x &&
                        state.Position.X < mapArray[x + y * mapWidth].x + tileWidth &&
                        state.Position.Y > mapArray[x + y * mapWidth].y &&
                        state.Position.Y < mapArray[x + y * mapWidth].y + tileHeight &&
                        state.LeftButton == ButtonState.Pressed)
                    {
                        if(kState.IsKeyDown(Keys.NumPad0))
                        {
                            selectedColor = Black();
                        }
                        if(kState.IsKeyDown(Keys.NumPad1))
                        {
                            selectedColor = Red();
                        }
                        if(kState.IsKeyDown(Keys.NumPad2))
                        {
                            selectedColor = Green();
                        }
                        if(kState.IsKeyDown(Keys.NumPad3))
                        {
                            selectedColor = Blue();
                        }
                        cubeList.Add(new theCube(g, selectedColor, tileWidth, tileHeight, mapArray[x + y * mapWidth].x, mapArray[x + y * mapWidth].y));
                    }
                    if (state.Position.X > mapArray[x + y * mapWidth].x &&
                        state.Position.X < mapArray[x + y * mapWidth].x + tileWidth &&
                        state.Position.Y > mapArray[x + y * mapWidth].y &&
                        state.Position.Y < mapArray[x + y * mapWidth].y + tileHeight &&
                        state.RightButton == ButtonState.Pressed)
                    {
                        //cubeArray[x + y * mapWidth].isDisplay = false;
                    }
                }
            }
        }
        private void MoveCubes()
        {
            int redMulti = 0;
            int blueMulti = 0;
            int greenMulti = 0;
            int blackMulti = 0;

            for(int i = 0; i < cubeList.Count; i++)
            {
                int direction = rand.Next(0, 101);

                switch (direction)
                {
                    case int n when (n < (25 - blackMulti) && cubeList[i].getCubeColor().Equals(Black())):
                        cubeList[i].isMovingUp = true;
                        cubeList[i].isMovingDown = false;
                        cubeList[i].isMovingLeft = false;
                        cubeList[i].isMovingRight = false;
                        break;
                    case int n when (n > (24 - blackMulti) && n < 50) && cubeList[i].getCubeColor().Equals(Black()):
                        cubeList[i].isMovingUp = false;
                        cubeList[i].isMovingDown = true;
                        cubeList[i].isMovingLeft = false;
                        cubeList[i].isMovingRight = false;
                        break;
                    case int n when (n > 49 && n < 75) && cubeList[i].getCubeColor().Equals(Black()):
                        cubeList[i].isMovingUp = false;
                        cubeList[i].isMovingDown = false;
                        cubeList[i].isMovingLeft = true;
                        cubeList[i].isMovingRight = false;
                        break;
                    case int n when (n > 74 && n < 101) && cubeList[i].getCubeColor().Equals(Black()):
                        cubeList[i].isMovingUp = false;
                        cubeList[i].isMovingDown = false;
                        cubeList[i].isMovingLeft = false;
                        cubeList[i].isMovingRight = true;
                        break;
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case int n when (n < (25 + redMulti) && cubeList[i].getCubeColor().Equals(Red())):
                        cubeList[i].isMovingUp = true;
                        cubeList[i].isMovingDown = false;
                        cubeList[i].isMovingLeft = false;
                        cubeList[i].isMovingRight = false;
                        break;
                    case int n when (n > (24 + redMulti) && n < 50) && cubeList[i].getCubeColor().Equals(Red()):
                        cubeList[i].isMovingUp = false;
                        cubeList[i].isMovingDown = true;
                        cubeList[i].isMovingLeft = false;
                        cubeList[i].isMovingRight = false;
                        break;
                    case int n when (n > 49 && n < 75) && cubeList[i].getCubeColor().Equals(Red()):
                        cubeList[i].isMovingUp = false;
                        cubeList[i].isMovingDown = false;
                        cubeList[i].isMovingLeft = true;
                        cubeList[i].isMovingRight = false;
                        break;
                    case int n when (n > 74 && n < 101) && cubeList[i].getCubeColor().Equals(Red()):
                        cubeList[i].isMovingUp = false;
                        cubeList[i].isMovingDown = false;
                        cubeList[i].isMovingLeft = false;
                        cubeList[i].isMovingRight = true;
                        break;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    case int n when (n < 25 && cubeList[i].getCubeColor().Equals(Green())):
                        cubeList[i].isMovingUp = true;
                        cubeList[i].isMovingDown = false;
                        cubeList[i].isMovingLeft = false;
                        cubeList[i].isMovingRight = false;
                        break;
                    case int n when (n > 24 && n < 50) && cubeList[i].getCubeColor().Equals(Green()):
                        cubeList[i].isMovingUp = false;
                        cubeList[i].isMovingDown = true;
                        cubeList[i].isMovingLeft = false;
                        cubeList[i].isMovingRight = false;
                        break;
                    case int n when (n > 49 && n < (75 + greenMulti)) && cubeList[i].getCubeColor().Equals(Green()):
                        cubeList[i].isMovingUp = false;
                        cubeList[i].isMovingDown = false;
                        cubeList[i].isMovingLeft = true;
                        cubeList[i].isMovingRight = false;
                        break;
                    case int n when (n > (74 + greenMulti) && n < 101) && cubeList[i].getCubeColor().Equals(Green()):
                        cubeList[i].isMovingUp = false;
                        cubeList[i].isMovingDown = false;
                        cubeList[i].isMovingLeft = false;
                        cubeList[i].isMovingRight = true;
                        break;

                    case int n when (n < 25 && cubeList[i].getCubeColor().Equals(Blue())):
                        cubeList[i].isMovingUp = true;
                        cubeList[i].isMovingDown = false;
                        cubeList[i].isMovingLeft = false;
                        cubeList[i].isMovingRight = false;
                        break;
                    case int n when (n > 24 && n < 50) && cubeList[i].getCubeColor().Equals(Blue()):
                        cubeList[i].isMovingUp = false;
                        cubeList[i].isMovingDown = true;
                        cubeList[i].isMovingLeft = false;
                        cubeList[i].isMovingRight = false;
                        break;
                    case int n when (n > 49 && n < (75 - blueMulti)) && cubeList[i].getCubeColor().Equals(Blue()):
                        cubeList[i].isMovingUp = false;
                        cubeList[i].isMovingDown = false;
                        cubeList[i].isMovingLeft = true;
                        cubeList[i].isMovingRight = false;
                        break;
                    case int n when (n > (74 - blueMulti) && n < 101) && cubeList[i].getCubeColor().Equals(Blue()):
                        cubeList[i].isMovingUp = false;
                        cubeList[i].isMovingDown = false;
                        cubeList[i].isMovingLeft = false;
                        cubeList[i].isMovingRight = true;
                        break;

                    case int n when (n < (25 - blackMulti) && cubeList[i].getCubeColor().Equals(Purple())):
                        cubeList[i].isMovingUp = true;
                        cubeList[i].isMovingDown = false;
                        cubeList[i].isMovingLeft = false;
                        cubeList[i].isMovingRight = false;
                        break;
                    case int n when (n > (24 - blackMulti) && n < 50) && cubeList[i].getCubeColor().Equals(Purple()):
                        cubeList[i].isMovingUp = false;
                        cubeList[i].isMovingDown = true;
                        cubeList[i].isMovingLeft = false;
                        cubeList[i].isMovingRight = false;
                        break;
                    case int n when (n > 49 && n < 75) && cubeList[i].getCubeColor().Equals(Purple()):
                        cubeList[i].isMovingUp = false;
                        cubeList[i].isMovingDown = false;
                        cubeList[i].isMovingLeft = true;
                        cubeList[i].isMovingRight = false;
                        break;
                    case int n when (n > 74 && n < 101) && cubeList[i].getCubeColor().Equals(Purple()):
                        cubeList[i].isMovingUp = false;
                        cubeList[i].isMovingDown = false;
                        cubeList[i].isMovingLeft = false;
                        cubeList[i].isMovingRight = true;
                        break;
                }

                if (cubeList[i].isMovingUp && cubeList[i].getY() > 0)
                {
                    cubeList[i].setY(cubeList[i].getY() - tileHeight);
                }
                else if (cubeList[i].isMovingDown && cubeList[i].getY() + tileHeight < screenHeight)
                {
                    cubeList[i].setY(cubeList[i].getY() + tileHeight);
                }
                else if (cubeList[i].isMovingLeft && cubeList[i].getX() > 0)
                {
                    cubeList[i].setX(cubeList[i].getX() - tileWidth);
                }
                else if (cubeList[i].isMovingRight && cubeList[i].getX() + tileWidth < screenWidth)
                {
                    cubeList[i].setX(cubeList[i].getX() + tileWidth);
                }
            }
        }
        private bool CollidingCubesUp()
        {
            for (int i = 0; i < cubeList.Count; i++)
            {
                for (int j = 0; j < cubeList.Count; j++)
                {
                    if ((cubeList[i].getY() - tileHeight).Equals(cubeList[j].getY()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool CollidingCubesDown()
        {
            for (int i = 0; i < cubeList.Count; i++)
            {
                for (int j = 0; j < cubeList.Count; j++)
                {
                    if ((cubeList[i].getY() + tileHeight).Equals(cubeList[j].getY()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool CollidingCubesLeft()
        {
            for (int i = 0; i < cubeList.Count; i++)
            {
                for (int j = 0; j < cubeList.Count; j++)
                {
                    if ((cubeList[i].getX() - tileWidth).Equals(cubeList[j].getX()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool CollidingCubesRight()
        {
            for (int i = 0; i < cubeList.Count; i++)
            {
                for (int j = 0; j < cubeList.Count; j++)
                {
                    if ((cubeList[i].getX() + tileWidth).Equals(cubeList[j].getX()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void BreedingCubes()
        {
            for(int i = 0; i < cubeList.Count; i++)
            {
                for(int j = 0; j < cubeList.Count; j++)
                {
                    if ((cubeList[i].getX() + tileWidth).Equals(cubeList[j].getX()) &&
                        cubeList[i].getCubeColor().Equals(Black()) && cubeList[j].getCubeColor().Equals(Red()) && !cubeList[i].hasBred)
                    {
                        cubeList[i].hasBred = true;
                        cubeList.Add(new theCube(g, Blue(), tileWidth, tileHeight, cubeList[i].getX(), cubeList[i].getY() + 1));
                    }

                    if ((cubeList[i].getX() + tileWidth).Equals(cubeList[j].getX()) &&
                        cubeList[i].getCubeColor().Equals(Red()) && cubeList[j].getCubeColor().Equals(Blue()) && !cubeList[i].hasBred)
                    {
                        cubeList[i].hasBred = true;
                        cubeList.Add(new theCube(g, Purple(), tileWidth, tileHeight, cubeList[i].getX(), cubeList[i].getY() + 1));
                    }
                }
            }
        }
    }
}
