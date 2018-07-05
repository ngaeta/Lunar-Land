using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquareInvadersExercise;
using DrawExercise;

namespace LunarLand
{
    class Asteroid
    {
        private SpriteObj sprite;
        private Vector2 position;

        public Vector2 Position { get { return position; } }
        public int Height { get { return sprite.Height; } }
        public int Width { get { return sprite.Width; } }

        public Asteroid(string fileName, int x, int y)
        {
            sprite = new SpriteObj(fileName, x, y);
            position = new Vector2(x, y);
            sprite.Translate(-Width / 2, -Height / 2);
        }

        public Asteroid(string fileName, Vector2 pos) : this(fileName, (int) pos.X, (int) pos.Y)
        {
            
        }

        public void Translate(float x, float y)
        {
            position.X += x;
            position.Y += y;
            sprite.Translate(x, y);
        }

        public void Draw()
        {
            sprite.Draw();
            //GfxTools.DrawRect((int)position.X - Width / 2, (int)position.Y - Height / 2, Height, Width, new ColorRGB(0, 0, 255));
            //pivot GfxTools.DrawRect((int)position.X, (int)position.Y, Height, Width, new ColorRGB(255, 0, 0));
        }

        private bool PixelCollides(Vector2 center, float ray, bool erase = false)
        {
            bool collision = false;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Vector2 pixelPos = new Vector2(position.X - (Width / 2) + x, position.Y - (Height / 2) + y);
                    float pixToBulletDist = pixelPos.Sub(center).GetLength();

                    if (pixToBulletDist <= ray)
                    {
                        //obtains index of current pixel's alpha
                        int pixelAlfaIndex = (y * Width + x) * 4 + 3;

                        if (erase)
                        {//must erase pixels inside the explosion's circle
                            sprite.GetSprite().bitmap[pixelAlfaIndex] = 0;
                            collision = true;
                        }
                        else
                        {
                            if (sprite.GetSprite().bitmap[pixelAlfaIndex] != 0)
                                return true;
                        }
                    }
                }
            }
            return collision;
        }

        public bool Collides(Vector2 center, float ray)
        {
            float explosionRay = 50;
            bool collision = false;
            Vector2 dist = position.Sub(center);
            if (dist.GetLength() <= Width / 2 + ray)//bounding circle collision
            {
                if (PixelCollides(center, ray))//collision between bullet and sprite pixels
                {
                    //Console.WriteLine("collisione");
                    collision = true;
                    //pixel erasing
                    PixelCollides(center, explosionRay, true);
                }

            }
            return collision;
        }
    }
}
