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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level lv1, lv2, lv3, lv4, lv5;

        Rectangle titleRect;
        Texture2D titleTexture, startBTexture, instructionsBTexture, buttonTexture, gbOriginalTexture, gbTutuTexture, gbLeafTexture;
        Rectangle startBGRect, startButtonRect, instructionsButtonRect, editButtonRect, mouseRect, instructionsRect, backButton, backgroundRect, bubbleRect;
        Rectangle[] colorRects;
        Texture2D startBGTexture, editText, rectangleTexture, backgroundTexture, oneText, twoText, threeText, fourText, fiveText, coinText, shieldText, bubbleText, boostText, bushText, cakeText, candyText, cabbageText, carrotText;
        enum GameState { start, instructions, play, editCharacter, end };
        GameState gameState;
        SpriteFont startFont;

        KeyboardState oldKB;
        MouseState oldMouse;

        Gingerbread_Man gbMan;
        Boolean duck, isJumping, isOnGround;
        float velocity = -10f;
        float gravity = 0.3f;
        int maxJumpHeight = 133;
        int startY, score, boostingTimer, bushTimer, shieldTimer;
        float bubbleOpacity;
        Boolean shieldActivated, boostActivated, bushActivated, intersect;

        List<Level> activeLevels;
        string[] levelFiles = { @"Content/level1.txt", @"Content/level2.txt", @"Content/level3.txt", @"Content/level4.txt", @"Content/level4.txt" };
        int nextLevelIndex = 0;
        string highScore;
        SpriteFont spriteFont;
        Highscore hs;
        LeaderBoard leaderBoard;
        int lscore, candy;
        int coinScore;

        CoinTracker trackerC;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = (4000 / 3);
            graphics.PreferredBackBufferHeight = (1600 / 3);
            graphics.ApplyChanges();

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            startBGRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);//start screen background
            backgroundRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            startButtonRect = new Rectangle(900, 100, 250, 100);// start button
            mouseRect = new Rectangle(0, 0, 1, 1);// mouse coordinates to check if mouse is touching a button when pressed
            instructionsButtonRect = new Rectangle(900, 200, 250, 100);// instructions button
            instructionsRect = new Rectangle(50, 50, 700, 350);// Place for instructions
            editButtonRect = new Rectangle(900, 300, 250, 100);// Changing character colors button
            colorRects = new Rectangle[3];//Red, yellow, blue, green
            int x = 500;
            for(int i = 0; i < colorRects.Length; i++)
            {
                colorRects[i] = new Rectangle(x, 300, 60, 80);
                x += 150;
            }
            backButton = new Rectangle(10, GraphicsDevice.Viewport.Height - 60, 100, 50);// takes you back to start screen

            gameState = GameState.start;

            oldKB = Keyboard.GetState();
            oldMouse = Mouse.GetState();
            duck = false;
            isJumping = false;
            isOnGround = true;
            score = 0;

            shieldActivated = false;

            hs = new Highscore();
            highScore = hs.GetHighscore();

            leaderBoard = new LeaderBoard(0);
            lscore = 0;

            shieldActivated = false;
            boostActivated = false;
            bushActivated = false;
            bubbleRect = new Rectangle(0, 0, 0, 0);
            bubbleOpacity = 0.5f;
            intersect = false;

            trackerC = new CoinTracker();

            titleRect = new Rectangle(250, 80, 495, 222);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            startBGTexture = Content.Load<Texture2D>("candyLand");
            buttonTexture = Content.Load<Texture2D>("Button");
            startFont = Content.Load<SpriteFont>("SpriteFont1");
            rectangleTexture = Content.Load<Texture2D>("Rectangle");
            backgroundTexture = Content.Load<Texture2D>("bkg");
            coinText = Content.Load<Texture2D>("CoinSpriteSheet");
            shieldText = Content.Load<Texture2D>("lol");
            boostText = Content.Load<Texture2D>("Mint");
            bubbleText = Content.Load<Texture2D>("Bubble");
            bushText = Content.Load<Texture2D>("bush");
            cakeText = Content.Load<Texture2D>("cake");
            candyText = Content.Load<Texture2D>("candy");
            cabbageText = Content.Load<Texture2D>("cabbage");
            carrotText = Content.Load<Texture2D>("carrot");
            oneText = Content.Load<Texture2D>("one");
            twoText = Content.Load<Texture2D>("two");
            threeText = Content.Load<Texture2D>("three");
            fourText = Content.Load<Texture2D>("four");
            fiveText = Content.Load<Texture2D>("five");
            titleTexture = Content.Load<Texture2D>("Title");
            startBTexture = Content.Load<Texture2D>("StartButton");
            instructionsBTexture = Content.Load<Texture2D>("InstructionsButton");
            gbOriginalTexture = Content.Load<Texture2D>("GBManOriginal");
            gbTutuTexture = Content.Load<Texture2D>("GBManTutu");
            gbLeafTexture = Content.Load<Texture2D>("GBManLeaf");
            editText = Content.Load<Texture2D>("editRect");
            activeLevels = new List<Level>();

            lv1 = new Level(levelFiles[0], 3, coinText, oneText, twoText, threeText, fourText, fiveText, shieldText, boostText, bushText, cakeText, candyText, cabbageText, carrotText, 1000);
            lv2 = new Level(levelFiles[1], 3, coinText, oneText, twoText, threeText, fourText, fiveText, shieldText, boostText, bushText, cakeText, candyText, cabbageText, carrotText, lv1.GetLastLevel());
            lv3 = new Level(levelFiles[2], 3, coinText, oneText, twoText, threeText, fourText, fiveText, shieldText, boostText, bushText, cakeText, candyText, cabbageText, carrotText, lv1.GetLastLevel());
            lv4 = new Level(levelFiles[3], 3, coinText, oneText, twoText, threeText, fourText, fiveText, shieldText, boostText, bushText, cakeText, candyText, cabbageText, carrotText, lv1.GetLastLevel());
            lv5 = new Level(levelFiles[4], 3, coinText, oneText, twoText, threeText, fourText, fiveText, shieldText, boostText, bushText, cakeText, candyText, cabbageText, carrotText, lv1.GetLastLevel());

            activeLevels.Add(lv1);
            activeLevels.Add(lv2);
            activeLevels.Add(lv3);
            activeLevels.Add(lv4);
            activeLevels.Add(lv5);

            spriteFont = Content.Load<SpriteFont>("SpriteFont1");

            //nextLevelIndex = 0;

            ReadFileOfGingerbreadMan(@"Content/GingerbreadMan.txt");
        }

        private void ReadFileOfGingerbreadMan(String path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    int loc = 0;
                    Rectangle[] temp = new Rectangle[10];
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] parts = line.Split(' ');
                        temp[loc] = new Rectangle(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]), Convert.ToInt32(parts[3]));
                        loc++;
                    }
                    gbMan = new Gingerbread_Man(temp, gbOriginalTexture, Color.White);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read: ");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            KeyboardState kb = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            mouseRect.X = mouse.X;
            mouseRect.Y = mouse.Y;

            if (gameState == GameState.start)
            {
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)//Checks what button is pressed
                {
                    if (mouseRect.Intersects(startButtonRect))
                    {
                        gameState = GameState.play;
                    }
                    if (mouseRect.Intersects(instructionsButtonRect))
                    {
                        gameState = GameState.instructions;
                    }
                    if (mouseRect.Intersects(editButtonRect))
                    {
                        gameState = GameState.editCharacter;
                    }
                }
                bushActivated = false;
                boostActivated = false;
            }

            if (gameState == GameState.play)
            {
                for (int l = 0; l < activeLevels.Count; l++)
                {
                    Level lvl = activeLevels[l];
                    if (boostActivated)
                        lvl.setSpeed(4);
                    else if (bushActivated)
                        lvl.setSpeed(2);
                    else
                        lvl.setSpeed(3);
                    if (lvl != null)
                        lvl.Update();
                }

                if (boostActivated)
                    boostingTimer++;
                else
                    boostingTimer = 0;
                if (boostingTimer == 300)
                    boostActivated = false;

                if (bushActivated)
                    bushTimer++;
                else
                    bushTimer = 0;
                if (bushTimer == 300)
                    bushActivated = false;

                if (shieldActivated)
                    shieldTimer++;
                else
                    shieldTimer = 0;
                if (shieldTimer == 300)
                    shieldActivated = false;
                bubbleRect = new Rectangle(gbMan.GetRect().X - 20, gbMan.GetRect().Y - 15, 100, 100);

                score++;
                gbMan.Update(gameTime);

                for (int l = 0; l < activeLevels.Count; l++)
                {
                    Level lvl = activeLevels[l];
                    if (lvl.GetLatPlatformRect().Right < 0)
                    {
                        Level lastLevel = activeLevels[activeLevels.Count - 1];
                        int newX = lastLevel.GetEndX();


                        string nextFile = levelFiles[nextLevelIndex];
                        nextLevelIndex = (nextLevelIndex + 1) % levelFiles.Length;

                        activeLevels.RemoveAt(l);
                        activeLevels.Add(new Level(nextFile, 3, coinText, oneText, twoText, threeText, fourText, fiveText, shieldText, boostText, bushText, cakeText, candyText, cabbageText, carrotText, newX));
                        Console.WriteLine($"Added {nextFile} at {newX}");

                        break;
                    }
                }

                foreach (var lvl in activeLevels)
                    lvl.Update();


                if (isOnGround && kb.IsKeyDown(Keys.Up) && !oldKB.IsKeyDown(Keys.Space))
                {
                    isJumping = true;
                    isOnGround = false;
                    startY = gbMan.GetRect().Y;
                    velocity = -10f; // jump velocity
                }

                if (kb.IsKeyDown(Keys.Down) && !oldKB.IsKeyDown(Keys.Down) && duck == false)// Down or S
                {
                    duck = true;
                    gbMan.SetY(gbMan.GetRect().Y + 50);
                    gbMan.SetHeight(50);
                    gbMan.SetDuckingTexture();
                }

                if (duck == true && !kb.IsKeyDown(Keys.Down))
                {
                    gbMan.SetY(gbMan.GetRect().Y + 80);
                    gbMan.SetHeight(80);
                    duck = false;
                }


                if (!isOnGround)
                {
                    velocity += gravity;
                    gbMan.ChangeY((int)velocity);

                    // Max jump height check
                    if (isJumping && (startY - gbMan.GetRect().Y >= maxJumpHeight || velocity > 0))
                    {
                        isJumping = false;
                    }
                }


                isOnGround = false;

                for (int l = 0; l < activeLevels.Count; l++)
                {
                    Level level = activeLevels[l];

                    for (int i = 0; i < level.GetPlatforms().GetLength(0); i++)
                    {
                        for (int j = 0; j < level.GetPlatforms().GetLength(1); j++)
                        {
                            var platform = level.GetPlatforms()[i, j];
                            if (platform == null) continue;


                            Rectangle platRect = platform.GetRectangle();

                            Rectangle playerRect = gbMan.GetRect();


                            int hitboxPaddingX = 15;
                            int hitboxPaddingY = 20;
                            playerRect = new Rectangle(
                                     gbMan.GetRect().Left + hitboxPaddingX,
                                     gbMan.GetRect().Top,
                                     gbMan.GetRect().Width - 2 * hitboxPaddingX,
                                     gbMan.GetRect().Height - 2);



                            bool isLanding =
                                playerRect.Bottom >= platRect.Top &&
                                playerRect.Bottom - velocity <= platRect.Top &&
                                playerRect.Right > platRect.Left &&
                                playerRect.Left < platRect.Right - 15 &&
                                velocity >= 0;

                            if (isLanding)
                            {

                                gbMan.SetY(platRect.Top - playerRect.Height);
                                velocity = 0;
                                isOnGround = true;
                                isJumping = false;
                                break;
                            }


                            bool headHitsBlock =
                                playerRect.Top <= platRect.Bottom - 5

                                && playerRect.Intersects(platRect);


                            int playerCenterX = (playerRect.Left - 10) + (50 / 2);
                            bool centeredUnderBlock = playerCenterX > platRect.Left && playerCenterX < platRect.Right;

                            if (headHitsBlock && centeredUnderBlock && !isLanding)
                            {
                                if (shieldActivated)
                                {
                                    shieldActivated = false;
                                }
                                else
                                {
                                    // Player's head hit the middle of the block above!
                                    gameState = GameState.end;
                                    // Or whatever your "game over" logic is
                                    return;     // Exit update early, or handle as needed
                                }
                            }
                        }
                        if (isOnGround) break;
                    }
                }


                // --- GROUND (FLOOR) COLLISION ---
                int groundY = 450 - gbMan.GetRect().Height;
                if (gbMan.GetRect().Y >= groundY)
                {
                    gbMan.SetY(groundY);
                    velocity = 0;
                    isOnGround = true;
                    isJumping = false;
                }

                //if landing
                for (int l = 0; l < activeLevels.Count; l++)
                {
                    Level level = activeLevels[l];


                    for (int i = 0; i < level.GetPlatforms().GetLength(0); i++)
                    {
                        for (int j = 0; j < level.GetPlatforms().GetLength(1); j++)
                        {
                            var platform = level.GetPlatforms()[i, j];
                            if (!isOnGround && platform == null && !isJumping)
                            {
                                gbMan.SetLandingTexture();
                            }
                            if (!isOnGround && platform == null && isJumping)
                            {
                                gbMan.SetJumpingTexture();
                            }

                            //COIN
                            if (level.GetCoins()[i, j] != null &&
                               !level.GetCoins()[i, j].isCollected &&
                                level.GetCoins()[i, j].GetRectangle().Intersects(gbMan.GetRect()))
                            {
                                coinScore = level.GetCoins()[i, j].Disappear(coinScore);
                                level.GetCoins()[i, j].isCollected = true;
                            }
                        }

                    }
                }

                for(int l = 0; l < activeLevels.Count; l++)
                {
                    Level level = activeLevels[l];

                    for (int i = 0; i < level.GetCoins().GetLength(0); i++)
                    {
                        for (int j = 0; j < level.GetCoins().GetLength(1); j++)
                        {

                            if (level.GetCoins()[i, j] != null && gbMan.GetRect().Intersects(level.GetCoins()[i, j].GetRectangle()))
                            {
                                level.RemoveCoin(i, j);
                                score++;
                            }
                            if (level.GetShields()[i, j] != null && gbMan.GetRect().Intersects(level.GetShields()[i, j].GetRectangle()))
                            {
                                level.RemoveShield(i, j);
                                shieldActivated = true;
                            }
                            if (level.GetBoosts()[i, j] != null && gbMan.GetRect().Intersects(level.GetBoosts()[i, j].GetRectangle()))
                            {
                                level.RemoveBoost(i, j);
                                boostActivated = true;
                            }
                            if (level.GetBushes()[i, j] != null && gbMan.GetRect().Intersects(level.GetBushes()[i, j].GetRectangle()))
                            {
                                level.RemoveBush(i, j);
                                bushActivated = true;
                            }
                            if (level.GetCakes()[i, j] != null && gbMan.GetRect().Intersects(level.GetCakes()[i, j].GetRectangle()))
                            {
                                level.RemoveCake(i, j);
                                candy += 10;
                            }

                            if (level.GetCandies()[i, j] != null && gbMan.GetRect().Intersects(level.GetCandies()[i, j].GetRectangle()))
                            {
                                level.RemoveCandy(i, j);
                                candy++;
                            }

                            if (level.GetCabbages()[i, j] != null && gbMan.GetRect().Intersects(level.GetCabbages()[i, j].GetRectangle()))
                            {
                                level.RemoveCabbage(i, j);
                                if (score >= 3)
                                    score -= 3;
                            }

                            if (level.GetCarrots()[i, j] != null && gbMan.GetRect().Intersects(level.GetCarrots()[i, j].GetRectangle()))
                            {
                                level.RemoveCarrot(i, j);
                                if (score >= 1)
                                    score -= 1;
                            }
                        }
                    }
                }
            }

            if(gameState == GameState.editCharacter)
            {
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)//Checks what button is pressed
                {
                    if (mouseRect.Intersects(colorRects[0]))
                    {
                        gbMan.changeTexture(gbOriginalTexture);
                    }
                    if (mouseRect.Intersects(colorRects[1]))
                    {
                        gbMan.changeTexture(gbTutuTexture);
                    }
                    if (mouseRect.Intersects(colorRects[2]))
                    {
                        gbMan.changeTexture(gbLeafTexture);
                    }
                    if (mouseRect.Intersects(backButton))
                    {
                        gameState = GameState.start;
                    }
                }
            }

            if (gameState == GameState.instructions)
            {
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)//Checks what button is pressed
                {
                    if (mouseRect.Intersects(backButton))
                    {
                        gameState = GameState.start;
                    }
                }
            }

            if(gameState == GameState.end)
            {
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)//Checks what button is pressed
                {
                    hs.SetHighScore(score);
                    highScore = hs.GetHighscore();
                    leaderBoard.EnterScore("" + score);
                    lscore = Convert.ToInt32(leaderBoard.GetHighestScore());
                    if (mouseRect.Intersects(instructionsButtonRect))
                    {
                        RestartGame();
                        for(int i = activeLevels.Count -1; i > 0; i--)
                        {
                            activeLevels.RemoveAt(i);
                        }
                    }
                }
            }


            oldMouse = mouse;
            oldKB = kb;
            base.Update(gameTime);
        }
        public void ApplyPhysics()
        {
            int groundY = 450 - gbMan.GetRect().Height;
            if (gbMan.GetRect().Y <= groundY)
            {
                gbMan.ChangeY(5);
                velocity = 0;
                isOnGround = true;
            }
        }

        private void RestartGame()
        {
            // Reset score
            score = 0;

            // Reset physics
            velocity = -10f;
            isJumping = false;
            isOnGround = true;

            // Reset Gingerbread Man position (put at original location)
            gbMan.SetY(350); // Or whatever your original Y is
            gbMan.SetX(100); // Original X
            gbMan.SetHeight(80); // Reset height if ducked

            // Reset levels
            activeLevels.Clear();
            lv1 = new Level(levelFiles[0], 3, coinText, oneText, twoText, threeText, fourText, fiveText, shieldText, boostText, bushText, cakeText, candyText, cabbageText, carrotText, 1000);
            lv2 = new Level(levelFiles[1], 3, coinText, oneText, twoText, threeText, fourText, fiveText, shieldText, boostText, bushText, cakeText, candyText, cabbageText, carrotText, lv1.GetLastLevel());
            lv3 = new Level(levelFiles[2], 3, coinText, oneText, twoText, threeText, fourText, fiveText, shieldText, boostText, bushText, cakeText, candyText, cabbageText, carrotText, lv1.GetLastLevel());
            lv4 = new Level(levelFiles[3], 3, coinText, oneText, twoText, threeText, fourText, fiveText, shieldText, boostText, bushText, cakeText, candyText, cabbageText, carrotText, lv1.GetLastLevel());
            lv5 = new Level(levelFiles[4], 3, coinText, oneText, twoText, threeText, fourText, fiveText, shieldText, boostText, bushText, cakeText, candyText, cabbageText, carrotText, lv1.GetLastLevel());
            activeLevels.Add(lv1);
            activeLevels.Add(lv2);
            activeLevels.Add(lv3);
            activeLevels.Add(lv4);
            activeLevels.Add(lv5);
            nextLevelIndex = 0;

            // Reset game state
            gameState = GameState.start;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if(gameState == GameState.start)
            {
                spriteBatch.Draw(startBGTexture, startBGRect, Color.White);
                spriteBatch.Draw(startBTexture, startButtonRect, Color.White);
                spriteBatch.Draw(instructionsBTexture, instructionsButtonRect, Color.White);
                spriteBatch.Draw(editText, editButtonRect, Color.White);
                spriteBatch.Draw(titleTexture, titleRect, Color.White);
            }
            else if (gameState == GameState.instructions)
            {
                spriteBatch.Draw(startBGTexture, startBGRect, Color.White);
                spriteBatch.Draw(rectangleTexture, instructionsRect, Color.White);
                spriteBatch.DrawString(startFont, "Avoid the obstacles\nPress Up/W to Jump\nPress Down/S to Duck", new Vector2(100, 100), Color.Black);
                spriteBatch.Draw(buttonTexture, backButton, Color.White);
                spriteBatch.DrawString(startFont, "Back", new Vector2(30, GraphicsDevice.Viewport.Height - 52), Color.White);
            }
            else if(gameState == GameState.play)
            {
                spriteBatch.Draw(startBGTexture, backgroundRect, Color.White*0.6f);
                foreach (var lvl in activeLevels)
                    lvl.Draw(spriteBatch);
                spriteBatch.Draw(oneText, new Rectangle(-300, 450, (4000 / 2), (200)), new Rectangle(10, 43, 160, 77), Color.White);
                gbMan.Draw(spriteBatch);
                if (shieldActivated == true)
                    spriteBatch.Draw(bubbleText, bubbleRect, Color.White * bubbleOpacity);
            }
            else if(gameState == GameState.editCharacter)
            {
                spriteBatch.Draw(startBGTexture, startBGRect, Color.White);
                spriteBatch.Draw(rectangleTexture, new Rectangle(GraphicsDevice.Viewport.Width / 2 - 200, 50, 400, 400), Color.White);
                spriteBatch.Draw(gbMan.GetTexture(), new Rectangle(GraphicsDevice.Viewport.Width/2 - gbMan.GetRect().Width/2, GraphicsDevice.Viewport.Height / 2 - 200, 120,160), gbMan.GetSourceRect(2), gbMan.GetColor());
                spriteBatch.Draw(gbOriginalTexture, colorRects[0], gbMan.GetSourceRect(2), Color.White);
                spriteBatch.Draw(gbTutuTexture, colorRects[1], gbMan.GetSourceRect(2), Color.White);
                spriteBatch.Draw(gbLeafTexture, colorRects[2], gbMan.GetSourceRect(2), Color.White);
                spriteBatch.Draw(buttonTexture, backButton, Color.White);
                spriteBatch.DrawString(startFont, "Back", new Vector2(30, GraphicsDevice.Viewport.Height - 52), Color.White);
            }
            else if (gameState == GameState.end)
            {
                spriteBatch.Draw(startBGTexture, startBGRect, Color.Black);
                spriteBatch.Draw(buttonTexture, instructionsButtonRect, Color.White);
                spriteBatch.DrawString(startFont, "Score: " + score, new Vector2(100, 100), Color.White);
                spriteBatch.DrawString(startFont, "Restart", new Vector2(970, 233), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
