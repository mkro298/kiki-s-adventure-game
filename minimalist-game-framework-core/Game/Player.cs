using System;
using System.Collections.Generic;

class Player
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(640, 480);
    // Define some constants controlling animation speed:
    static readonly float Framerate = 10;
    static readonly float WalkSpeed = 70;

    //Load basic sprite sheet
    Texture tex = Engine.LoadTexture("basic.png");
    Texture powers = Engine.LoadTexture("powers.png");
    Texture texK = null;

    float frames = 6.0f;
    int bound = 100;
    int currP = 0;

    // Keep track of K's state:
    public Vector2 kPos = Resolution / 2;
    public Vector2 kVel = new Vector2(WalkSpeed, 0);
    bool kFaceLeft = false;
    float kFrameIndex = 0;

    int points = 0;
 
    public Boolean inhale = false;

    public Player()
    {
    }
    int yCoord = 0;

    //assumes enemy is hit by player through powers or through inhaling 
    public void enemyHit(Enemy a)
    {
        currP = a.returnPower();
        points += 50;
    }
    public void Update()
    {
        inhale = false;
        texK = tex;
        bool kIdle = true;
        frames = 6.0f;
        bound = 100;
        // For moving left
        if (Engine.GetKeyHeld(Key.A))
            {
                kPos.X -= Engine.TimeDelta * WalkSpeed;
                kFaceLeft = true;
                kIdle = false;
                yCoord = 200;
            frames = 6.0f;
            bound = 100;
        }
            // For moving right
            if (Engine.GetKeyHeld(Key.D))
            {
                kPos.X += Engine.TimeDelta * WalkSpeed;
                kFaceLeft = false;
                kIdle = false;
                yCoord = 200;
            frames = 6.0f;
            bound = 100;
        }
            // For moving up
            if (Engine.GetKeyHeld(Key.W))
            {
                kVel.Y = 5
                kVel.Y += 10;
                kPos.Y -= kVel.Y;
                kIdle = false;
                yCoord = 200;
            frames = 6.0f;
            bound = 100;
            inhale = true;
            }
            // Otherwise, full enable gravity
            else if(kPos.Y < 380)
            {
                kPos.Y += kVel.Y * Engine.TimeDelta + 10;
            }
            // For inhaling
            if (Engine.GetKeyHeld(Key.Space))
            {
                kIdle = false;
                yCoord = 100;
            frames = 6.0f;
            bound = 100;
        }
        if (Engine.GetKeyHeld(Key.P))
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
        Vector2 kDrawPos =  kPos + new Vector2(-8, -8);
        TextureMirror kMirror = kFaceLeft ? TextureMirror.Horizontal : TextureMirror.None;
        Engine.DrawTexture(texK, kDrawPos, source: kFrameBounds, mirror: kMirror);
        if (kFaceLeft&&bound==200)
        {
            kPos.X += 100f;
        }
    }
}
