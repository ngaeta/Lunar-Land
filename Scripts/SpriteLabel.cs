using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;
using DrawExercise;

namespace SquareInvadersExercise
{
    class SpriteLabel
    {
        private Sprite sprite;
        private Vector2 position;

        public Vector2 Position
        {
            set
            {
                position = value;
            }
        }

        public SpriteLabel(int x, int y, string fileName)
        {
            sprite = new Sprite(fileName);
            position = new Vector2(x - sprite.width/2, y - sprite.width/2);
        }

        public SpriteLabel(Vector2 position, string fileName) : this((int) position.X,(int) position.Y, fileName)
        {
           
        }

        public void Draw()
        {
            GfxTools.DrawSprite(sprite, (int) position.X, (int) position.Y);
        }
    }
}
