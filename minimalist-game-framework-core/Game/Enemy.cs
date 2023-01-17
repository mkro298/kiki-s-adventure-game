using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

class Enemy
{
    //animation speeds
    static readonly float Framerate = 10;
    static readonly float WalkSpeed = 1;

    //load basic sprite sheet
    readonly Texture enemy = Engine.LoadTexture("ENEMY.png");
    readonly Texture enemyDeath = Engine.LoadTexture("ENEMY_DEATH.png");
    Texture spriteSheet;

    int frames = 12;

    //enemy state
    Vector2 enemyPos;
    Boolean movingLeft = false;
    float eFrameIndex = 0;

    Boolean enemyHit = false;
    public Boolean isAlive = true;
    public Boolean hit = false;
    Boolean noTransfer = false;
    int framesDrawn = 0;
    int yCoord;
    public Boolean enemyDying = false;

    public Boolean flickering = false;
    public int flick = 0;
    public Boolean visible = true;

    int power = 0;
    Player pl;

    int time = 0;

    int minX;
    int maxX;
    int yPos;


    public Enemy(int p, Player a, int minX, int maxX, int yPos)
    {
        power = p;
        pl = a;
        spriteSheet = enemy;
        yCoord = p;
        this.minX = minX;
        this.maxX = maxX;
        this.yPos = yPos;
        enemyPos = new Vector2(minX, yPos);
    }

    public void hitPlayer(int scroll)
    { 
        if (!hit&&pl.inhale == true && Math.Abs((enemyPos.X-8+scroll)-pl.kPos.X)<150 && Math.Abs(enemyPos.Y-pl.kPos.Y)<80)
        {
            pl.enemyHitI();
            hit = true;
            spriteSheet = enemyDeath; 
            frames = 9;
            yCoord = 0;
            enemyDying = true;
            
        }
        else if(!hit && pl.usingPower == true && Math.Abs((enemyPos.X - 8 + scroll) - pl.kPos.X) < 200 && Math.Abs(enemyPos.Y - pl.kPos.Y) < 80)
        {
            pl.enemyHitP();
            hit = true;
            spriteSheet = enemyDeath;
            frames = 9;
            yCoord = 0;
            noTransfer = true;
            enemyDying = true;
        }

        if (Math.Abs((enemyPos.X - 8 + scroll) - pl.kPos.X) < 70 && Math.Abs(enemyPos.Y - pl.kPos.Y) < 20)
        {
            if (!flickering)
            {
                pl.enemyCollision();
                flickering = true;
                flick = 0;
            }                
        }
        if (flickering)
        {
                flick++;
                if (flick % 5 == 0)
                {
                    visible = !visible;
                }
                if (flick >= 200)
                {
                    flickering = false;
                    flick = 0;
                    visible = true;
                }
            
        }
    }

    public void runEnemyCode(int scroll) 
    {
        //enemyPos.X += scroll;
        if (!hit)
        {
            hitPlayer(scroll);
        }
        if (movingLeft)
        {
            enemyPos.X -= WalkSpeed;
        }
        else
        {
            enemyPos.X += WalkSpeed;
        }
        if (enemyPos.X == minX)
        {
            movingLeft = false;
        }
        if (enemyPos.X == maxX)
        {
            movingLeft = true;
        }
        //animation
        if (enemyDying)
        {
            eFrameIndex = 0;
            enemyDying = false;
        }
        else
        {
            eFrameIndex = (eFrameIndex + Engine.TimeDelta * Framerate) % frames;
        }
        if (eFrameIndex >= 8 && frames == 9)
        {
            isAlive = false;
            if (!noTransfer)
            {
                pl.currP = power;
            }
        }
        Bounds2 eFrameBounds = new Bounds2(((int)eFrameIndex) * 100,yCoord*100, 100, 100);

        //draw sprite
        Vector2 eDrawPos = enemyPos + new Vector2(-8 + scroll, -8);
        TextureMirror eMirror = movingLeft ? TextureMirror.Horizontal : TextureMirror.None;
        if (visible)
        {
            Engine.DrawTexture(spriteSheet, eDrawPos, source: eFrameBounds, mirror: eMirror);
        }
    }
}
