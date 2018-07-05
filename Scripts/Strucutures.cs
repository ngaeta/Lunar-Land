using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawExercise
{
    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2 Sub(Vector2 vec)
        {
            return new Vector2(X - vec.X, Y - vec.Y);
        }

        public Vector2 Add(Vector2 vec)
        {
            return new Vector2(X + vec.X, Y + vec.Y);
        }

        public float GetLength()
        {
            return (float) Math.Sqrt(X * X + Y * Y);  //teorema di pitagora per trovare il modulo di un vettore
        }
    }

    public struct ColorRGB
    {
        public byte Red;
        public byte Green;
        public byte Blue;

        public ColorRGB(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
    }

}
