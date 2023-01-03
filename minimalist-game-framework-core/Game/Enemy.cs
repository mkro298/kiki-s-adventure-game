using System;
using System.Collections.Generic;

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
    int framesDrawn = 0;
    int yCoord;

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
        if (!hit&&pl.inhale == true && Math.Abs((enemyPos.X-8+scroll)-pl.kPos.X)<80 && Math.Abs(enemyPos.Y-pl.kPos.Y)<40)
        {
            pl.enemyHit(this);
            hit = true;
            spriteSheet = enemyDeath; 
            frames = 9;
            yCoord = 0;
            
        }

        if (Math.Abs((enemyPos.X - 8 + scroll) - pl.kPos.X) < 70 && Math.Abs(enemyPos.Y - pl.kPos.Y) < 20)
        {
            if(enemyPos.X >= pl.kPos.X)
            {
                pl.bounceBack = true;
            }
            else
            {
                pl.bounceForward = true;
            }
        }
        else
        {
            pl.bounceBack = false;
            pl.bounceBack = false;
        }


    }

    public void runEnemyCode(int scroll) 
    {
        //enemyPos.X += scroll;
        hitPlayer(scroll);
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
        eFrameIndex = (eFrameIndex + Engine.TimeDelta * Framerate) % frames;
        if (eFrameIndex >= 8 && frames == 9)
        {
            isAlive = false;
            pl.currP = power;
        }
        Bounds2 eFrameBounds = new Bounds2(((int)eFrameIndex) * 100,yCoord*100, 100, 100);

        //draw sprite
        Vector2 eDrawPos = enemyPos + new Vector2(-8 + scroll, -8);
        TextureMirror eMirror = movingLeft ? TextureMirror.Horizontal : TextureMirror.None;
        Engine.DrawTexture(spriteSheet, eDrawPos, source: eFrameBounds, mirror: eMirror);
    }
}
