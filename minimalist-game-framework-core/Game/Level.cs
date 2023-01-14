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
    public int doorX;
    public int doorY;
    public int scrollBound;

    public Level(Texture [] background, int numBlocks, String envCoords, 
                 String enemyFile, int minPoints, int doorX, int doorY, int scrollBound)
    {
        this.background = background;
        this.numBlocks = numBlocks;
        this.envCoords = envCoords;
        this.enemyFile = enemyFile;
        this.minPoints = minPoints;
        this.doorX = doorX;
        this.doorY = doorY;
        this.scrollBound = scrollBound;
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
            
            if (Scene.numLevel == 1)
            {
                Engine.DrawTexture(Scene.door1, new Vector2(doorX + scroll, doorY), source: new Bounds2(75, 0, 75, 100));

            }else if (Scene.numLevel == 2)
            {
                Engine.DrawTexture(Scene.door2, new Vector2(doorX + scroll, doorY), source: new Bounds2(75, 0, 75, 100));
            }
            
            DoorOpen = true;
        }
        else
        {
            if (Scene.numLevel == 1)
            {
                Engine.DrawTexture(Scene.door1, new Vector2(doorX + scroll, doorY), source: new Bounds2(0, 0, 75, 100));
            }
            else if (Scene.numLevel == 2)
            {
                Engine.DrawTexture(Scene.door2, new Vector2(doorX + scroll, doorY), source: new Bounds2(0, 0, 75, 100));
            }
        }



        for (int i = 0; i < Scene.blocks.Length; i++)
        {
            Scene.blocks[i].draw(scroll);
        } 

        //adjust scroll
        if (Engine.GetKeyHeld(Key.Right) && Scene.player.getKPosition().X >= 940 && scroll >= scrollBound && Scene.player.getMoveRight())
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

        if (Scene.numLevel == 1)
        {
            Engine.DrawString("press the left/right", new Vector2(250 + scroll, 200), Color.White, Scene.font);
            Engine.DrawString(" arrow keys to run", new Vector2(250 + scroll, 225), Color.White, Scene.font);

            Engine.DrawString("press the up arrow", new Vector2(750 + scroll, 300), Color.White, Scene.font);
            Engine.DrawString("     key to jump", new Vector2(750 + scroll, 325), Color.White, Scene.font);

            Engine.DrawString("  press space to inhale", new Vector2(1875 + scroll, 200), Color.White, Scene.font);
            Engine.DrawString("enemies and gain powers", new Vector2(1875 + scroll, 225), Color.White, Scene.font);

            Engine.DrawString("     press 'up' twice to", new Vector2(1875 + scroll, 400), Color.White, Scene.font);
            Engine.DrawString("         jump mid-air", new Vector2(1875 + scroll, 425), Color.White, Scene.font);

            Engine.DrawString("       press 'alt' to kill", new Vector2(2800 + scroll, 200), Color.White, Scene.font);
            Engine.DrawString(" enemies with your powers", new Vector2(2800 + scroll, 225), Color.White, Scene.font);

            Engine.DrawString("you can use the powers once", new Vector2(2800 + scroll, 300), Color.White, Scene.font);

            Engine.DrawString("collect enough points to", new Vector2(5105 + scroll, 350), Color.White, Scene.font);
            Engine.DrawString("bring color to this world,", new Vector2(5105 + scroll, 375), Color.White, Scene.font);
            Engine.DrawString("open the gates, and exit", new Vector2(5105 + scroll, 400), Color.White, Scene.font);
            Engine.DrawString("         this realm", new Vector2(5105 + scroll, 425), Color.White, Scene.font);



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
