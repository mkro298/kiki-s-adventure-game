using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(640, 480);
    Player b = new Player();


    public Game()
    {
        Enemy c = new Enemy(0, b);
    }
    public void Update()
    {
        b.Update(); 
    }
}
