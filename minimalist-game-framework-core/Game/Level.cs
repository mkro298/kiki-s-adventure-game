using System;
using System.IO;
using System.Collections.Generic;

class Level 
{
    Texture [] background;
    int numBlocks;
    public int scroll;
    public String envCoords;
    public String enemyFile;
    public int color = 0;
    public Boolean DoorOpen;
    public int minPoints;

    public Level(Texture [] background, int numBlocks, String envCoords, String enemyFile, int minPoints)
    {
        this.background = background;
        this.numBlocks = numBlocks;
        this.envCoords = envCoords;
        this.enemyFile = enemyFile;
        this.minPoints = minPoints;
        Reload(this.envCoords);
    }

    public void Update()
    {
        Engine.DrawTexture(background[color], Vector2.Zero);

        if ((Scene.player.points >= minPoints)&&(color<5))
        {
            color++;
        }

        if (color == 5)
        {
            Engine.DrawTexture(Scene.door, new Vector2(8300 + scroll, 575), source: new Bounds2(75, 0, 75, 100));
            DoorOpen = true;
        }
        else
        {
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
