using System;
using System.IO;
using System.Collections.Generic;

class Scene 
{
    static Vector2 Resolution = Game.Resolution;

    static Boolean win = false; // tells whether player passed the level
    static Boolean dead = true;
    static Boolean menuOpen = false; // tells us if menu is open or not

    //textures for screens
    static Texture start = Engine.LoadTexture("start.png");
    static Texture menu = Engine.LoadTexture("openMenu.png");
    static Texture levelSelect = Engine.LoadTexture("levelSelect.png");
    static Texture gameover = Engine.LoadTexture("game over.png");
    static Texture done = Engine.LoadTexture("level done.png");
    static Texture instruction1 = Engine.LoadTexture("instruction 1.png");
    static Texture instruction2 = Engine.LoadTexture("instruction 2.png");
    static Texture credits = Engine.LoadTexture("credits.png");

    //Texture for backstory panels

    static Texture pg1 = Engine.LoadTexture("kiki pg1.png");
    static Texture pg2 = Engine.LoadTexture("kiki pg2.png");
    static Texture pg3 = Engine.LoadTexture("kiki pg3.png");
    static Texture pg4 = Engine.LoadTexture("kiki pg4.png");
    static Texture pg5 = Engine.LoadTexture("kiki pg5.png");

    static Texture healthSheet = Engine.LoadTexture("Health.png");

    //textures for basic buttons
    static Texture back = Engine.LoadTexture("back_next.png");
    static TextureMirror next = TextureMirror.Horizontal;
    static Texture menuButton = Engine.LoadTexture("menu.png");
    static Texture exitButton = Engine.LoadTexture("exit.png");

    //textures for starting options
    static Texture startButton = Engine.LoadTexture("startButton.png");
    static Texture storyButton = Engine.LoadTexture("storyButton.png");

    //textures for menu options
    static Texture instuctionsButton = Engine.LoadTexture("instructionsButton.png");
    static Texture storyLongButton = Engine.LoadTexture("storyLongButton.png");
    static Texture quitButton = Engine.LoadTexture("quitButton.png");

    //level buttons
    static Texture lvl1 = Engine.LoadTexture("level1.png");
    static Texture lvl2 = Engine.LoadTexture("level2.png");
    static Boolean [] levels = { true, false };

    // door
    public static Texture door = Engine.LoadTexture("door.png");

    static readonly Texture backgroundGrey = Engine.LoadTexture("Kirby red level background - Grayscale.png");
    static readonly Texture backgroundColor = Engine.LoadTexture("Kirby red level background.png");
    static Font font = Engine.LoadFont("font.ttf", 20);
    static int numBlocksLevel1 = 122;
    static int numBlocksLevel2 = 202;

    public static Block[] blocks;
    public static Player player = new Player(blocks);
    static EnemyManager enemyManager;
    static Level level1 = new Level(backgroundColor, backgroundGrey, numBlocksLevel1, "Assets/trial level coords.txt", "level 1 enemies.txt");
    static Level level2 = new Level(backgroundColor, backgroundGrey, numBlocksLevel2, "Assets/env coords.txt", "level 1 enemies.txt");

    static int screen = 0;
    static int numLevel = 1;

    public Scene()
    {
        Setup();
    }

    public void Setup()
    {
        reload();
    }

    public static void Update()
    {
        
        //start screen
        if (screen == 0)
        {
            Engine.DrawTexture(start, Vector2.Zero);

            level1.DoorOpen = false;
            player.points = 0;

            button(startButton, 130, 500, 210, 60,1);
            button(storyButton, 370, 500, 210, 60, 8);
        }


        //level select
        else if (screen == 1)
        {
            Engine.DrawTexture(levelSelect, Vector2.Zero);
            button(instuctionsButton, 740, 300, 300, 60, 2);
            button(storyLongButton, 740, 380, 300, 60, 8);
            button(quitButton, 740, 460, 300, 60, 0);

            levelButtons(300,350, 1, lvl1);
            levelButtons(430, 350, 2, lvl2);

        }
        //1st instructions screen
        else if (screen == 2)
        {

            Engine.DrawTexture(instruction1, Vector2.Zero);
            arrowButtons();
            menuButtons();
            button(exitButton, 1205, 20, 40, 40, 1);
        }

        //2nd instructions screen
        else if (screen == 3)
        {
            Engine.DrawTexture(instruction2, Vector2.Zero);
            button(back, 30, 355, 40, 40, 2);
            menuButtons();
            button(exitButton, 1205, 20, 40, 40, 1);
        }


        //levels
        else if (screen == 3)
        {
            if (numLevel == 1)
            {
                UpdateLevel(level1);
            } 
            else if (numLevel == 2)
            {
                UpdateLevel(level2);
            }

        }

        //game over screen
        else if (screen == 5)
        {
            if (win)
            {
                screen++;
            }
            else
            {
                menuButtons();
                Engine.DrawTexture(gameover, Vector2.Zero);
                Engine.DrawString("High Score: " + player.highScore(), new Vector2(550, 100), Color.White, font);
                if ((Engine.GetMouseButtonDown(MouseButton.Left)) && (!menuOpen))
                {
                    screen = 6;
                }
            }

        }

        //level complete screen (end screen)
        else if (screen == 6)
        {
            menuButtons();
            Engine.DrawTexture(done, Vector2.Zero);
            Engine.DrawString("High Score: " + player.highScore(), new Vector2(550, 300), Color.White, font);
            if (Engine.GetMouseButtonDown(MouseButton.Left))
            {
                screen++;
            }
        }

        //end screen
        else if (screen == 7)
        {
            menuButtons();
            Engine.DrawTexture(credits, Vector2.Zero);
            if (Engine.GetMouseButtonDown(MouseButton.Left))
            {
                screen = 0;
            }
        }
        
        //kiki backstory
        else if (screen == 8)
        {
            Engine.DrawTexture(pg1, Vector2.Zero);
            arrowButtons();
            menuButtons();
            button(exitButton, 1205, 20, 40, 40, 1);
        }

        else if (screen == 9)
        {
            Engine.DrawTexture(pg2, Vector2.Zero);
            arrowButtons();
            menuButtons();
            button(exitButton, 1205, 20, 40, 40, 1);
        }
        else if (screen == 10)
        {
            Engine.DrawTexture(pg3, Vector2.Zero);
            arrowButtons();
            menuButtons();
            button(exitButton, 1205, 20, 40, 40, 1);
        }
        else if (screen == 11)
        {
            Engine.DrawTexture(pg4, Vector2.Zero);
            arrowButtons();
            menuButtons();
            button(exitButton, 1205, 20, 40, 40, 1);
        }
        else if (screen == 12)
        {
            Engine.DrawTexture(pg5, Vector2.Zero);
            button(back, 30, 355, 40, 40, 2);
            menuButtons();
            button(exitButton, 1205, 20, 40, 40, 1);
        }


        if (menuOpen)
        {

            openMenu();

        }
    }

    public static void button(Texture image, int x, int y, int width, int height, int i)
    {
        Vector2 mouse = Engine.MousePosition;
        Bounds2 bound;

        if ((mouse.X >= x) && (mouse.X <= x+width) &&
            (mouse.Y >= y) && (mouse.Y <= y+height))
        {
            bound = new Bounds2(width, 0, width, height);
            if (Engine.GetMouseButtonDown(MouseButton.Left) || Engine.GetMouseButtonDown(MouseButton.Right))
            {
                screen = i;
                menuOpen = false;
            }
        }
        else
        {
            bound = new Bounds2(0, 0, width, height);
        }

        Engine.DrawTexture(image, new Vector2(x,y), source: bound);

    }

    //back/next buttons for switching screens
    public static void arrowButtons()
    {
        Vector2 mouse = Engine.MousePosition;

        Bounds2 bBound;
        Bounds2 nBound;

        if ((mouse.X >= 30) && (mouse.X <= 70) &&
            (mouse.Y >= 355) && (mouse.Y <= 395))
        {
            bBound = new Bounds2(40, 0, 40, 40);
            if (Engine.GetMouseButtonDown(MouseButton.Left) || Engine.GetMouseButtonDown(MouseButton.Right))
            {
                screen--;
                menuOpen = false;
            }
        }
        else
        {
            bBound = new Bounds2(0, 0, 40, 40);
        }

        if ((mouse.X >= 1205) && (mouse.X <= 1245) &&
            (mouse.Y >= 355) && (mouse.Y <= 395))
        {
            nBound = new Bounds2(40, 0, 40, 40);
            if (Engine.GetMouseButtonDown(MouseButton.Left) || Engine.GetMouseButtonDown(MouseButton.Right))
            {
                screen++;
                menuOpen = false;
            }
        }
        else
        {
            nBound = new Bounds2(0, 0, 40, 40);
        }

        if (Engine.GetKeyDown(Key.Space))
        {
            screen++;
        }

        Engine.DrawTexture(back, new Vector2(30, 355), source: bBound);
        Engine.DrawTexture(back, new Vector2(1205, 355), source: nBound, mirror: next);

    }



    //menu button functions
    public static void menuButtons()
    {
        Vector2 mouse = Engine.MousePosition;
        Bounds2 mBound;

        if ((mouse.X >= 30) && (mouse.X <= 70) &&
            (mouse.Y >= 20) && (mouse.Y <= 60))
        {
            mBound = new Bounds2(40, 0, 40, 40);
            if (Engine.GetMouseButtonDown(MouseButton.Left) || Engine.GetMouseButtonDown(MouseButton.Right))
            {
                if (menuOpen)
                {
                    menuOpen = false;
                }
                else
                {
                    menuOpen = true;
                }
            }
        }
        else
        {
            mBound = new Bounds2(0, 0, 40, 40);
        }

        Engine.DrawTexture(menuButton, new Vector2(30, 20), source: mBound);

    }

    //opens the menu
    public static void openMenu()
    {
        Engine.DrawTexture(menu, new Vector2 (30,80));

        if (Engine.GetMouseButtonDown(MouseButton.Left) || Engine.GetMouseButtonDown(MouseButton.Right))
        {
            Vector2 mouse = Engine.MousePosition;

            //level select button
            if ((mouse.X >= 30) && (mouse.X <= 140) &&
            (mouse.Y >= 126) && (mouse.Y <= 165))
            {
                menuOpen = false;
                screen = 1;
            }

            //instructions button
            else if ((mouse.X >= 30) && (mouse.X <= 140) &&
            (mouse.Y >= 166) && (mouse.Y <= 205))
            {
                menuOpen = false;
                screen = 2;
            }

            //story button
            else if ((mouse.X >= 30) && (mouse.X <= 140) &&
            (mouse.Y >= 206) && (mouse.Y <= 246))
            {
                menuOpen = false;
                screen = 8;
            }

            //quit button (goes to start screen)
            else if ((mouse.X >= 30) && (mouse.X <= 140) &&
            (mouse.Y >= 246) && (mouse.Y <= 296))
            {
                menuOpen = false;
                screen = 0;
            }
        }

    }
    public static void levelButtons(int x, int y, int lvl, Texture image)
    {
        Vector2 mouse = Engine.MousePosition;
        Bounds2 bound;
        
            if (levels[lvl-1])
            {

                if ((mouse.X >= x) && (mouse.X <= x +100) &&
                    (mouse.Y >= y) && (mouse.Y <= y + 100))
                {
                    bound = new Bounds2(100, 0, 100, 100);
                    if (Engine.GetMouseButtonDown(MouseButton.Left) || Engine.GetMouseButtonDown(MouseButton.Right))
                    {
                        screen = 3 + lvl;
                    }
                }
                else
                {
                    bound = new Bounds2(0, 0, 100, 100);
                }
            }
            else
            {
                bound = new Bounds2(200, 0, 100, 100);
            }

            Engine.DrawTexture(image, new Vector2(x, y), source: bound);

     }

     

    public static void reload()
    {
        Engine.reload();
        StreamReader sr = new StreamReader(level1.envCoords);
        blocks = new Block[numBlocksLevel1];
        for (int i = 0; i < blocks.Length; i++)
        {
            string line = sr.ReadLine();
            string[] nums = line.Split(' ');

            blocks[i] = new Block(Int32.Parse(nums[0]), Int32.Parse(nums[1]), Int32.Parse(nums[2]));
        }
        player.updateBlocks(blocks);
        sr.Close();
    }

    private static void UpdateLevel(Level level)
    {
        if (dead)
        {
            dead = false;
            level.scroll = 0;
            player = new Player(blocks);
            player.kPos.X = 260;
            player.kPos.Y = Resolution.Y / 2;
            enemyManager = new EnemyManager(player);
            enemyManager.initializeEnemies(level.enemyFile);
        }

        if (Engine.GetKeyDown(Key.R))
        {
            reload();
        }

        if (player.points > 100)
        {
            level.color = 1;
        }

        level.Update();
        player.Update(level.scroll);
        enemyManager.Update(level.scroll);

        menuButtons();
        Engine.DrawString("Current Score: " + player.points.ToString(),
                            new Vector2(1000, 50), Color.White, font);
        Bounds2 hFrameBounds = new Bounds2(((int)(player.health / 100)) * 110, 0, 110, 20);
        Engine.DrawTexture(healthSheet, new Vector2(1000, 80), source: hFrameBounds, size: new Vector2(220, 40));

        if (player.getKPosition().Y >= 1000)
        {
            dead = true;
        }

        if (dead == true)
        {
            win = false;
            screen++;
        }

        if ((player.kPos.X >= 8300 + level.scroll) &&
            (player.kPos.X <= 8375 + level.scroll) &&
            (player.kPos.Y >= 575) &&
            (player.kPos.Y <= 675) &&
            level.DoorOpen)
        {
            win = true;
            dead = true;
            screen++;
            numLevel++;
        }

    }
}
