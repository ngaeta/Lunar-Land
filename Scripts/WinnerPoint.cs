using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;
using DrawExercise;
using SquareInvadersExercise;

namespace LunarLand
{
    class WinnerPoint
    {
        const string TARGET_FILE = "Assets/target.png";

        private SpriteObj target;
        private Vector2 position;
        private int width;
        private int height;

        public Vector2 Position { get { return position; } }

        public WinnerPoint(int x, int y)
        {
            position = new Vector2(x, y);
            target = new SpriteObj(TARGET_FILE, position);
            width = target.Width;
            height = target.Height;
        }

        public WinnerPoint(Vector2 vector) : this((int) vector.X, (int) vector.Y)
        {
            
        }

        public bool Collides(Vector2 center, float ray)
        {
            int offset = 6;

            if(center.X - ray >= position.X - offset && center.X + ray <= position.X + width + offset)
            {
                return true;
            }

            return false;
        }

        public void Draw()
        {
            target.Draw();
        }
    }
}
