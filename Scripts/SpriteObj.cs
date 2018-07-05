using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;
using DrawExercise;

namespace SquareInvadersExercise
{
    class SpriteObj
    {
        private Sprite sprite;
        private Vector2 position;

        public int Width { get { return sprite.width;  } }
        public int Height { get { return sprite.height; } }
        public Sprite Sprite { get { return sprite; } }
        public Vector2 Position
        {
            get { return position; }
            set { position = value;}
        }

        public SpriteObj(string fileName, int x = 0, int y=0)
        {
            sprite = new Sprite(fileName);
            position = new Vector2(x, y);
        }

        public SpriteObj(string fileName, Vector2 position) : this(fileName, (int) position.X, (int) position.Y)
        {
        }

        public SpriteObj (Sprite sprite)
        {
            this.sprite = sprite;
        }

        public void Translate(float offsetX, float offsetY)
        {
            position.X += offsetX;
            position.Y += offsetY;
        }

        public void Translate(Vector2 offset)
        {
            Translate(offset.X, offset.Y);
        }

        public void Draw()
        {
            GfxTools.DrawSprite(sprite, (int)position.X, (int)position.Y);
        }

        public Sprite GetSprite()
        {
            return sprite;
        }

        public void SetSprite(Sprite sprite)
        {
            this.sprite = sprite;
        }

        internal void SetPosition(Vector2 startPos)
        {
            throw new NotImplementedException();
        }
    }
}
