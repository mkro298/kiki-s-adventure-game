using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

class EnemyManager
{
    List<Enemy> enemies = new List<Enemy>();
    Boolean[] levels;
    Player pl;

    public EnemyManager(Player a, Boolean[] levelsParam)
    {
        pl = a;
        levels = levelsParam;
    } 

    public void initializeEnemies(String file)
    {
        StreamReader sr = new StreamReader(file);
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            string[] nums = line.Split(' ');
            if (levels[0] == true)
            {
                enemies.RemoveAll();
                enemies.Add(new Enemy(Int32.Parse(nums[0]), pl, Int32.Parse(nums[1]), Int32.Parse(nums[2]), Int32.Parse(nums[3])));
            } else if (levels[1] == true)
            {
                enemies.RemoveAll();
                enemies.Add(new Enemy(Int32.Parse(nums[0]), pl, Int32.Parse(nums[1]), Int32.Parse(nums[2]), Int32.Parse(nums[3])));
            }
            
        }
        sr.Close();
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

