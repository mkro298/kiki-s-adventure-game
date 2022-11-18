using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(1275, 750);
    Block[] blocks = new Block[10];

    public Game()
    {
        //Engine.DrawLine(Vector2.Zero, new Vector2(1275, 750), Color.White);
    }

    public void Update()
    {
        Engine.DrawRectSolid(new Bounds2(Vector2.Zero, new Vector2(1275, 750)), Color.White);

        blocks[0] = new Block(0, 9);
        blocks[1] = new Block(1, 8);
        blocks[2] = new Block(2, 7);
        blocks[3] = new Block(3, 6);
        blocks[4] = new Block(4, 5);
        blocks[5] = new Block(5, 4);
        blocks[6] = new Block(6, 3);
        blocks[7] = new Block(7, 2);
        blocks[8] = new Block(8, 1);
        blocks[9] = new Block(9, 0);

        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].draw();
        }
        /*
        int count = 0;
        for (int x = 0; x < blocks.Length; x++)
        {
            for (int y = 0; y < blocks[x].Length; y++)
            {
                if (env[x][y] == 1)
                {

                }
            }
        }
        */
    }
    
}

class Block
{
    int leftbound;
    int upperbound;
    Vector2 coordinates;
    int blockSize = 75;

    public Block()
    {
        leftbound = 0;
        upperbound = 0;
        coordinates = Vector2.Zero;
    }
    public Block(double left, double upper)
    {
        leftbound = (int)left;
        upperbound = (int)upper;
        coordinates = new Vector2((int)left / blockSize, (int)upper / blockSize);
    }
    public Block(int x, int y)
    {
        coordinates = new Vector2(x, y);
        leftbound = x * blockSize;
        upperbound = y * blockSize;
    }

    public int getLeftbound()
    {
        return leftbound;
    }
    public int getUpperbound()
    {
        return upperbound;
    }
    
    public void draw()
    {
        Engine.DrawRectSolid(new Bounds2(new Vector2(leftbound, upperbound), new Vector2(blockSize, blockSize)), Color.Black);
    }
}
