using System;
using System.Collections.Generic;

class Enemy
{
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(640, 480);

    //animation speeds
    static readonly float Framerate = 10;
    static readonly float WalkSpeed = 50;

    //load basic sprite sheet
    readonly Texture enemy = Engine.LoadTexture("ENEMY.png");

    //enemy state
    Vector2 enemyPos = new Vector2(400f, 200.0f);
    Boolean movingLeft = false;
    float eFrameIndex = 0;

    Boolean enemyHit = false;
    Boolean isAlive = true;
    int framesDrawn = 0;
    int yCoord = 0;
    
    int power = 0;
    Player pl;

    int time = 0;


    public Enemy(int p, Player a)
    {
        power = p;
        pl = a;
        yCoord = power;
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
        //animation
        eFrameIndex = (eFrameIndex + Engine.TimeDelta * Framerate) % 12;
        Bounds2 eFrameBounds = new Bounds2(((int)eFrameIndex) * 100,yCoord, 100, 100);

        //draw sprite
        Vector2 eDrawPos = enemyPos + new Vextor2(-8, -8);
        TextureMirror eMirror = movingLeft ? TextureMirror.Horizontal : TextureMirror.None;
        Engine.DrawTexture(enemy, eDrawPos, source: eFrameBounds, mirror: eMirror);
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
