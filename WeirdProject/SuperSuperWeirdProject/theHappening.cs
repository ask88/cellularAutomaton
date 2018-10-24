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

        public theCube()
        {

        }

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

        private theCube[] cubeArray;
        private mapData[] mapArray;
        private bool[] isOccupied;

        public theHappening(Game1 g, int screenWidth, int screenHeight)
        {
            this.g = g;
            tileWidth = 10;
            tileHeight = 10;

            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            mapWidth = screenWidth / tileWidth;
            mapHeight = screenHeight / tileHeight;

            cubeArray = new theCube[mapWidth * mapHeight];
            mapArray = new mapData[mapWidth * mapHeight];
            isOccupied = new bool[mapWidth * mapHeight];

            for(int y = 0; y < mapHeight; y++)
            {
                for(int x = 0; x < mapWidth; x++)
                {
                    mapArray[x + y * mapWidth] = new mapData();
                    mapArray[x + y * mapWidth].x = (tileWidth * x);
                    mapArray[x + y * mapWidth].y = (tileHeight * y);
                    cubeArray[x + y * mapWidth] = new theCube();
                    isOccupied[x + y * mapWidth] = false;
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
                //BreedingCubes();
                lastTime = firstTime;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for(int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if ((cubeArray[x + y * mapWidth].getTexture() != null))
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
                        cubeArray[x + y * mapWidth] = new theCube(g, selectedColor, tileWidth, tileHeight, mapArray[x + y * mapWidth].x, mapArray[x + y * mapWidth].y);
                        isOccupied[x + y * mapWidth] = true;
                    }
                    if (state.Position.X > mapArray[x + y * mapWidth].x &&
                        state.Position.X < mapArray[x + y * mapWidth].x + tileWidth &&
                        state.Position.Y > mapArray[x + y * mapWidth].y &&
                        state.Position.Y < mapArray[x + y * mapWidth].y + tileHeight &&
                        state.RightButton == ButtonState.Pressed)
                    {
                        //cubeList.Remove();
                    }
                }
            }
        }
        private void MoveCubes()
        {
            for(int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    int direction = rand.Next(0, 101);

                    switch (20/*direction*/)
                    {
                        case int n when (n < 25 && cubeArray[x+y*mapWidth].getCubeColor().Equals(Black())):
                            cubeArray[x + y * mapWidth].isMovingUp = true;
                            cubeArray[x + y * mapWidth].isMovingDown = false;
                            cubeArray[x + y * mapWidth].isMovingLeft = false;
                            cubeArray[x + y * mapWidth].isMovingRight = false;
                            break;
                        case int n when (n > 24 && n < 50) && cubeArray[x + y * mapWidth].getCubeColor().Equals(Black()):
                            cubeArray[x + y * mapWidth].isMovingUp = false;
                            cubeArray[x + y * mapWidth].isMovingDown = true;
                            cubeArray[x + y * mapWidth].isMovingLeft = false;
                            cubeArray[x + y * mapWidth].isMovingRight = false;
                            break;
                        case int n when (n > 49 && n < 75) && cubeArray[x + y * mapWidth].getCubeColor().Equals(Black()):
                            cubeArray[x + y * mapWidth].isMovingUp = false;
                            cubeArray[x + y * mapWidth].isMovingDown = false;
                            cubeArray[x + y * mapWidth].isMovingLeft = true;
                            cubeArray[x + y * mapWidth].isMovingRight = false;
                            break;
                        case int n when (n > 74 && n < 101) && cubeArray[x + y * mapWidth].getCubeColor().Equals(Black()):
                            cubeArray[x + y * mapWidth].isMovingUp = false;
                            cubeArray[x + y * mapWidth].isMovingDown = false;
                            cubeArray[x + y * mapWidth].isMovingLeft = false;
                            cubeArray[x + y * mapWidth].isMovingRight = true;
                            break;
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        case int n when (n < 25 && cubeArray[x + y * mapWidth].getCubeColor().Equals(Red())):
                            cubeArray[x + y * mapWidth].isMovingUp = true;
                            cubeArray[x + y * mapWidth].isMovingDown = false;
                            cubeArray[x + y * mapWidth].isMovingLeft = false;
                            cubeArray[x + y * mapWidth].isMovingRight = false;
                            break;
                        case int n when (n > 24 && n < 50) && cubeArray[x + y * mapWidth].getCubeColor().Equals(Red()):
                            cubeArray[x + y * mapWidth].isMovingUp = false;
                            cubeArray[x + y * mapWidth].isMovingDown = true;
                            cubeArray[x + y * mapWidth].isMovingLeft = false;
                            cubeArray[x + y * mapWidth].isMovingRight = false;
                            break;
                        case int n when (n > 49 && n < 75) && cubeArray[x + y * mapWidth].getCubeColor().Equals(Red()):
                            cubeArray[x + y * mapWidth].isMovingUp = false;
                            cubeArray[x + y * mapWidth].isMovingDown = false;
                            cubeArray[x + y * mapWidth].isMovingLeft = true;
                            cubeArray[x + y * mapWidth].isMovingRight = false;
                            break;
                        case int n when (n > 74 && n < 101) && cubeArray[x + y * mapWidth].getCubeColor().Equals(Red()):
                            cubeArray[x + y * mapWidth].isMovingUp = false;
                            cubeArray[x + y * mapWidth].isMovingDown = false;
                            cubeArray[x + y * mapWidth].isMovingLeft = false;
                            cubeArray[x + y * mapWidth].isMovingRight = true;
                            break;
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        case int n when (n < 25 && cubeArray[x + y * mapWidth].getCubeColor().Equals(Green())):
                            cubeArray[x + y * mapWidth].isMovingUp = true;
                            cubeArray[x + y * mapWidth].isMovingDown = false;
                            cubeArray[x + y * mapWidth].isMovingLeft = false;
                            cubeArray[x + y * mapWidth].isMovingRight = false;
                            break;
                        case int n when (n > 24 && n < 50) && cubeArray[x + y * mapWidth].getCubeColor().Equals(Green()):
                            cubeArray[x + y * mapWidth].isMovingUp = false;
                            cubeArray[x + y * mapWidth].isMovingDown = true;
                            cubeArray[x + y * mapWidth].isMovingLeft = false;
                            cubeArray[x + y * mapWidth].isMovingRight = false;
                            break;
                        case int n when (n > 49 && n < 75) && cubeArray[x + y * mapWidth].getCubeColor().Equals(Green()):
                            cubeArray[x + y * mapWidth].isMovingUp = false;
                            cubeArray[x + y * mapWidth].isMovingDown = false;
                            cubeArray[x + y * mapWidth].isMovingLeft = true;
                            cubeArray[x + y * mapWidth].isMovingRight = false;
                            break;
                        case int n when (n > 74 && n < 101) && cubeArray[x + y * mapWidth].getCubeColor().Equals(Green()):
                            cubeArray[x + y * mapWidth].isMovingUp = false;
                            cubeArray[x + y * mapWidth].isMovingDown = false;
                            cubeArray[x + y * mapWidth].isMovingLeft = false;
                            cubeArray[x + y * mapWidth].isMovingRight = true;
                            break;
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        case int n when (n < 25 && cubeArray[x + y * mapWidth].getCubeColor().Equals(Blue())):
                            cubeArray[x + y * mapWidth].isMovingUp = true;
                            cubeArray[x + y * mapWidth].isMovingDown = false;
                            cubeArray[x + y * mapWidth].isMovingLeft = false;
                            cubeArray[x + y * mapWidth].isMovingRight = false;
                            break;
                        case int n when (n > 24 && n < 50) && cubeArray[x + y * mapWidth].getCubeColor().Equals(Blue()):
                            cubeArray[x + y * mapWidth].isMovingUp = false;
                            cubeArray[x + y * mapWidth].isMovingDown = true;
                            cubeArray[x + y * mapWidth].isMovingLeft = false;
                            cubeArray[x + y * mapWidth].isMovingRight = false;
                            break;
                        case int n when (n > 49 && n < 75) && cubeArray[x + y * mapWidth].getCubeColor().Equals(Blue()):
                            cubeArray[x + y * mapWidth].isMovingUp = false;
                            cubeArray[x + y * mapWidth].isMovingDown = false;
                            cubeArray[x + y * mapWidth].isMovingLeft = true;
                            cubeArray[x + y * mapWidth].isMovingRight = false;
                            break;
                        case int n when (n > 74 && n < 101) && cubeArray[x + y * mapWidth].getCubeColor().Equals(Blue()):
                            cubeArray[x + y * mapWidth].isMovingUp = false;
                            cubeArray[x + y * mapWidth].isMovingDown = false;
                            cubeArray[x + y * mapWidth].isMovingLeft = false;
                            cubeArray[x + y * mapWidth].isMovingRight = true;
                            break;
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        case int n when (n < 25 && cubeArray[x + y * mapWidth].getCubeColor().Equals(Purple())):
                            cubeArray[x + y * mapWidth].isMovingUp = true;
                            cubeArray[x + y * mapWidth].isMovingDown = false;
                            cubeArray[x + y * mapWidth].isMovingLeft = false;
                            cubeArray[x + y * mapWidth].isMovingRight = false;
                            break;
                        case int n when (n > 24 && n < 50) && cubeArray[x + y * mapWidth].getCubeColor().Equals(Purple()):
                            cubeArray[x + y * mapWidth].isMovingUp = false;
                            cubeArray[x + y * mapWidth].isMovingDown = true;
                            cubeArray[x + y * mapWidth].isMovingLeft = false;
                            cubeArray[x + y * mapWidth].isMovingRight = false;
                            break;
                        case int n when (n > 49 && n < 75) && cubeArray[x + y * mapWidth].getCubeColor().Equals(Purple()):
                            cubeArray[x + y * mapWidth].isMovingUp = false;
                            cubeArray[x + y * mapWidth].isMovingDown = false;
                            cubeArray[x + y * mapWidth].isMovingLeft = true;
                            cubeArray[x + y * mapWidth].isMovingRight = false;
                            break;
                        case int n when (n > 74 && n < 101) && cubeArray[x + y * mapWidth].getCubeColor().Equals(Purple()):
                            cubeArray[x + y * mapWidth].isMovingUp = false;
                            cubeArray[x + y * mapWidth].isMovingDown = false;
                            cubeArray[x + y * mapWidth].isMovingLeft = false;
                            cubeArray[x + y * mapWidth].isMovingRight = true;
                            break;
                            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    }

                    if (cubeArray[x + y * mapWidth].isMovingUp && cubeArray[x + y * mapWidth].getY() > 0 && isOccupied[x + (y - 1) * mapWidth])
                    {
                        isOccupied[x + y * mapWidth] = false;
                        cubeArray[x + y * mapWidth].setY(cubeArray[x + y * mapWidth].getY() - tileHeight);
                        isOccupied[x + y * mapWidth] = true;
                    }
                    else if (cubeArray[x + y * mapWidth].isMovingDown && cubeArray[x + y * mapWidth].getY() + tileHeight < screenHeight)
                    {
                        isOccupied[x + y * mapWidth] = false;
                        cubeArray[x + y * mapWidth].setY(cubeArray[x + y * mapWidth].getY() + tileHeight);
                        isOccupied[x + y * mapWidth] = true;
                    }
                    else if (cubeArray[x + y * mapWidth].isMovingLeft && cubeArray[x + y * mapWidth].getX() > 0)
                    {
                        isOccupied[x + y * mapWidth] = false;
                        cubeArray[x + y * mapWidth].setX(cubeArray[x + y * mapWidth].getX() - tileWidth);
                        isOccupied[x + y * mapWidth] = true;
                    }
                    else if (cubeArray[x + y * mapWidth].isMovingRight && cubeArray[x + y * mapWidth].getX() + tileWidth < screenWidth)
                    {
                        isOccupied[x + y * mapWidth] = false;
                        cubeArray[x + y * mapWidth].setX(cubeArray[x + y * mapWidth].getX() + tileWidth);
                        isOccupied[x + y * mapWidth] = true;
                    }
                }
            }
        }
        /*
        private void BreedingCubes()
        {
            for(int y = 0; y < mapHeight; y++)
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
        */
    }
}
