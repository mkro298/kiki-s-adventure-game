using System;
using System.IO;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(1275, 750);

    public static int speed = 5;
    int scroll = 0;

    int index = 0; // keeps track of current screen
    Boolean win = false; // tells whether player passed the level
    Boolean menuOpen = false; // tells us if menu is open or not

    Stack<String> screens = new Stack<String>();

    //textures for screens
    Texture start = Engine.LoadTexture("start.png");
    Texture menu = Engine.LoadTexture("menu screen.png");
    Texture gameover = Engine.LoadTexture("game over.png");
    Texture done = Engine.LoadTexture("level done.png");

    //textures for basic buttons
    Texture back = Engine.LoadTexture("back_next.png");
    TextureMirror next = TextureMirror.Horizontal;
    Texture menuButton = Engine.LoadTexture("menu.png");

    //textures for menu options
    Texture resume = Engine.LoadTexture("Resume Button.png");
    Texture infoButton = Engine.LoadTexture("Instructions Button.png");
    Texture quit = Engine.LoadTexture("Quit Button.png");

    readonly Texture background = Engine.LoadTexture("Kirby red level background.png");
    readonly Font font = Engine.LoadFont("font.ttf", 20);
    static int numBlocks = 202;

    static Block[] blocks;
    Player player = new Player(blocks);
    Enemy enemy;


    public Game()
    {
        Engine.DrawTexture(background, Vector2.Zero);
        reload();
        enemy = new Enemy(2, player);
    }

    public void Update()
    {

        //start screen
        if (index == 0)
        {
            Engine.DrawTexture(start, Vector2.Zero);
            if ((Engine.GetMouseButtonDown(MouseButton.Left)) && (!menuOpen))
            {
                index++;
            }

            //1st instructions screen
        }
        else if (index == 1)
        {
            arrowButtons();
            menuButtons();

            //2nd instructions screen
        }
        else if (index == 2)
        {
            arrowButtons();
            menuButtons();

            //level 1
        }
        else if (index == 3)
        {

            Engine.DrawTexture(background, Vector2.Zero);
            if (Engine.GetKeyDown(Key.R))
            {
                reload();
            }
            enemy.runEnemyCode();
            player.Update(scroll);

            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i].draw(scroll);
            }

            int speed = 5;

            //adjust scroll
            if (Engine.GetKeyHeld(Key.Right) && player.getKPosition().X > 940 && scroll > -7425 && player.getMoveRight())
            {
                scroll -= speed;
            }
            if (Engine.GetKeyHeld(Key.Left) && player.getKPosition().X < 255 && scroll < 0 && player.getMoveLeft())
            {
                scroll -= speed;
            }
            // draw the blocks
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i].draw(scroll);
            }
            /*
            if (enemy.isAlive)
            {
                enemy.runEnemyCode();
            }
            */
            menuButtons();
        }

        //game over screen
        else if (index == 4)
        {
            if (win)
            {
                index++;
            }
            else
            {
                menuButtons();
                Engine.DrawTexture(gameover, Vector2.Zero);
                if ((Engine.GetMouseButtonDown(MouseButton.Left)) && (!menuOpen))
                {
                    index = 0;
                }
            }

        }

        //level complete screen (end screen)
        else if (index == 5)
        {
            menuButtons();
            Engine.DrawTexture(done, Vector2.Zero);
            if (Engine.GetMouseButtonDown(MouseButton.Left))
            {
                index = 0;
            }
        }
        if (menuOpen)
        {

            openMenu();

        }

    }

    //back/next buttons for switching screens
    public void arrowButtons()
    {
        Vector2 mouse = Engine.MousePosition;

        Bounds2 bBound;
        Bounds2 nBound;

        if ((mouse.X >= 30) && (mouse.X <= 70) &&
            (mouse.Y >= 20) && (mouse.Y <= 60))
        {
            bBound = new Bounds2(40, 0, 40, 40);
            if (Engine.GetMouseButtonDown(MouseButton.Left))
            {
                index--;
            }
        }
        else
        {
            bBound = new Bounds2(0, 0, 40, 40);
        }

        if ((mouse.X >= 1205) && (mouse.X <= 1245) &&
            (mouse.Y >= 20) && (mouse.Y <= 60))
        {
            nBound = new Bounds2(40, 0, 40, 40);
            if (Engine.GetMouseButtonDown(MouseButton.Left))
            {
                index++;
            }
        }
        else
        {
            nBound = new Bounds2(0, 0, 40, 40);
        }

        Engine.DrawTexture(back, new Vector2(30, 20), source: bBound);
        Engine.DrawTexture(back, new Vector2(1205, 20), source: nBound, mirror: next);

    }

    //menu button functions
    public void menuButtons()
    {
        Vector2 mouse = Engine.MousePosition;
        Bounds2 mBound;

        if ((mouse.X >= 80) && (mouse.X <= 120) &&
            (mouse.Y >= 20) && (mouse.Y <= 60))
        {
            mBound = new Bounds2(40, 0, 40, 40);
            if (Engine.GetMouseButtonDown(MouseButton.Left))
            {
                menuOpen = true;
            }
        }
        else
        {
            mBound = new Bounds2(0, 0, 40, 40);
        }

        Engine.DrawTexture(menuButton, new Vector2(80, 20), source: mBound);

    }

    //opens the menu
    public void openMenu()
    {
        Engine.DrawTexture(menu, Vector2.Zero);

        Engine.DrawTexture(resume, new Vector2(0, 300));
        Engine.DrawTexture(infoButton, new Vector2(0, 400));
        Engine.DrawTexture(quit, new Vector2(0, 500));

        if (Engine.GetMouseButtonDown(MouseButton.Left))
        {
            Vector2 mouse = Engine.MousePosition;

            //resume button
            if ((mouse.X >= 600) && (mouse.X <= 690) &&
            (mouse.Y >= 300) && (mouse.Y <= 330))
            {
                menuOpen = false;
            }

            //instructions button
            else if ((mouse.X >= 570) && (mouse.X <= 710) &&
            (mouse.Y >= 400) && (mouse.Y <= 430))
            {
                menuOpen = false;
                index = 1;
            }

            //quit button (goes to start screen)
            else if ((mouse.X >= 620) && (mouse.X <= 670) &&
            (mouse.Y >= 500) && (mouse.Y <= 530))
            {
                menuOpen = false;
                index = 0;
            }



        }
    }

    private void reload()
    {
        Engine.reload();
        StreamReader sr = new StreamReader("assets/env coords.txt");
        blocks = new Block[numBlocks];
        for (int i = 0; i < blocks.Length; i++)
        {
            string line = sr.ReadLine();
            string[] nums = line.Split(' ');

            blocks[i] = new Block(Int32.Parse(nums[0]), Int32.Parse(nums[1]), Int32.Parse(nums[2]));
        }
        player.updateBlocks(blocks);
        sr.Close();
    }
}
