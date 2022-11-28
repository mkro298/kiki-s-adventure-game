using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(640, 480);
    Player b = new Player();


    public Game()
    {
    }
    public void Update()
    {
        b.Update(); 
    }
}
