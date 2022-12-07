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
    readonly Texture enemyDeath = Engine.LoadTexture("ENEMY_DEATH.png");
    Texture spriteSheet;

    int frames = 12;

    //enemy state
    Vector2 enemyPos = new Vector2(300f, 360.0f);
    Boolean movingLeft = false;
    float eFrameIndex = 0;

    Boolean enemyHit = false;
    public Boolean isAlive = true;
    Boolean hit = false;
    int framesDrawn = 0;
    int yCoord;
    
    int power = 0;
    Player pl;

    int time = 0;


    public Enemy(int p, Player a)
    {
        power = p;
        pl = a;
        yCoord = power;
        spriteSheet = enemy;
        yCoord = p;
    }

    public void hitPlayer()
    {
        if (!hit&&pl.inhale == true && Math.Abs(enemyPos.X-pl.kPos.X)<200 && Math.Abs(enemyPos.Y-pl.kPos.Y)<200)
        {
           pl.enemyHit(this);
            hit = true;
            spriteSheet = enemyDeath;
            frames = 9;
            yCoord = 0;
        }
    }

    public void runEnemyCode()
    {

        hitPlayer();
        if (movingLeft)
        {
            enemyPos.X--;
        }
        else
        {
            enemyPos.X++;
        }
        if (enemyPos.X == (200))
        {
            movingLeft = false;
        }
        if (enemyPos.X == 400)
        {
            movingLeft = true;
        }
        //animation
        eFrameIndex = (eFrameIndex + Engine.TimeDelta * Framerate) % frames;
        if (eFrameIndex >= 8 && frames == 9)
        {
            isAlive = false;
            pl.currP = power;
        }
        Bounds2 eFrameBounds = new Bounds2(((int)eFrameIndex) * 100,yCoord*100, 100, 100);

        //draw sprite
        Vector2 eDrawPos = enemyPos + new Vector2(-8, -8);
        TextureMirror eMirror = movingLeft ? TextureMirror.Horizontal : TextureMirror.None;
        Engine.DrawTexture(spriteSheet, eDrawPos, source: eFrameBounds, mirror: eMirror);
    }
}
