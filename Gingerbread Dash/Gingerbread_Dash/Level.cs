using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gingerbread_Dash
{
    class Level
    {
        string path;
        string[,] elements;
        int cols;

        int x = 135;
        int speed;

        Coin[,] coins;
        Platform[,] platforms;
        Platform lastPlatform;
        Shield[,] shields;
        Boost[,] boosts;
        Bush[,] bushes;
        Cake[,] cakes;
        Candy[,] candies;
        Cabbage[,] cabbages;
        Carrot[,] carrots;

        Texture2D coinTexture, oneText, twoText, threeText, fourText, fiveText, shieldTexture, boostText, bushText, cakeText, candyText, cabbageText, carrotText;

        int start;

        public Level(string p, int spd, Texture2D coinText, Texture2D oneTexture, Texture2D twoTexture, Texture2D threeTexture, Texture2D fourTexture, Texture2D fiveTexture, Texture2D shieldText, Texture2D boostTexture, Texture2D bushTexture, Texture2D cakeTexture, Texture2D candyTexture, Texture2D cabbageTexture, Texture2D carrotTexture, int s)
        {
            start = s;
            path = p;
            cols = 0;
            elements = new string[100, 10];
            elements = ReadFile(path);
            speed = spd;

            coins = new Coin[100, 10];
            platforms = new Platform[100, 10];
            shields = new Shield[100, 10];
            boosts = new Boost[100, 10];
            bushes = new Bush[100, 10];
            cakes = new Cake[100, 10];
            candies = new Candy[100, 10];
            cabbages = new Cabbage[100, 10];
            carrots = new Carrot[100, 10];

            coinTexture = coinText;
            oneText = oneTexture;
            twoText = twoTexture;
            threeText = threeTexture;
            fourText = fourTexture;
            fiveText = fiveTexture;
            shieldTexture = shieldText;
            boostText = boostTexture;
            bushText = bushTexture;
            cakeText = cakeTexture;
            candyText = candyTexture;
            cabbageText = cabbageTexture;
            carrotText = carrotTexture;

            BuildLevel(coinTexture, oneText, twoText, threeText, fourText, fiveText, shieldTexture, boostText, bushText, cakeText, candyText, cabbageText, carrotText);


            Console.WriteLine($"Level created: {path} at {start}");
        }

        public string[,] ReadFile(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] items = line.Split(' ');

                        for (int i = 0; i < items.Length; i++)
                        {
                            elements[i, cols] = items[i];
                            //Console.WriteLine("item: " + elements[i, cols]);
                        }

                        cols += 1;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Cannot read file");
            }

            return elements;

        }

        public void BuildLevel(Texture2D coi, Texture2D T1, Texture2D T2, Texture2D T3, Texture2D T4, Texture2D T5, Texture2D shieldT, Texture2D boostT, Texture2D bushT, Texture2D cakeT, Texture2D candyT, Texture2D cabbageT, Texture2D carrotT)
        {
            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {

                    if (elements[i, j] != null && elements[i, j].Equals("1"))
                    {
                        Platform platform = new Platform(T1, new Rectangle((i * 80) + start, x + j * 45, 80, 30), speed);
                        platforms[i, j] = platform;
                        Console.WriteLine("strat:" + start);
                        lastPlatform = platforms[i, j];
                    }

                    if (elements[i, j] != null && elements[i, j].Equals("2"))
                    {
                        Platform platform = new Platform(T2, new Rectangle((i * 80) + start, x + j * 45, 80, 30), speed);
                        platforms[i, j] = platform;
                        Console.WriteLine("strat:" + start);
                        lastPlatform = platforms[i, j];
                    }

                    if (elements[i, j] != null && elements[i, j].Equals("3"))
                    {
                        Platform platform = new Platform(T3, new Rectangle((i * 80) + start, x + j * 45, 80, 30), speed);
                        platforms[i, j] = platform;
                        Console.WriteLine("strat:" + start);
                        lastPlatform = platforms[i, j];
                    }

                    if (elements[i, j] != null && elements[i, j].Equals("4"))
                    {
                        Platform platform = new Platform(T4, new Rectangle((i * 80) + start, x + j * 45, 80, 30), speed);
                        platforms[i, j] = platform;
                        Console.WriteLine("strat:" + start);
                        lastPlatform = platforms[i, j];
                    }

                    if (elements[i, j] != null && elements[i, j].Equals("5"))
                    {
                        Platform platform = new Platform(T5, new Rectangle((i * 80) + start, x + j * 45, 80, 30), speed);
                        platforms[i, j] = platform;
                        Console.WriteLine("strat:" + start);
                        lastPlatform = platforms[i, j];
                    }

                    if (elements[i, j] != null && elements[i, j].Equals("c"))
                    {
                        Coin coin = new Coin(new Rectangle(30 + (i * 80) + start, x + j * 45, 20, 20), coi, speed);
                        coins[i, j] = coin;
                    }

                    if (elements[i, j] != null && elements[i, j].Equals("r"))
                    {
                        Shield shield = new Shield(new Rectangle(30 + (i * 80) + start, x + j * 45, 50, 50), shieldT, speed);
                        shields[i, j] = shield;
                    }

                    if (elements[i, j] != null && elements[i, j].Equals("m"))
                    {
                        Boost boosting = new Boost(new Rectangle(30 + (i * 80) + start, x + j * 45, 50, 50), boostT, speed);
                        boosts[i, j] = boosting;
                    }

                    if (elements[i, j] != null && elements[i, j].Equals("b"))
                    {
                        Bush bush = new Bush(new Rectangle(30 + (i * 80) + start, x + j * 45, 50, 50), bushT, speed);
                        bushes[i, j] = bush;
                    }

                    if (elements[i, j] != null && elements[i, j].Equals("p"))
                    {
                        Cake cake = new Cake(new Rectangle(30 + (i * 80) + start, x + j * 45, 50, 50), cakeT, speed);
                        cakes[i, j] = cake;
                    }

                    if (elements[i, j] != null && elements[i, j].Equals("a"))
                    {
                        Candy candy = new Candy(new Rectangle(30 + (i * 80) + start, x + j * 45, 50, 50), candyT, speed);
                        candies[i, j] = candy;
                    }

                    if (elements[i, j] != null && elements[i, j].Equals("g"))
                    {
                        Cabbage cabbage = new Cabbage(new Rectangle(30 + (i * 80) + start, x + j * 45, 50, 50), cabbageT, speed);
                        cabbages[i, j] = cabbage;
                    }

                    if (elements[i, j] != null && elements[i, j].Equals("t"))
                    {
                        Carrot carrot = new Carrot(new Rectangle(30 + (i * 80) + start, x + j * 45, 50, 50), carrotT, speed);
                        carrots[i, j] = carrot;
                    }
                }
            }

            Console.WriteLine("Last X: " + GetLastLevel());
        }

        public void setSpeed(int spd)
        {
            for (int i = 0; i < platforms.GetLength(0); i++)
            {
                for (int j = 0; j < platforms.GetLength(1); j++)
                {
                    if (platforms[i, j] != null)
                    {
                        platforms[i, j].setSpeed(spd);
                    }

                    if (coins[i, j] != null)
                    {
                        coins[i, j].setSpeed(spd);
                    }

                    if (shields[i, j] != null)
                    {
                        shields[i, j].setSpeed(spd);
                    }

                    if (boosts[i, j] != null)
                    {
                        boosts[i, j].setSpeed(spd);
                    }

                    if (bushes[i, j] != null)
                    {
                        bushes[i, j].setSpeed(spd);
                    }

                    if (cakes[i, j] != null)
                    {
                        cakes[i, j].setSpeed(spd);
                    }

                    if (candies[i, j] != null)
                    {
                        candies[i, j].setSpeed(spd);
                    }

                    if (cabbages[i, j] != null)
                    {
                        cabbages[i, j].setSpeed(spd);
                    }

                    if (carrots[i, j] != null)
                    {
                        carrots[i, j].setSpeed(spd);
                    }
                }
            }
        }

        public Rectangle GetLatPlatformRect()
        {
            return lastPlatform.GetRectangle();
        }
        public int GetLastLevel()
        {
            int lastX = 0;

            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {

                    if (elements[i, j] != null && elements[i, j].Equals("."))
                    {
                        lastX = start + ((i + 1) * 80);

                    }


                }
            }

            return lastX;
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < platforms.GetLength(0); i++)
            {
                for (int j = 0; j < platforms.GetLength(1); j++)
                {

                    if (platforms[i, j] != null)
                    {
                        platforms[i, j].Draw(spriteBatch);
                    }

                    if (coins[i, j] != null)
                    {
                        coins[i, j].Draw(spriteBatch);
                    }

                    if (shields[i, j] != null)
                    {
                        shields[i, j].Draw(spriteBatch);
                    }

                    if (boosts[i, j] != null)
                    {
                        boosts[i, j].Draw(spriteBatch);
                    }

                    if (bushes[i, j] != null)
                    {
                        bushes[i, j].Draw(spriteBatch);
                    }

                    if (cakes[i, j] != null)
                    {
                        cakes[i, j].Draw(spriteBatch);
                    }

                    if (candies[i, j] != null)
                    {
                        candies[i, j].Draw(spriteBatch);
                    }

                    if (cabbages[i, j] != null)
                    {
                        cabbages[i, j].Draw(spriteBatch);
                    }

                    if (carrots[i, j] != null)
                    {
                        carrots[i, j].Draw(spriteBatch);
                    }
                }
            }

        }

        public void Update()
        {
            for (int i = 0; i < coins.GetLength(0); i++)
            {
                for (int j = 0; j < coins.GetLength(1); j++)
                {
                    if (coins[i, j] != null)
                    {
                        Console.WriteLine("pasesd" + i + " " + j);
                        coins[i, j].Move();
                    }

                    if (platforms[i, j] != null)
                    {
                        platforms[i, j].Move();
                    }

                    if (shields[i, j] != null)
                    {
                        shields[i, j].Move();
                    }

                    if (boosts[i, j] != null)
                    {
                        boosts[i, j].Move();
                    }

                    if (bushes[i, j] != null)
                    {
                        bushes[i, j].Move();
                    }

                    if (cakes[i, j] != null)
                    {
                        cakes[i, j].Move();
                    }

                    if (candies[i, j] != null)
                    {
                        candies[i, j].Move();
                    }
                    if (cabbages[i, j] != null)
                    {
                        cabbages[i, j].Move();
                    }
                    if (carrots[i, j] != null)
                    {
                        carrots[i, j].Move();
                    }
                }
            }
        }

        public int GetEndX()
        {
            int lastX = 0;
            for (int i = 0; i < platforms.GetLength(0); i++)
            {
                for (int j = 0; j < platforms.GetLength(1); j++)
                {
                    if (platforms[i, j] != null)
                    {
                        int platformRight = platforms[i, j].GetRectangle().Right;
                        if (platformRight > lastX)
                        {
                            lastX = platformRight;
                        }
                    }
                }
            }
            return lastX;
        }

        public Platform[,] GetPlatforms()
        {
            return platforms;
        }

        public Coin[,] GetCoins()
        {
            return coins;
        }

        public Shield[,] GetShields()
        {
            return shields;
        }

        public Boost[,] GetBoosts()
        {
            return boosts;
        }

        public Bush[,] GetBushes()
        {
            return bushes;
        }
        public Cake[,] GetCakes()
        {
            return cakes;
        }

        public Candy[,] GetCandies()
        {
            return candies;
        }

        public Cabbage[,] GetCabbages()
        {
            return cabbages;
        }

        public Carrot[,] GetCarrots()
        {
            return carrots;
        }


        public void RemoveCoin(int i, int j)
        {
            coins[i, j] = null;
        }
        public void RemoveShield(int i, int j)
        {
            shields[i, j] = null;
        }
        public void RemoveBoost(int i, int j)
        {
            boosts[i, j] = null;
        }
        public void RemoveBush(int i, int j)
        {
            bushes[i, j] = null;
        }
        public void RemoveCake(int i, int j)
        {
            cakes[i, j] = null;
        }
        public void RemoveCandy(int i, int j)
        {
            candies[i, j] = null;
        }
        public void RemoveCabbage(int i, int j)
        {
            cabbages[i, j] = null;
        }

        public void RemoveCarrot(int i, int j)
        {
            carrots[i, j] = null;
        }

    }
}
