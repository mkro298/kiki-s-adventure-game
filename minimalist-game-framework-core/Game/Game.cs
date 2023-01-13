using System;
using System.IO;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Kiki";
    public static readonly Vector2 Resolution = new Vector2(1275, 750);

    Boolean dead = true;
    Double time = 0;
    Music music = Engine.LoadMusic("Kiki.mp3");
    public static int speed = 5;
    
    static Block[] blocks;
    Player player = new Player(blocks);
    Scene scene = new Scene();

    EnemyManager enemyManager;

    public Game()
    {
        Scene.reload();

        PlayerInput controller = new PlayerInput(player);


        enemyManager = new EnemyManager(player, Scene.levels);

        dead = true;

        
        //plays music
        if (time % 125.0 == 0)
        {
            Engine.PlayMusic(music);
            time = 0;
        } 
    }

    public void Update()
    {
        time += 0.016;
        Scene.Update();
    }
}
