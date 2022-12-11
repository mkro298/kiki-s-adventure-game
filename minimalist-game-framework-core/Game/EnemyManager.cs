using System;
using System.Collections.Generic;
using System.Text;

class EnemyManager
{
    List<Enemy> enemies = new List<Enemy>();
    Player pl;

    public EnemyManager(Player a)
    {
        Player pl = a;
    }

    public void initializeEnemies()
    {
        enemies.Add(new Enemy(1, pl, 200, 400, 200));
        enemies.Add(new Enemy(2, pl, 500, 700, 200));
        enemies.Add(new Enemy(3, pl, 800, 1000, 200));
        enemies.Add(new Enemy(4, pl, 1100, 1300, 200));
    }

    public void Update()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].isAlive)
            {
                enemies[i].runEnemyCode();
            }
            else
            {
                enemies.RemoveAt(i);
                i--;
            }
            
        }
    }
}

