using Aiv.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawExercise;

namespace SquareInvadersExercise
{
    class Animation2
    {
        float fps;   //frame al secondo, frequenza aggiornamento animazione
        float fpsCounter;
        SpriteObj owner;
        Sprite[] sprites;
        string[] spriteAnimation;
        bool loop;
        bool isPlaying;

        public Sprite[] Sprites {get { return sprites; } }

        int currentIndex;
        public int CurrentIndex { get { return currentIndex; } }

        public Animation2(string[] spriteAnimation, float fps, SpriteObj owner, bool loop)
        {
            this.fps = fps;
            this.owner = owner;
            this.spriteAnimation = spriteAnimation;
            this.loop = loop;
            isPlaying = true;
            sprites = new Sprite[spriteAnimation.Length];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = new Sprite(spriteAnimation[i]);
            }
        }

        public void Update()
        {
            fpsCounter -= GfxTools.Win.deltaTime;

            if (isPlaying && fpsCounter <= 0)
            {
                fpsCounter = 1 / fps;     //fps al secondo? cosa serve? 1/30 significa 30 animazioni in un secondo??? 1/2 fa due animazioni al secondo

                if (currentIndex >= sprites.Length)
                {
                    OnAnimationDeads();
                }

                owner.SetSprite(sprites[currentIndex++]);
            }

        }

        public void OnAnimationDeads()
        {
            if (loop)
            {
                currentIndex = 0;
                return;
            }

            Stop();
        }

        public void Start()
        {
            isPlaying = true;
        }

        public void Stop()
        {
            currentIndex = sprites.Length - 1;
            isPlaying = false;
        }

        public void Restart()
        {
            currentIndex = 0;
            isPlaying = true;
        }

    }
}
