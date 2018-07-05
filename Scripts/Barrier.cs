﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawExercise;

namespace SquareInvadersExercise
{
    class Barrier
    {
        const string BARRIER_FILE = "Assets/barrier.png";

        private SpriteObj sprite;
        private Vector2 position;
        public int Width { get { return sprite.Width; } }
        public int Height { get { return sprite.Height; } }

        public Barrier(int x, int y)
        {
            sprite = new SpriteObj(BARRIER_FILE, x, y);
            position = new Vector2(x, y);
            sprite.Translate(-Width/2, -Height/2);
        }

        public void Draw()
        {
            sprite.Draw();
            //Vector2 pixelPos = new Vector2(position.X - Width / 2, position.Y - Height / 2);
            //GfxTools.DrawRect((int) pixelPos.X, (int) pixelPos.Y, 5, 5, new ColorRGB(0, 0, 255));
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
            float explosionRay = 15;
            bool collision = false;
            Vector2 dist = position.Sub(center);
            if (dist.GetLength() <= Width / 2 + ray)//bounding circle collision
            {
                if (PixelCollides(center, ray))//collision between bullet and sprite pixels
                {
                    
                    collision = true;
                    //pixel erasing
                    PixelCollides(center, explosionRay, true);
                }

            }
            return collision;
        }
    }
}
