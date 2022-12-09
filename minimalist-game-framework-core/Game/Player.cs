using System;
using System.Collections.Generic;

class Player
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(1275, 750);
    // Define some constants controlling animation speed:
    static readonly float Framerate = 10;
    static readonly float WalkSpeed = Game.speed;

    //Load basic sprite sheet
    Texture tex = Engine.LoadTexture("basic.png");
    Texture powers = Engine.LoadTexture("powers.png");
    Texture texK = null;

    float frames = 6.0f;
    int bound = 100;
    public int currP = -1;

    // Keep track of K's state:
    public Vector2 kPos = Resolution / 2;
    public Vector2 kVel = new Vector2(WalkSpeed, 0);
    bool kFaceLeft = false;
    float kFrameIndex = 0;

    int points = 0;
    int yCoord = 0;
    public Boolean inhale = false;
    bool canMoveLeft = true;
    bool canMoveRight = true;

    // blocks array
    Block[] blocks;

    public Player() { }

    public Player(Block[] blocksArray)
    {
        blocks = blocksArray;
    }

    //assumes enemy is hit by player through powers or through inhaling 
    public void enemyHit(Enemy a)
    {
        points += 50;
    }

    public void Update(int scroll)
    {
        Engine.DrawString("Current Score: " + points.ToString(), new Vector2 (50,650), Color.White, Game.font);
        inhale = false;
        texK = tex;
        bool kIdle = true;
        frames = 6.0f;
        bound = 100;

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
        if (Engine.GetKeyUp(Key.Up) && canMoveUp)
        {
            kVel.Y+= WalkSpeed * WalkSpeed * WalkSpeed / 6;
            kIdle = false;
            yCoord = 200;
            frames = 6.0f;
            bound = 100;
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
        if (Engine.GetKeyHeld(Key.P)&&currP!=-1)
        {
            texK = powers;
            kIdle = false;
            yCoord = currP * 100;
            frames = 12.0f;
            bound = 200;
            inhale = true;

            if (kFaceLeft)
            {
                kPos.X -= 100f;
            }

        }

        // Advance through sprite's 6-frame animation and select the current frame
        kFrameIndex = (kFrameIndex + Engine.TimeDelta * Framerate) % frames;
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
