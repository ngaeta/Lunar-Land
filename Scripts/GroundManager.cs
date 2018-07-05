using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquareInvadersExercise;
using DrawExercise;

namespace LunarLand
{
    static class GroundManager
    {
        const string BACKGROUND_NAME = "Assets/background.png";

        private static WinnerPoint winnerPoint;
        private static Ground ground;
        private static SpriteObj background;
        private static Mountain[] mountain;

        public static void Init()
        {
            ground = new Ground(0, 0);
            ground.Translate(new Vector2(0, Game.Win.height - ground.Height));
            background = new SpriteObj(BACKGROUND_NAME, 0, 0);
            winnerPoint = new WinnerPoint((int) ground.Position.X + Game.Win.width - 100, (int) ground.Position.Y);

            mountain = new Mountain[4];
            mountain[0] = new Mountain((int)Game.Win.width / 2 + 50, (int)ground.Position.Y - 30);
            mountain[1] = new Mountain(210, (int)ground.Position.Y - 40, "Assets/Mountains/mountain_02.png");
            mountain[2] = new Mountain(250, (int)ground.Position.Y);
            mountain[3] = new Mountain((int)winnerPoint.Position.X - 150, (int)ground.Position.Y - 30, "Assets/Mountains/mountain_03.png");
        }

        public static void Draw()
        {
            ground.Draw();
            winnerPoint.Draw();

            for(int i=0; i < mountain.Length; i++)
            {
                mountain[i].Draw();
            }
        }

        public static bool Collides(Vector2 center, float ray, ref bool withGround)
        {
            if(ground.Collides(center, ray))
            {
                //Console.WriteLine("Collision with ground");
                withGround = true;
                return true;
            }
            else
            {
                for(int i=0; i < mountain.Length; i++)
                {
                    if(mountain[i].Collides(center, ray))
                    {
                        //Console.WriteLine("Collision with Mountain");
                        withGround = false;
                        return true;
                    }
                }
            }

            withGround = false;
            return false;
        }

        public static bool InWinnerPoint(Vector2 collider, float ray)
        {
            return winnerPoint.Collides(collider, ray);
        }

        public static int GetGroundHeight()
        {
            return ground.Height;
        }

        public static Vector2 GetGroundPosition()
        {
            return ground.Position;
        }
    }
}
