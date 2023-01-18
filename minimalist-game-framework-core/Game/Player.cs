using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

class Player 
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(1275, 750);
    // Define some constants controlling animation speed:
    static readonly float Framerate = 10;
    static readonly float WalkSpeed = Game.speed;

    //sound effect
    public static Sound heartbeat = Engine.LoadSound("Heartbeat.mp3");
    static Sound attack = Engine.LoadSound("attack.mp3");
    static Sound powerUp = Engine.LoadSound("power.mp3");

    //Indicator texture
    Texture indicator = Engine.LoadTexture("Indicator.png");

    //Load basic sprite sheet
    Texture tex = Engine.LoadTexture("basic.png");
    Texture powers = Engine.LoadTexture("powers.png");
    Texture playerHit = Engine.LoadTexture("enemyHit.png");
    Texture texK = null;

    //display info 
    float frames = 6.0f;
    int bound = 100;

    //current player power 
    public int currP = -1;


    // Keep track of K's state:
    public Vector2 kPos = Resolution / 2;
    public Vector2 kVel = new Vector2(WalkSpeed, 0);
    bool kFaceLeft = false;
    float kFrameIndex = 0;

    public int points = 0;
    public int health = 0;
    int yCoord = 0;
    public Boolean inhale = false;
    public Boolean usingPower = false;
    bool canMoveLeft = true;
    bool canMoveRight = true;
    int jumps = 0;
    // Variables keeping track of player input
    bool goLeft = false;
    bool goRight = false;
    bool goUp = false;
    bool goPower = false;
    bool goBounce = false;
    public bool bounceBack = false;
    public bool bounceForward = false;
    bool hit = false;
    int level;

    StreamReader sr;

    // blocks array
    Block[] blocks;

    public Player() { }

    public Player(Block[] blocksArray, int l)
    {
        blocks = blocksArray;
        level = l;
    }

    //assumes enemy is hit by player through powers and increases points 
    public void enemyHit()
    {
        if (usingPower)
        {
            points += 50; 
        }
    }

    //assumes player collided with enemy and takes one life 
    public void enemyCollision()
    {
        health += 100;
        hit = true;
    }

    //returns overall high score for the level 
    public String highScore()
    {
        Engine.reload();
        //stores the high score on a seperate file for each level 
        if (level == 1)
        {
            sr = new StreamReader("assets/highScore1.txt");
        }
        else
        {
            sr = new StreamReader("assets/highScore2.txt");
        }
        int highScore = int.Parse(sr.ReadLine());
        sr.Close();
        //checks if current score beat high score 
        if (points > highScore)
        {
            if (level == 1)
            {
                File.WriteAllTextAsync("assets/highScore1.txt", points.ToString());
            }
            else
            {
                File.WriteAllTextAsync("assets/highScore2.txt", points.ToString());
            }
            return points.ToString();
        }
        return highScore.ToString();
    }

    //main update loop 
    public void Update(int scroll)
    {
        //resets player state 
        inhale = false;
        texK = tex;
        bool kIdle = true;
        frames = 6.0f;
        bound = 100;

        //draws circle that shows current power 
        Engine.DrawTexture(indicator, new Vector2(910, 50), source: new Bounds2((currP + 1) * 60, 0, 60, 60));

        canMoveLeft = true;
        canMoveRight = true;
        bool canMoveDown = true;
        bool canMoveUp = true;
        // check block bounds
        for (int i = 0; i < blocks.Length; i++)
        {
            Bounds2 bounds = new Bounds2(new Vector2(blocks[i].getLeftbound() + scroll, blocks[i].getUpperbound()), new Vector2(75f, 75f));

            if (bounds.Contains(new Vector2(kPos.X + 44, kPos.Y + 17)) ||
                bounds.Contains(new Vector2(kPos.X + 29, kPos.Y + 25)) ||
                bounds.Contains(new Vector2(kPos.X + 21, kPos.Y + 45)) ||
                bounds.Contains(new Vector2(kPos.X + 20, kPos.Y + 80)) ||
                bounds.Contains(new Vector2(kPos.X + 31, kPos.Y + 93)))
            {
                canMoveLeft = false;
                if (bounds.Contains(new Vector2(kPos.X + 45, kPos.Y + 17)) ||
                    bounds.Contains(new Vector2(kPos.X + 30, kPos.Y + 25)) ||
                    bounds.Contains(new Vector2(kPos.X + 22, kPos.Y + 45)) ||
                    bounds.Contains(new Vector2(kPos.X + 21, kPos.Y + 80)) ||
                    bounds.Contains(new Vector2(kPos.X + 32, kPos.Y + 93)))
                {
                    kPos.X++;
                }
            }

            if (bounds.Contains(new Vector2(kPos.X + 56, kPos.Y + 17)) ||
                bounds.Contains(new Vector2(kPos.X + 71, kPos.Y + 25)) ||
                bounds.Contains(new Vector2(kPos.X + 79, kPos.Y + 45)) ||
                bounds.Contains(new Vector2(kPos.X + 80, kPos.Y + 80)) ||
                bounds.Contains(new Vector2(kPos.X + 69, kPos.Y + 93)))
            {
                canMoveRight = false;
                if (bounds.Contains(new Vector2(kPos.X + 55, kPos.Y + 17)) ||
                    bounds.Contains(new Vector2(kPos.X + 70, kPos.Y + 25)) ||
                    bounds.Contains(new Vector2(kPos.X + 78, kPos.Y + 45)) ||
                    bounds.Contains(new Vector2(kPos.X + 79, kPos.Y + 80)) ||
                    bounds.Contains(new Vector2(kPos.X + 68, kPos.Y + 93)))
                {
                    kPos.X--;
                }
            }

            if (bounds.Contains(new Vector2(kPos.X + 22, kPos.Y + 39)) ||
                bounds.Contains(new Vector2(kPos.X + 30, kPos.Y + 24)) ||
                bounds.Contains(new Vector2(kPos.X + 50, kPos.Y + 16)) ||
                bounds.Contains(new Vector2(kPos.X + 70, kPos.Y + 24)) ||
                bounds.Contains(new Vector2(kPos.X + 78, kPos.Y + 39)))
            {
                canMoveUp = false;
                kVel.Y = 0;
                if (bounds.Contains(new Vector2(kPos.X + 22, kPos.Y + 40)) ||
                    bounds.Contains(new Vector2(kPos.X + 30, kPos.Y + 25)) ||
                    bounds.Contains(new Vector2(kPos.X + 50, kPos.Y + 17)) ||
                    bounds.Contains(new Vector2(kPos.X + 70, kPos.Y + 25)) ||
                    bounds.Contains(new Vector2(kPos.X + 78, kPos.Y + 40)))
                {
                    kPos.Y++;
                }
            }

            if (bounds.Contains(new Vector2(kPos.X + 65, kPos.Y + 99)) ||
                bounds.Contains(new Vector2(kPos.X + 34, kPos.Y + 99)))
            {
                canMoveDown = false;
                jumps = 0;
                kVel.Y = 0;
                if (bounds.Contains(new Vector2(kPos.X + 65, kPos.Y + 98)) ||
                    bounds.Contains(new Vector2(kPos.X + 34, kPos.Y + 98)))
                {
                    kPos.Y--;
                }
            }
        }

        // For moving left
        if (Engine.GetKeyHeld(Key.Left) && canMoveLeft)
        {
            if (kPos.X > 255)
            {
                kPos.X -= WalkSpeed;
            }
            kFaceLeft = true;
            kIdle = false;
            yCoord = 200;
            frames = 6.0f;
            bound = 100;
        }
        // For moving right
        if (Engine.GetKeyHeld(Key.Right) && canMoveRight)
        {
            if (kPos.X < 940)
            {
                kPos.X += WalkSpeed;
            }
            kFaceLeft = false;
            kIdle = false;
            yCoord = 200;
            frames = 6.0f;
            bound = 100;
        }
        // For moving up
        if (Engine.GetKeyDown(Key.Up) && canMoveUp && jumps < 2)
        {
            kVel.Y *= 0.25f;
            kVel.Y += WalkSpeed * WalkSpeed * WalkSpeed / 6;

            kVel.Y = Math.Min(kVel.Y, 20.0f);

            kIdle = false;
            yCoord = 200;
            frames = 6.0f;
            bound = 100;
            jumps++;
        }
        
        // Otherwise, full enable gravity
        else if (canMoveDown)
        {
            kVel.Y--;
        }
        kPos.Y -= kVel.Y;

        // For inhaling
        if (Engine.GetKeyHeld(Key.Space))
        {
            kIdle = false;
            yCoord = 100;
            frames = 6.0f;
            bound = 100;
            inhale = true;
        } 
        //for using power 
        if (usingPower||((Engine.GetKeyDown(Key.LeftAlt)|| Engine.GetKeyDown(Key.RightAlt)) &&currP!=-1))
        {
            texK = powers;
            kIdle = false;
            yCoord = currP * 100;
            frames = 12.0f;
            bound = 200;
            usingPower = true;
            if (kFaceLeft)
            {
                kPos.X -= 100f;
            }

        }
        //bounceback when enemy hit 
        if (hit)
        {
            if (canMoveLeft)
            {
                kPos.X -= 10;
            }else if (canMoveRight){
                kPos.X += 10;
            }

            texK = playerHit;
            kIdle = false;
            yCoord = 0;
            frames = 7.0f;
            bound = 100;
        }

        // Advance through sprite's 6-frame animation and select the current frame
        kFrameIndex = (kFrameIndex + Engine.TimeDelta * Framerate) % frames;
        if (kFrameIndex > 11 && usingPower)
        {
            usingPower = false;
            Engine.PlaySound(powerUp);
            currP = -1;
        }

        if (kFrameIndex > 6 && hit)
        {
            Engine.PlaySound(attack);
            hit = false;
        }
        Bounds2 kFrameBounds = new Bounds2(((int)kFrameIndex) * bound, kIdle ? 0 : yCoord, bound, 100);
        // Draw sprite
        Vector2 kDrawPos = kPos + new Vector2(0, 0);
        TextureMirror kMirror = kFaceLeft ? TextureMirror.Horizontal : TextureMirror.None;
        Engine.DrawTexture(texK, kDrawPos, source: kFrameBounds, mirror: kMirror);
        if (kFaceLeft && bound == 200)
        {
            kPos.X += 100f;
        }
    }

    public bool getMoveLeft()
    {
        return canMoveLeft;
    }
    public bool getMoveRight()
    {
        return canMoveRight;
    }
    public Vector2 getKPosition()
    {
        return kPos;
    }

    public void updateBlocks(Block[] blockArray)
    {
        blocks = blockArray;
    }
}