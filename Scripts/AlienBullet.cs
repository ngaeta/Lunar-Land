using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawExercise;


namespace SquareInvadersExercise
{
    class AlienBullet
    {
        Vector2 velocity;
        SpriteObj sprite;
        Animation2 animation;
        private Vector2 Position;
        public bool IsAlive;
        private int damage;

        public Vector2 GetPosition { get { return Position; } }
        public int Width { get { return sprite.Width; } }
        public int Height { get { return sprite.Height; } }

        public AlienBullet(int damage, Vector2 velocity, string[] AnimationString)
        {
            Position = new Vector2(0, 0);
            this.velocity = velocity;
            sprite = new SpriteObj(AnimationString[0], Position);
            animation = new Animation2(AnimationString, 2, sprite, true);
            this.damage = damage;
        }

        public void Update()
        {
            animation.Update();

            float deltaY = velocity.Y * GfxTools.Win.deltaTime;
            Position.X += velocity.X * GfxTools.Win.deltaTime;
            Position.Y += deltaY;

            if (sprite != null)
                sprite.Translate(0,deltaY);

            if (Position.Y + Height / 2 < 0 || Position.Y - Height / 2 >= GfxTools.Win.height)
            {
                IsAlive = false;
            }
        }

        public void Draw()
        {
            GfxTools.DrawSprite(sprite.GetSprite(), (int)(Position.X - Width / 2), (int)(Position.Y - Height / 2));
        }

        public void Shoot(Vector2 startPos)
        {
            Position = startPos;
            IsAlive = true;
            sprite.Position = startPos;
        }

        public bool Collides(Vector2 center, float ray)
        {
            Vector2 dist = Position.Sub(center);
            return (dist.GetLength() <= Width / 2 + ray);
        }

        public int GetDamage()
        {
            return damage;
        }
    }
}
