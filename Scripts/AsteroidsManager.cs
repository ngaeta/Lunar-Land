using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawExercise;
using SquareInvadersExercise;

namespace LunarLand
{
    static class AsteroidsManager
    {
        private static Asteroid[] asteroids;

        public static void Init(int numberAsteroids, int xOffset = 0, int yOffset = 0)
        {
            int winWidth = GfxTools.Win.width;
            int winHeight = GfxTools.Win.height;
            asteroids = new Asteroid[numberAsteroids];
            Vector2 prevPos = new Vector2(xOffset, yOffset);
            Ship player = Game.Player;
            string fileName = "Assets/Asteroids/asteroids_0";
            int numAstInColumn = 0;

            for (int i=0; i < numberAsteroids; i++)
            {

                //posizioniamo il primo all'inizio per evitare un percorso semplice
                if (i==0)
                {
                    asteroids[i] = new Asteroid(fileName + RandomGenerator.GetRandom(1, 7) + ".png", (int)prevPos.X + player.Width*2 + RandomGenerator.GetRandom(0, player.Width), (int)prevPos.Y);
                    numAstInColumn = RandomGenerator.GetRandom(2, 4);
                }

                //posizioniamo l ultimo asteroide un po sopra il winner point, if ad hoc, modificare per renderlo relativo al winner point
                else if(i==numberAsteroids-1)
                {    
                    asteroids[i] = new Asteroid(fileName + RandomGenerator.GetRandom(1, 7) + ".png",  winWidth - 100, winHeight/2);
                }

                else
                {
                    if (numAstInColumn == 0)
                    {
                        prevPos.X += player.Width * 2 + RandomGenerator.GetRandom(0, player.Width);
                        prevPos.Y = yOffset;
                        numAstInColumn = RandomGenerator.GetRandom(2, 4);
                    }

                    prevPos.Y -= player.Height * 2 - RandomGenerator.GetRandom(0, player.Height);
                    int numAsteroids = RandomGenerator.GetRandom(1, 7);
                    asteroids[i] = new Asteroid(fileName + numAsteroids + ".png", (int)prevPos.X + player.Width + 
                        RandomGenerator.GetRandom(0, player.Width), (int)prevPos.Y);

                    numAstInColumn--;
                }

                asteroids[i].Translate(asteroids[i].Width / 2, -asteroids[i].Height / 2);
            }
        }

        public static void Draw()
        {
            for(int i=0; i < asteroids.Length; i++)
            {
                asteroids[i].Draw();
            }
        }

        public static bool Collides(Vector2 center, float ray)
        {
            for (int i = 0; i < asteroids.Length; i++)
            {
                if (asteroids[i].Collides(center, ray))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
