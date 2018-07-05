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
    class Ship
    {
        #region CONST DECLARATION
        private const string SHIP_UP_FILE = "Assets/Ship/ship_on.png";
        private const string SHIP_UP_LEFT_FILE = "Assets/Ship/ship_on_left.png";
        private const string SHIP_UP_RIGHT_FILE = "Assets/Ship/ship_on_right.png";
        private const string SHIP_DOWN_FILE = "Assets/Ship/ship_off.png";
        private const string SHIP_DOWN_LEFT_FILE = "Assets/Ship/ship_off_left.png";
        private const string SHIP_DOWN_RIGHT_FILE = "Assets/Ship/ship_off_right.png";
        private const float DEFAULT_RESISTANCE = 8f;
        #endregion

        private SpriteObj spriteUp, spriteDown, spriteUp_Left, spriteUp_Right, spriteDown_Left, spriteDown_Right;
        private SpriteObj currentSprite;
        private Vector2 position;
        private Vector2 velocity;
        private Vector2 force;
        private int width;
        private int height;
        private float ray;
        private bool isGrounded;
        private float resistance;
        private bool isAlive, isVisible;
        private Explosion explosionDie;

        public bool IsGrounded { get { return isGrounded; } }
        public Vector2 Position { get { return position; } }
        public float Ray { get { return ray; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public bool IsAlive { get { return isAlive; } }
        public bool IsVisible { get { return isVisible; } }

        public Ship(int x, int y, float xForce, float yForce, float resistance = DEFAULT_RESISTANCE) 
        {
            position = new Vector2(x, y);
            CreateSprites();
            currentSprite = spriteDown;
            width = currentSprite.Width;
            height = currentSprite.Height;
            TranslateSprites(-width / 2, -height / 2);
            ray = width / 2;
            force = new Vector2(xForce, yForce);
            velocity = new Vector2(0, 0);
            explosionDie = new Explosion();
            this.resistance = yForce * resistance;
            isGrounded = false;
            isAlive = true;
            isVisible = true;
        }

        public Ship(Vector2 position, Vector2 force, float resistance= DEFAULT_RESISTANCE) : 
            this((int) position.X, (int) position.Y, force.X, force.Y, resistance)
        {

        }

        public void Input()
        {
            bool upPressed = false;

            if (Game.Win.GetKey(KeyCode.Up))
            {
                upPressed = true;
                currentSprite = spriteUp;
                velocity.Y -= force.Y;
                //velocity.Y -= force.Y * Game.DeltaTime;
            }
            else
            {
                currentSprite = spriteDown;
            }

            if (Game.Win.GetKey(KeyCode.Right) && !isGrounded)
            {
                //velocity.X += force.X * Game.DeltaTime;
                velocity.X += force.X;
                if (upPressed)
                    currentSprite = spriteUp_Right;
                else
                    currentSprite = spriteDown_Right;
            }
            else if (Game.Win.GetKey(KeyCode.Left) && !isGrounded)
            {
                //velocity.X -= force.X * Game.DeltaTime;
                velocity.X -= force.X;
                if (upPressed)
                    currentSprite = spriteUp_Left;
                else
                    currentSprite = spriteDown_Left;
            }
        }

        public void Update()
        {
            if (IsAlive)
            {
                if (!isGrounded)
                {
                    velocity.Y += Game.Gravity;
                    //velocity.Y += Game.Gravity * Game.DeltaTime;

                    if (velocity.X > 0)
                    {
                        velocity.X -= Game.FrictionX;
                        //velocity.X -= Game.FrictionX * Game.DeltaTime;
                        if (velocity.X <= 0)
                            velocity.X = 0;
                    }
                    else if (velocity.X < 0)
                    {
                        velocity.X += Game.FrictionX;
                        //velocity.X += Game.FrictionX * Game.DeltaTime;
                        if (velocity.X >= 0)
                            velocity.X = 0;
                    }
                }

                //prova senza delta time float deltaY = velocity.Y;
                //float deltaY = velocity.Y * Game.DeltaTime;
                //float deltaX = velocity.X * Game.DeltaTime;
                float deltaY = velocity.Y * Game.DeltaTime;
                float deltaX = velocity.X * Game.DeltaTime;

                position.Y += deltaY;
                position.X += deltaX;

                TranslateSprites(deltaX, deltaY);

                if (position.X - ray < 0 || position.X + ray> Game.Win.width || position.Y - height/2 < 0)
                {
                    Die();
                    return;
                }

                if (AsteroidsManager.Collides(position, ray))
                {
                    Die();
                    return;
                }

                if (GroundManager.Collides(position, height / 2, ref isGrounded))
                {
                    if (!IsGrounded)
                    {
                        Die();
                        return;
                    }
                    else if (isGrounded && velocity.Y != 0)
                    {
                        if (velocity.GetLength() >= resistance)
                        {
                            Die();
                            Console.WriteLine("si");
                            return;
                        }
                        else
                            velocity.Y = velocity.X = 0f;
                    }
                }
            }
            else if(isVisible)
            {
                if (!explosionDie.Update())
                    isVisible = false;
            }
        }

        public void Die()
        {
            isAlive = false;
            explosionDie.Position = new Vector2(position.X - width / 2, position.Y - height / 2);
        }

        public void Draw()
        {
           // GfxTools.DrawRect((int) position.X - width/2, (int) position.Y - height/2, height, width, new ColorRGB(0, 0, 255));
            //pivot GfxTools.DrawRect((int)position.X, (int)position.Y, height, width, new ColorRGB(255, 0, 0));

            if (isAlive)
            {
                currentSprite.Draw();
            }
            else if(isVisible)
            {
                explosionDie.Draw();
            }
        }

        private void CreateSprites()
        {
            spriteUp = new SpriteObj(SHIP_UP_FILE, position);
            spriteUp_Left = new SpriteObj(SHIP_UP_LEFT_FILE, position);
            spriteUp_Right = new SpriteObj(SHIP_UP_RIGHT_FILE, position);
            spriteDown = new SpriteObj(SHIP_DOWN_FILE, position);
            spriteDown_Left = new SpriteObj(SHIP_DOWN_LEFT_FILE, position);
            spriteDown_Right = new SpriteObj(SHIP_DOWN_RIGHT_FILE, position);
            
        }

        private void TranslateSprites(float x, float y)
        {
            spriteUp.Translate(x, y);
            spriteUp_Left.Translate(x, y);
            spriteUp_Right.Translate(x, y);
            spriteDown.Translate(x, y);
            spriteDown_Left.Translate(x, y);
            spriteDown_Right.Translate(x, y);
        }
    }
}
