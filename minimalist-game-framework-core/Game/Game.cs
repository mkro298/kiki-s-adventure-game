using System;
using System.IO;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(1275, 750);
    readonly Font font = Engine.LoadFont("font.ttf", 20);
    static int numBlocks = 202;
    Block[] blocks;
    
    int scroll = 0;

    public Game()
    {
        reload();
    }

    public void Update()
    {
        Engine.DrawRectSolid(new Bounds2(Vector2.Zero, new Vector2(1275, 750)), Color.White);

        if (Engine.GetKeyDown(Key.R))
        {
            reload();
        }

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
