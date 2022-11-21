using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(640, 480);

    // Define some constants controlling animation speed:
    static readonly float Framerate = 10;
    static readonly float WalkSpeed = 50;

    //Load basic sprite sheet
    Texture texK = Engine.LoadTexture("basic.png");

    // Keep track of K's state:
    Vector2 kPos = Resolution / 2;
    bool kFaceLeft = false;
    float kFrameIndex = 0;
    public Game()
    {
    }
    int yCoord = 0;
    public void Update()
    {
        bool kIdle = true;
            // For moving left
            if (Engine.GetKeyHeld(Key.A))
            {
                kPos.X -= Engine.TimeDelta * WalkSpeed;
                kFaceLeft = true;
                kIdle = false;
                yCoord = 200;
        }
            // For moving right
            if (Engine.GetKeyHeld(Key.D))
            {
                kPos.X += Engine.TimeDelta * WalkSpeed;
                kFaceLeft = false;
                kIdle = false;
                yCoord = 200;
        }
            // For moving up
            if (Engine.GetKeyHeld(Key.W))
            {
                kPos.Y -= Engine.TimeDelta * WalkSpeed;
                kIdle = false;
                yCoord = 200;
        }
            // For inhaling
            if (Engine.GetKeyHeld(Key.Space))
            {
                kIdle = false;
                yCoord = 100;
            }

        // Advance through sprite's 6-frame animation and select the current frame
        kFrameIndex = (kFrameIndex + Engine.TimeDelta * Framerate) % 6.0f;
        Bounds2 kFrameBounds = new Bounds2(((int)kFrameIndex) * 100, kIdle ? 0 : yCoord, 100, 100);

        // Draw sprite
        Vector2 kDrawPos =  kPos + new Vector2(-8, -8);
        TextureMirror kMirror = kFaceLeft ? TextureMirror.Horizontal : TextureMirror.None;
        Engine.DrawTexture(texK, kDrawPos, source: kFrameBounds, mirror: kMirror);
    }
}
