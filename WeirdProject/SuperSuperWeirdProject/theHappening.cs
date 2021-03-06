﻿using Microsoft.Xna.Framework;
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
        public int livingCycle { get; set; }
        public int breedingCycle { get; set; }
        public int hungerCycle { get; set; }
        public bool isHungry { get; set; }
        private Color color;
        private Texture2D texture;

        public theCube()
        {

        }

        public theCube(Game1 g, Color color, int width, int height, int x, int y, bool hunger)
        {
            this.g = g;
            this.width = width;
            this.height = height;
            this.x = x;
            this.y = y;
            this.color = color;
            isHungry = hunger;
            livingCycle = 0;
            breedingCycle = 0;
            hungerCycle = 0;
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
        public int getLivingCycle() { return livingCycle; }
        public Color getCubeColor() { return color; }

        public void setX(int x) { this.x = x; }
        public void setY(int y) { this.y = y; }
        public void setLivingCycle(int livingCycle) { this.livingCycle = livingCycle; }
    }

    class theHappening
    {
        private const int DEATH_RATE = 10; //bigger number == living longer
        private const int BIRTH_RATE = 50; //lower number == breeding faster
        private const int HUNGER_RATE = 10;

        private Game1 g;
        private int screenWidth, screenHeight;
        private int mapWidth, mapHeight;
        private int tileWidth, tileHeight;
        private float firstTime = 0;
        private float lastTime = 0;
        private Random rand = new Random();

        private theCube[] cubeArray;
        private List<theCube> cubeList;
        private mapData[] mapArray;
        private bool[] isOccupied;

        public theHappening(Game1 g, int screenWidth, int screenHeight)
        {
            this.g = g;
            tileWidth = 5;
            tileHeight = 5;

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
            firstTime = gameTime.TotalGameTime.Milliseconds * 200;

                for (int y = 0; y < mapHeight; y++)
                {
                    for (int x = 0; x < mapWidth; x++)
                    {
                        if (firstTime != lastTime)
                        {
                            MoveCubes(x, y);
                            BreedingCubes(x, y);
                            EatCubes(x, y);
                            Death(x, y);
                        }
                    }
                }

            lastTime = firstTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for(int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    //implement a try catch for the null objects
                    if (cubeArray[x + y * mapWidth].getTexture() != null)
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
                        state.LeftButton == ButtonState.Pressed && !isOccupied[x + y * mapWidth])
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
                        if (kState.IsKeyDown(Keys.NumPad3))
                        {
                            selectedColor = Blue();
                        }

                        cubeList.Add(new theCube(g, selectedColor, tileWidth, tileHeight, mapArray[x + y * mapWidth].x, mapArray[x + y * mapWidth].y, true));
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
        private void MoveCubes(int x, int y)
        {
            int direction = rand.Next(1, 5);

            switch (direction)
            {
                case 1:
                    if (cubeArray[x + y * mapWidth].getTexture() != null &&
                        cubeArray[x + y * mapWidth].getX() > 0 &&
                        !isOccupied[(x - 1) + y * mapWidth])
                    {
                        cubeArray[x + y * mapWidth].setX(cubeArray[x + y * mapWidth].getX() - tileWidth);
                        cubeArray[(x - 1) + y * mapWidth] = cubeArray[x + y * mapWidth];
                        cubeArray[x + y * mapWidth] = new theCube();
                        isOccupied[x + y * mapWidth] = false;
                        isOccupied[(x - 1) + y * mapWidth] = true;
                    }
                    break;
                case 2:
                    if (cubeArray[x + y * mapWidth].getTexture() != null &&
                        cubeArray[x + y * mapWidth].getX() + tileWidth < screenWidth &&
                        !isOccupied[(x + 1) + y * mapWidth])
                    {
                        cubeArray[x + y * mapWidth].setX(cubeArray[x + y * mapWidth].getX() + tileWidth);
                        cubeArray[(x + 1) + y * mapWidth] = cubeArray[x + y * mapWidth];
                        cubeArray[x + y * mapWidth] = new theCube();
                        isOccupied[x + y * mapWidth] = false;
                        isOccupied[(x + 1) + y * mapWidth] = true;
                    }
                    break;
                case 3:
                    if (cubeArray[x + y * mapWidth].getTexture() != null &&
                        cubeArray[x + y * mapWidth].getY() > 0 &&
                        !isOccupied[x + (y - 1) * mapWidth])
                    {
                        cubeArray[x + y * mapWidth].setY(cubeArray[x + y * mapWidth].getY() - tileHeight);
                        cubeArray[x + (y - 1) * mapWidth] = cubeArray[x + y * mapWidth];
                        cubeArray[x + y * mapWidth] = new theCube();
                        isOccupied[x + y * mapWidth] = false;
                        isOccupied[x + (y - 1) * mapWidth] = true;
                    }
                    break;
                case 4:
                    if (cubeArray[x + y * mapWidth].getTexture() != null &&
                        cubeArray[x + y * mapWidth].getY() + tileHeight < screenHeight &&
                        !isOccupied[x + (y + 1) * mapWidth])
                    {
                        cubeArray[x + y * mapWidth].setY(cubeArray[x + y * mapWidth].getY() + tileHeight);
                        cubeArray[x + (y + 1) * mapWidth] = cubeArray[x + y * mapWidth];
                        cubeArray[x + y * mapWidth] = new theCube();
                        isOccupied[x + y * mapWidth] = false;
                        isOccupied[x + (y + 1) * mapWidth] = true;
                    }
                    break;
            }
        }
        private void BreedingCubes(int x, int y)
        {
            if (cubeArray[x + y * mapWidth].getTexture() != null &&
                        cubeArray[x + y * mapWidth].isHungry == false &&
                        cubeArray[x + y * mapWidth].getX() > 0 &&
                        cubeArray[x + y * mapWidth].getX() + tileWidth < screenWidth &&
                        cubeArray[x + y * mapWidth].getY() > 0 &&
                        cubeArray[x + y * mapWidth].getY() + tileHeight < screenHeight)
            {
                if (!isOccupied[(x - 1) + y * mapWidth])
                {

                }
            }
        }

        private void EatCubes(int x, int y)
        {
            if (cubeArray[x + y * mapWidth].getTexture() != null &&
                               cubeArray[x + y * mapWidth].isHungry &&
                               cubeArray[x + y * mapWidth].getX() > 0 &&
                               cubeArray[x + y * mapWidth].getX() + tileWidth < screenWidth &&
                               cubeArray[x + y * mapWidth].getY() > 0 &&
                               cubeArray[x + y * mapWidth].getY() + tileHeight < screenHeight)
            {
                //red
                if (isOccupied[(x - 1) + y * mapWidth] && cubeArray[x + y * mapWidth].getCubeColor().R != cubeArray[(x - 1) + y * mapWidth].getCubeColor().R)
                {
                    cubeArray[x + y * mapWidth].isHungry = false;
                    cubeArray[(x - 1) + y * mapWidth] = new theCube();
                    isOccupied[(x - 1) + y * mapWidth] = false;
                }

                //green
                if (isOccupied[(x - 1) + y * mapWidth] && cubeArray[x + y * mapWidth].getCubeColor().G != cubeArray[(x - 1) + y * mapWidth].getCubeColor().G)
                {
                    cubeArray[x + y * mapWidth].isHungry = false;
                    cubeArray[(x - 1) + y * mapWidth] = null;
                    isOccupied[(x - 1) + y * mapWidth] = false;
                }

                //blue
                if (isOccupied[(x - 1) + y * mapWidth] && cubeArray[x + y * mapWidth].getCubeColor().B != cubeArray[(x - 1) + y * mapWidth].getCubeColor().B)
                {
                    cubeArray[x + y * mapWidth].isHungry = false;
                    cubeArray[(x - 1) + y * mapWidth] = null;
                    isOccupied[(x - 1) + y * mapWidth] = false;
                }
            }
        }

        private void Death(int x, int y)
        {
            if (cubeArray[x + y * mapWidth].livingCycle > DEATH_RATE && cubeArray[x + y * mapWidth].isHungry == true)
            {
                cubeArray[x + y * mapWidth] = new theCube();
                cubeArray[x + y * mapWidth].isHungry = false;
                cubeArray[x + y * mapWidth].livingCycle = 0;
                isOccupied[x + y * mapWidth] = false;
            }
            else
            {
                cubeArray[x + y * mapWidth].livingCycle++;
            }
        }
    }
}
