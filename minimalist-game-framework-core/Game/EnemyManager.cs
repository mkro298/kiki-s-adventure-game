using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

class EnemyManager
{
    List<Enemy> enemies = new List<Enemy>();
    Boolean[] levels;
    Player pl;
    Random rand = new Random();

    //constructor
    public EnemyManager(Player a, Boolean[] levelsParam) 
    {
        pl = a;
        levels = levelsParam;
    } 

    //loads the enemies onto the level
    public void initializeEnemies(String file)
    {
        enemies.Clear();
        StreamReader sr = new StreamReader(file);  //read enemy data in from a file
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            string[] nums = line.Split(' ');

            //first level enemies
            if (levels[0] == true)
            {

                enemies.Add(new Enemy(Int32.Parse(nums[0]), pl, Int32.Parse(nums[1]), Int32.Parse(nums[2]), Int32.Parse(nums[3])));
            } 
            
            //second level enemies
            else if (levels[1] == true)
            { 
                enemies.Add(new Enemy(Int32.Parse(nums[0]), pl, Int32.Parse(nums[1]), Int32.Parse(nums[2]), Int32.Parse(nums[3])));

            }
            
        }
        sr.Close();
    }

    public void Update(int scroll)
    {
        //loops through enemy list and runs uodate code
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

