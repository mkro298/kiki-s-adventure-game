using System;
using System.IO;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Minimalist Game Framework"; 
    public static readonly Vector2 Resolution = new Vector2(1275, 750);
    public static int speed = 5;
    int scroll = 0;
    readonly Font font = Engine.LoadFont("font.ttf", 20);
    readonly Texture background = Engine.LoadTexture("Kirby red level background.png");
    static int numBlocks = 202;
    static Block[] blocks;
    Player player = new Player(blocks);
    
    public Game()
    {
        Engine.DrawTexture(background, Vector2.Zero);
        reload();
        Enemy enemy = new Enemy(0, player);
    }
    public void Update()
    {
        // reload the files and textures if r is pressed
        Engine.DrawTexture(background, Vector2.Zero);
        if (Engine.GetKeyDown(Key.R))
        {
            reload();
        }

        // update the player
        player.Update(scroll);

        //adjust scroll
        if (Engine.GetKeyHeld(Key.Right) && player.getKPosition().X > 940 && scroll > -7425 && player.getMoveRight())
        {
            scroll -= speed;
        }
        if (Engine.GetKeyHeld(Key.Left) && player.getKPosition().X < 255 && scroll < 0 && player.getMoveLeft())
        {
            scroll += speed;
        }

        // draw the blocks
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].draw(scroll);
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
