using System;
using System.IO;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Minimalist Game Framework"; 
    public static readonly Vector2 Resolution = new Vector2(1275, 750);
    readonly Texture background = Engine.LoadTexture("Kirby red level background.png");
    readonly Font font = Engine.LoadFont("font.ttf", 20);
    readonly Texture background = Engine.LoadTexture("Kirby red level background.png");
    static int numBlocks = 202;
    Block[] blocks;
    int scroll = 0;
    Player b = new Player();
    
    public Game()
    {
        Engine.DrawTexture(background, Vector2.Zero);
        reload();
        Enemy c = new Enemy(0, b);
    }
    public void Update()
    {
        Engine.DrawTexture(background, Vector2.Zero);
        if (Engine.GetKeyDown(Key.R))
        {
            reload();
        }
        
        b.Update(); 
        
        for (int i = 0; i < blocks.Length; i++)
        { 
            blocks[i].draw(scroll);
        }

        int speed = 5;

        if (Engine.GetKeyHeld(Key.Left) && scroll <= -1)
        {
            scroll += speed;
        }

        if (Engine.GetKeyHeld(Key.Right) && scroll >= -7425)
        {
            scroll -= speed;
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
        sr.Close();
    }

}
