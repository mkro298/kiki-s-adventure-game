using System;
using System.Collections.Generic;

class Enemy
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(640, 480);
    static readonly float Framerate = 100;
    readonly Texture enemy = Engine.LoadTexture("ENEMY.png");

    Vector2 enemyPos = new Vector2(400f, 200.0f);
    Boolean movingLeft = false;
    Boolean enemyHit = false;
    Boolean isAlive = true;
    int framesDrawn = 0;
    
    int power = 0;
    Player pl;


    public Enemy(int p, Player a)
    {
        power = p;
        pl = a;
    }

    public void hitPlayer()
    {
        if (pl.inhale == true&&enemyPos.X-pl.kPos.X<100)
        {
            pl.enemyHit(this);
        }
    }

    public void runEnemyCode()
    {
        //TODO
    }
    public int returnPower()
    {
        return power;
    }


    public void moveEnemy(Vector2 minPos, Vector2 maxPos)
    {
        if(enemyPos.X - 10f < minPos.X)
        {
            movingLeft = false;
        }
        else if (enemyPos.X + 10f  > maxPos.X)
        {
            movingLeft = true;
        }
        else if (movingLeft && enemyPos.X - 10f >= minPos.X)
        {
            enemyPos.X -= 10f;
        } 
        else if (!movingLeft && enemyPos.X < maxPos.X)
        {
            enemyPos.X += 10f;
        }
        Engine.DrawTexture(enemy, enemyPos, null, new Vector2(150.0f, 250.0f));
        framesDrawn++;
    }
}
