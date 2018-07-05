using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace DrawExercise
{
    class Rectangle
    {
        private Vector2 position;
        private int height;
        private int width;
        private ColorRGB colorBackground, colorBorder;
        private bool isAlive;

        public Rectangle (Vector2 pos, int height, int width)
        {
            position = pos;
            this.height = height;
            this.width = width;
            isAlive = true;
            colorBackground.Red = colorBackground.Blue = colorBackground.Green = 255;
            colorBorder = colorBackground;
        }

        public Rectangle(float x, float y, int height, int width)
        {
            position.X = x;
            position.Y = y;
            this.height = height;
            this.width = width;
            isAlive = true;
            colorBackground.Red = colorBackground.Blue = colorBackground.Green = 255;
            colorBorder = colorBackground;
        }

        public void SetColor(ColorRGB colorBackground, ColorRGB colorBorder)
        {
            this.colorBackground = colorBackground;
            this.colorBorder = colorBorder;
        }

        public void Translate(float xTranslate, float yTranslate)
        {
            position.X += xTranslate;
            position.Y += yTranslate;
        }

        public void SetPosition(Vector2 pos)
        {
            position.X = pos.X;
            position.Y = pos.Y;
        }
        
        public bool IsAlive()
        {
            return isAlive;
        }

        public void SetAlive(bool value)
        {
            isAlive = value;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public float GetX()
        {
            return position.X;
        }

        public float GetY()
        {
            return position.Y;
        }

        public void SetWidth(int width)
        {
            this.width = width;
        }

        public int GetWidth()
        {
            return width;
        }

        public void SetHeight(int height)
        {
            this.height=height;
        }

        public int GetHeight()
        {
            return height;
        }

        public void Draw()
        {
            if (isAlive)
            {
                GfxTools.DrawSolidRect((int) position.X, (int) position.Y, height, width, colorBackground, colorBorder);
            }
        }

        public string PrintStats()
        {
            return "(" + position.X + ", " + position.Y + ") -> (" + (position.X + width) + ", " + (position.Y + height) +")";
        }     
    }
}
