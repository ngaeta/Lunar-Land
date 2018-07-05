using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawExercise;
using SquareInvadersExercise;

namespace LunarLand
{
    class Background
    {
        const int NUMBER_PLANET = 1;

        private Rectangle[] stairs;
        private ColorRGB color;
        private SpriteObj[] planets;

        public Background(int numberStairs, int maxSize, bool withPlanets = true)
        {
            stairs = new Rectangle[numberStairs];

            for(int i=0; i < numberStairs; i++)
            {
                int randomSize = (int)RandomGenerator.GetRandom(0, maxSize);

                stairs[i] = new Rectangle((int)RandomGenerator.GetRandom(0, Game.Win.width),
                                          (int)RandomGenerator.GetRandom(0, Game.Win.height),
                                          randomSize, randomSize);
            }

            if (withPlanets)
            {
                string planetFile = "Assets/planets/planet_0";
                planets = new SpriteObj[NUMBER_PLANET];

                //un solo pianeta perchè il frame rate rallentava troppo
                for (int i = 0; i < NUMBER_PLANET; i++)
                {
                    planets[i] = new SpriteObj(planetFile + RandomGenerator.GetRandom(1, 10) + ".png", Game.Win.width / 2, Game.Win.height / 4);
                    planets[i].Translate(-planets[i].Width / 2, -planets[i].Height / 2);
                }
            }

            color = new ColorRGB(255, 255, 255);
        }

        public void Draw()
        {
            for(int i=0; i < stairs.Length; i++)
            {
                GfxTools.DrawSolidRect(stairs[i], color, color);
            }

            if (planets != null)
            {
                for (int i = 0; i < planets.Length; i++)
                {
                    planets[i].Draw();
                }
            }
        }
    }
}
