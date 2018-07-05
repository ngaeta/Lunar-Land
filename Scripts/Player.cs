using Aiv.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawExercise;

namespace SquareInvadersExercise
{
    class Player
    {
        const string HEART_SPRITE_NAME = "Assets/heartSmall.png";

        Vector2 position;
        int width;
        int height;
        //Rectangle baseRect;
        //Rectangle cannonRect;
        SpriteObj sprite;
        SpriteObj[] heartsEnergy;
        float speed;
        const float maxSpeed = 160.0f;
        int distToSide;
        Bullet[] bullets;
        float counter;
        bool isFirePressed;
        float shootDelay;
        bool isAlive;
        bool isVisible;
        //float timeDeadAnim;
        //float currentTimeDead;
        //ColorRGB colorBackground, colorBorder;
        int ray;
        int energy;


        public Player(Vector2 pos, /*ColorRGB colorBackground, ColorRGB colorBorder*/int energy = 3)
        {
            position = pos;
            distToSide = 20;
            shootDelay = 1f;
            counter = shootDelay;
            isAlive = true;
            
            isFirePressed = false;
            isVisible = true;
            this.energy = energy;

            //timeDeadAnim = 0.05f;
            //currentTimeDead = timeDeadAnim;
            //this.colorBackground = colorBackground;
            //this.colorBorder = colorBorder;

            //baseRect = new Rectangle(position.X - width / 2, position.Y - height / 2, height / 2, width);
            //baseRect.SetColor(colorBackground, colorBorder);
            //int cannWidth = width / 3;
            //cannonRect = new Rectangle(position.X - cannWidth / 2, position.Y - height, height / 2, cannWidth);
            //cannonRect.SetColor(colorBackground, colorBorder);

            sprite = new SpriteObj("Assets/player.png", position);
            sprite.Translate(-sprite.Width / 2, -sprite.Height); //la sposttiamo sopra, il pivot no però
            width = sprite.Width;
            height = sprite.Height;
            ray = width / 2;
            bullets = new Bullet[30];
            
            //bullet
            ColorRGB bulletCol = new ColorRGB(255, 255, 255);
            Vector2 velocity = new Vector2(0, -200f);

            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = new Bullet(2, 11, 1, bulletCol, velocity);
            }

            //hearts energy
            heartsEnergy = new SpriteObj[energy];
            Vector2 heartPos = new Vector2(position.X - width/2 - 6, position.Y);

            for (int i = 0; i < energy; i++)
            {
                heartsEnergy[i] = new SpriteObj(HEART_SPRITE_NAME, heartPos);
                heartPos.X += heartsEnergy[i].Width - 5;
            }
        }

        public bool Visible
        {
            get { return isVisible;  }
        }

        public void Input()
        {
            counter += GfxTools.Win.deltaTime;

            if (GfxTools.Win.GetKey(KeyCode.Right))
            {
                speed = maxSpeed;
            }
            else if (GfxTools.Win.GetKey(KeyCode.Left))
            {
                speed = -maxSpeed;
            }
            else
                speed = 0;

            if (GfxTools.Win.GetKey(KeyCode.Space))
            {
                if (isFirePressed == false && counter >= shootDelay)
                {
                    isFirePressed = true;
                    Shoot();
                    counter = 0;
                }
            }
            else if (isFirePressed)
            {
                isFirePressed = false;
            }
        }

        private Bullet GetFreeBullet()
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i].IsAlive == false)
                {
                    return bullets[i];
                }
            }
            return null;
        }

        public Vector2 GetCannonPosition()
        {
            Vector2 pos = new Vector2(position.X, position.Y-height/2);
            //GfxTools.DrawRect((int) pos.X, (int) pos.Y, 4, 4, new ColorRGB(255, 0, 0));
            return pos;
        }

        public void Shoot()
        {
            Bullet b = GetFreeBullet();
            if (b != null)
            {
                b.Shoot(new Vector2(position.X,position.Y-height-15));
            }
        }

        public void Update()
        {
            if (isAlive)
            {
                float deltaX = speed * GfxTools.Win.deltaTime;
                position.X += deltaX;
                float maxX = position.X + width / 2;
                float minX = position.X - width / 2;

                if (maxX > GfxTools.Win.width - distToSide)
                {
                    float overflowX = maxX - (GfxTools.Win.width - distToSide);
                    position.X -= overflowX;
                    deltaX -= overflowX;
                }
                else if (minX < distToSide)
                {
                    float overflowX = minX - distToSide;
                    position.X -= overflowX;
                    deltaX -= overflowX;
                }

                sprite.Translate(deltaX, 0);

                //Translate heart sprite energy under player
                for(int i=0; i< heartsEnergy.Length; i++)
                {
                    if(heartsEnergy[i] != null)
                        heartsEnergy[i].Translate(deltaX, 0);
                }

                for (int i = 0; i < bullets.Length; i++)
                {
                    if (bullets[i].IsAlive)
                    {
                        bullets[i].Update();
                       // Console.WriteLine("X: " + bullets[i].Position.X + " Y: " + bullets[i].Position.Y);

                        if (BarrierManager.Collides(bullets[i].Position, bullets[i].GetWidth()/2)) //perchè non /2
                        {
                           // Game.Stop();
                            bullets[i].IsAlive = false;
                        }

                        else if (EnemyMng.CollideWithBullet(bullets[i]))
                        {
                            bullets[i].IsAlive = false;
                            Game.AddScore(5);
                        }
                    }
                }
            }
        }

        public void Draw()
        {
            if (isVisible)
            {
                sprite.Draw();
            }

            if (isAlive)
            {
                for (int i = 0; i < bullets.Length; i++)
                {
                    if (bullets[i].IsAlive) 
                        bullets[i].Draw();

                    //Vector2 pos = bullets[i].Position;
                    //GfxTools.DrawRect((int)pos.X, (int)pos.Y, bullets[i].Height, bullets[i].GetWidth()/2, new ColorRGB(0, 0, 255));
                }

                for(int i=0; i < heartsEnergy.Length; i++)
                {
                    if(heartsEnergy[i] != null)
                        heartsEnergy[i].Draw();
                }
            }
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void OnHit(AlienBullet bullet)
        {
            int damage = bullet.GetDamage();

            //update hearts sprites
            while(damage > 0)
            {
                if (--energy <= 0)
                {
                    isAlive = false;
                    isVisible = false;
                    break;
                }

                heartsEnergy[energy] = null;
                damage--;
            }

            Vector2 bulletPos = bullet.GetPosition;
            ExplosionManager.ActiveExplosion(new Vector2(bulletPos.X - width / 2, bulletPos.Y - height / 2));
        }

        //public void OnHit(Bullet bullet)
        //{
        //    int damage = bullet.GetDamage();

        //    energy -= damage;

        //    if (energy <= 0)
        //    {
        //        isAlive = false;
        //    }

        //    Vector2 cannonPos = bullet.Position;
        //    ExplosionManager.ActiveExplosion(new Vector2(cannonPos.X - width / 2, cannonPos.Y - height / 2));
        //}

        public bool IsAlive()
        {
            return isAlive;
        }

        public int GetRay()
        {
            return ray;
        }
    }
}
