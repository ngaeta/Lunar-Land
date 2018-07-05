
using Aiv.Draw;
using DrawExercise;
using SquareInvadersExercise;

namespace LunarLand
{
    static class Game
    {
        const string WINNER_FILE = "Assets/win.png";
        const string GAME_OVER_FILE = "Assets/game_over.png";

        private static Window window;
        private static float gravity;
        private static float frictionX;
        private static Ship playerShip;
        private static bool isPlaying;
        private static SpriteLabel winnerText, gameOverText;
        private static float timeToDrawLabel;
        private static bool canInput;

        public static Ship Player { get { return playerShip; } }
        public static float DeltaTime { get { return window.deltaTime; } }
        public static Window Win { get { return window; } }
        public static float Gravity { get { return gravity; } }
        public static float FrictionX { get { return frictionX; } }

        static Game()
        {
            window = new Window(800, 600, "Lunar land", PixelFormat.RGB);
            gravity = 3f;
            frictionX = 1f;
            timeToDrawLabel = 2f;
            canInput = true;
            isPlaying = false;
        }

        public static void Play()
        {
            isPlaying = true;
            winnerText = new SpriteLabel(window.width / 2, window.height / 2, WINNER_FILE);
            gameOverText = new SpriteLabel(window.width / 2, window.height / 2, GAME_OVER_FILE);
            GfxTools.Init(window);
            GroundManager.Init();
            Background background = new Background(250, 5);
            playerShip = new Ship(50, window.height - GroundManager.GetGroundHeight() - 24, frictionX  * 1.8f, gravity * 2f);
            AsteroidsManager.Init(12, 0, (int)(GroundManager.GetGroundPosition().Y - playerShip.Height * 2));

            while (isPlaying)
            {
                if (Win.GetKey(KeyCode.Esc) || !Win.opened)
                    return;

                GfxTools.CleanScreen();

                if (canInput)
                {
                    playerShip.Input();
                }

                playerShip.Update();

                background.Draw();
                playerShip.Draw();
                AsteroidsManager.Draw();
                GroundManager.Draw();
                window.Blit();

                CheckGameStats();
            }

            //mettere if player è morto break, e fuori da isplaying usare Wait o altro metodo per disegnare il risultato
        }

        private static void CheckGameStats()
        {
            SpriteLabel labelToDraw = null;

            if (!playerShip.IsVisible)
            {
                canInput = false;
                labelToDraw = gameOverText;
            }

            else if (playerShip.IsGrounded && GroundManager.InWinnerPoint(playerShip.Position, playerShip.Ray))
            {
                canInput = false;
                labelToDraw = winnerText;
            }

            if (labelToDraw != null)
            {
                if (timeToDrawLabel <= 0)
                {
                    canInput = false;
                    labelToDraw.Draw();
                    window.Blit();
                    isPlaying = false;
                    System.Threading.Thread.Sleep(3500);
                }
                else
                    timeToDrawLabel -= GfxTools.Win.deltaTime;
            }
        }
    }
}
