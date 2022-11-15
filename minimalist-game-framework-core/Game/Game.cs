using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(1275, 750);
    readonly Texture puddleWalk = Engine.LoadTexture("env - Copy.png");

    public Game()
    {
    }

    public void Update()
    {
        Engine.DrawTexture(puddleWalk, Vector2.Zero);
    }
}
