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
            Engine.DrawRectSolid(new Bounds2(Vector2.Zero, new Vector2(100, 100)), Color.Red);
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

        Engine.DrawRectSolid(new Bounds2(new Vector2(300, 575), new Vector2(100, 100)), Color.Red);
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

class Block
{
    readonly Font font = Engine.LoadFont("font.ttf", 20);
    int leftbound;
    int upperbound;
    int rightbound;
    int tileType;
    Vector2 coordinates;
    int blockSize = 75;

    public Block()
    {
        leftbound = 0;
        upperbound = 0;
        tileType = 0;
        coordinates = Vector2.Zero;
    }
    public Block(double left, double upper)
    {
        leftbound = (int)left;
        upperbound = (int)upper;
        tileType = 0;
        coordinates = new Vector2((int)left / blockSize, (int)upper / blockSize);
    }
    public Block(int x, int y)
    {
        leftbound = x * blockSize;
        upperbound = y * blockSize;
        tileType = 0;
        coordinates = new Vector2(x, y);
    }
    public Block(int x, int y, int tileType)
    {
        leftbound = x * blockSize;
        upperbound = y * blockSize;
        this.tileType = tileType;
        coordinates = new Vector2(x, y);
    }

    public int getLeftbound()
    {
        return leftbound;
    }
    public int getUpperbound()
    {
        return upperbound;
    }
    public int getRightbound()
    {
        return leftbound + blockSize;
    }

    public void draw()
    {
        Color color = Color.Black;
        if (tileType == 0)
        {
            color = Color.Aqua;
        }
        if (tileType == 1)
        {
            color = Color.Coral;
        }
        Engine.DrawRectSolid(new Bounds2(new Vector2(leftbound, upperbound), new Vector2(blockSize, blockSize)), color);
        Engine.DrawString(coordinates.ToString() + tileType.ToString(), new Vector2(leftbound, upperbound), Color.Black, font);
    }
    public void draw(int scroll)
    {
        Color color = Color.Black;
        if (tileType == 0)
        {
            color = Color.Aqua;
        }
        if (tileType == 1)
        {
            color = Color.Coral;
        }
        Engine.DrawRectSolid(new Bounds2(new Vector2(leftbound + scroll, upperbound), new Vector2(blockSize, blockSize)), color);
        Engine.DrawString(coordinates.ToString() + tileType.ToString(), new Vector2(leftbound + scroll, upperbound), Color.Black, font);
    }
}
