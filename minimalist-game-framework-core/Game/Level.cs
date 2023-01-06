using System;
using System.IO;
using System.Collections.Generic;

class Level 
{
    Texture backgroundColor;
    Texture backgroundGrey;
    int numBlocks;
    public int scroll;
    String envCoords;
    public String enemyFile;
    public int color = 1;
    public Boolean DoorOpen;

    public Level(Texture backgroundColor, Texture backgroundGrey, int numBlocks, String envCoords, String enemyFile)
    {
        this.backgroundColor = backgroundColor;
        this.backgroundGrey = backgroundGrey;
        this.numBlocks = numBlocks;
        this.envCoords = envCoords;
        this.enemyFile = enemyFile;
        Reload(this.envCoords);
    }

    public void Update()
    {
        if (color == 1)
        {
            Engine.DrawTexture(backgroundColor, Vector2.Zero);
            Engine.DrawTexture(Scene.door, new Vector2(8300 + scroll, 575), source: new Bounds2(75, 0, 75, 100));
            DoorOpen = true;
        }
        else
        {
            Engine.DrawTexture(backgroundGrey, Vector2.Zero);
            Engine.DrawTexture(Scene.door, new Vector2(8300 + scroll, 575), source: new Bounds2(0, 0, 75, 100));
        }



        for (int i = 0; i < Scene.blocks.Length; i++)
        {
            Scene.blocks[i].draw(scroll);
        }

        //adjust scroll
        if (Engine.GetKeyHeld(Key.Right) && Scene.player.getKPosition().X >= 940 && scroll >= -7425 && Scene.player.getMoveRight())
        {
            scroll -= Game.speed;
        }
        if (Engine.GetKeyHeld(Key.Left) && Scene.player.getKPosition().X <= 255 && scroll <= 0 && Scene.player.getMoveLeft())
        {
            scroll += Game.speed;
        }
        // draw the blocks
        for (int i = 0; i < Scene.blocks.Length; i++)
        {
            Scene.blocks[i].draw(scroll);
        }

    }

    public void Reload(String envCoords)
    {
        StreamReader sr = new StreamReader(envCoords);
        Scene.blocks = new Block[numBlocks];
        for (int i = 0; i < Scene.blocks.Length; i++)
        {
            string line = sr.ReadLine();
            string[] nums = line.Split(' ');

            Scene.blocks[i] = new Block(Int32.Parse(nums[0]), Int32.Parse(nums[1]), Int32.Parse(nums[2]));
        }
        Scene.player.updateBlocks(Scene.blocks);
        sr.Close();
    }

    public void Draw()
    {
        
    }
}
