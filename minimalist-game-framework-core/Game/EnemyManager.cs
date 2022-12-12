using System;
using System.Collections.Generic;
using System.Text;

class EnemyManager
{
    List<Enemy> enemies = new List<Enemy>();
    Player pl;

    public EnemyManager(Player a)
    {
        pl = a;
    } 

    public void initializeEnemies()
    {
       // enemies.Add(new Enemy(0,pl, 400, 450, 480));
        enemies.Add(new Enemy(1, pl, 900, 990, 358));
        enemies.Add(new Enemy(2, pl, 1780, 1900, 582));
        enemies.Add(new Enemy(3, pl, 3800, 4100, 508));
        enemies.Add(new Enemy(4, pl, 4800, 4900, 582));
    }

    public void Update(int scroll)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].isAlive)
            {
                enemies[i].runEnemyCode(scroll);
            }
            else
            {
               enemies.RemoveAt(i);
               i--;
            }
            
        }
    }
}

