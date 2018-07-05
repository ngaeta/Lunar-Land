using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;
using SquareInvadersExercise;
using DrawExercise;

namespace LunarLand
{
    class Ground
    {
        const string GROUND_FILE = "Assets/ground_Horizontal.png";

        private SpriteObj sprite;
        private Vector2 position;

        public Vector2 Position { get { return position; } }
        public int Height { get { return sprite.Height; } }

        public Ground(int x, int y)
        {
            sprite = new SpriteObj(GROUND_FILE, x, y);
            position = new Vector2(x, y);
        }
        
        public void Draw()
        {
            sprite.Draw();
        }

        public void Translate(Vector2 vector)
        {
            position.X += vector.X;
            position.Y += vector.Y;
            sprite.Translate(vector);
        }   

        public bool Collides(Vector2 positionObj, float ray)
        {
            if (positionObj.Y + ray >= position.Y)
            { 
                return true;
            }

            return false;
        }
    }
}
