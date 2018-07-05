using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;


namespace DrawExercise
{
    static class GfxTools
    {
        private static Window window;

        public static void Init(Window win)
        {
            window = win;
        }

        public static Window Win
        {
            get
            {
                return window;
            }
        }

        private static bool DrawPixel(int x, int y, byte r, byte g, byte b)
        {
            //quando arriva alla width dello schermo continua sotto finchè non arriva alla fine della finsetra ed esce dall'array lanciando un eccezione
            if (x < 0 || x >= window.width || y < 0 || y >= window.height)
            {
                return false;
            }

            //per accedere all'indice dell'array attraverso le coordinate x, y. *3 perchè abbiamo 3 byte x ogni pixel
            window.bitmap[((y * window.width) + x) * 3] = r;
            window.bitmap[(((y * window.width) + x) * 3) + 1] = g;
            window.bitmap[(((y * window.width) + x) * 3) + 2] = b;
            return true;
        }

        private static bool DrawPixel(int x, int y, ColorRGB color)
        {

            if (x < 0 || x >= window.width || y < 0 || y >= window.height)
            {
                return false;
            }

            window.bitmap[((y * window.width) + x) * 3] = color.Red;
            window.bitmap[(((y * window.width) + x) * 3) + 1] = color.Green;
            window.bitmap[(((y * window.width) + x) * 3) + 2] = color.Blue;
            return true;
        }

        private static void DrawHorizontalLine(int x, int y, int lenght, byte r, byte g, byte b)
        {
            for (int i = x; i < x + lenght; i++)    // o x + lenght
            {
                DrawPixel(i, y, r, g, b);
            }
        }

        private static void DrawHorizontalLine(int x, int y, int lenght, ColorRGB color)
        {
            for (int i = x; i < x + lenght; i++)    // o x + lenght
            {
                DrawPixel( i, y, color.Red, color.Green, color.Blue);
            }
        }

        private static void DrawVerticalLine(int x, int y, int height, byte r, byte g, byte b)
        {
            for (int i = y; i < y + height; i++)    // o x + lenght
            {
                DrawPixel( x, i, r, g, b);
            }
        }

        private static void DrawVerticalLine(int x, int y, int height, ColorRGB color)
        {
            for (int i = y; i < y + height; i++)    // o x + lenght
            {
                DrawPixel( x, i, color.Red, color.Green, color.Blue);
            }
        }

        public static void DrawRect(Rectangle rect, byte r, byte g, byte b, int tickness = 1)
        {
            int x = (int) rect.GetX();
            int y = (int) rect.GetY();
            int width = rect.GetWidth();
            int height = rect.GetHeight();

            for (int i = 0; i < tickness; i++)
            {
                DrawHorizontalLine( x, y + i, width, r, g, b);
                DrawVerticalLine(x + i, y, height, r, g, b);
                DrawHorizontalLine( x, y + height - i, width, r, g, b);
                DrawVerticalLine( x + width - i, y, height, r, g, b);
            }
        }

        public static void DrawRect(Rectangle rect, ColorRGB colorBorder, int tickness = 1)
        {
            int x = (int)rect.GetX();
            int y = (int)rect.GetY();
            int width = rect.GetWidth();
            int height = rect.GetHeight();

            for (int i = 0; i < tickness; i++)
            {
                DrawHorizontalLine( x, y + i, width, colorBorder.Red, colorBorder.Green, colorBorder.Blue);
                DrawVerticalLine( x + i, y, height, colorBorder.Red, colorBorder.Green, colorBorder.Blue);
                DrawHorizontalLine( x, y + height - i, width, colorBorder.Red, colorBorder.Green, colorBorder.Blue);
                DrawVerticalLine( x + width - i, y, height, colorBorder.Red, colorBorder.Green, colorBorder.Blue);
            }
        }

        public static void DrawRect(int x, int y, int height, int width, ColorRGB colorBorder, int tickness = 1)
        {

            for (int i = 0; i < tickness; i++)
            {
                DrawHorizontalLine(x, y + i, width, colorBorder.Red, colorBorder.Green, colorBorder.Blue);
                DrawVerticalLine(x + i, y, height, colorBorder.Red, colorBorder.Green, colorBorder.Blue);
                DrawHorizontalLine(x, y + height - i, width, colorBorder.Red, colorBorder.Green, colorBorder.Blue);
                DrawVerticalLine(x + width - i, y, height, colorBorder.Red, colorBorder.Green, colorBorder.Blue);
            }
        }

        public static void DrawSolidRect(Rectangle rect, byte r, byte g, byte b, ColorRGB colorBorder, int tickness = 1)
        {
            DrawRect(rect, r, g, b, tickness);

            int x = (int)rect.GetX();
            int y = (int)rect.GetY();
            int width = rect.GetWidth();
            int height = rect.GetHeight();

            for (int i = y + 1; i < y + height; i++)
            {
                DrawHorizontalLine(x + 1, i, width - 1, colorBorder.Red, colorBorder.Green, colorBorder.Blue);
            }
        }

        public static void DrawSolidRect(Rectangle rect, ColorRGB colorBackground, ColorRGB colorBorder, int tickness = 1)
        {
            DrawRect(rect, colorBackground, tickness);

            int x = (int)rect.GetX();
            int y = (int)rect.GetY();
            int width = rect.GetWidth();
            int height = rect.GetHeight();

            for (int i = y + 1; i < y + height; i++)
            {
                DrawHorizontalLine(x + 1, i, width - 1, colorBorder.Red, colorBorder.Green, colorBorder.Blue);
            }

        }

        public static void DrawSolidRect(int x, int  y,  int height, int width, ColorRGB colorBackground, ColorRGB colorBorder, int tickness = 1)
        {
            DrawRect(x, y, height, width, colorBackground, tickness);

            for (int i = y + 1; i < y + height; i++)
            {
                DrawHorizontalLine(x + 1, i, width - 1, colorBorder.Red, colorBorder.Green, colorBorder.Blue);
            }

        }

        public static void CleanScreen()
        {
            for(int i=0; i < window.bitmap.Length; i++)
            {
                window.bitmap[i] = 0;
            }
        }

        public static void DrawSprite(Sprite sprite, int x, int y)
        {
            for (int i = 0; i < sprite.height; i++)
            {
                for (int j = 0; j < sprite.width; j++)
                {
                    int tempX = x + j;
                    int tempY = y + i;

                    if (tempX < 0 || tempX >= Win.width || tempY < 0 || tempY >= Win.height)
                    {
                        continue;  //se il pixel dell'immagine non sta nella nostra finestra non lo disegniamo, lo perdiamo.
                    }

                    int spriteByteIndex = (i * sprite.width + j) * 4; // ogni pixel 4 byte RGBA
                    int spriteR = sprite.bitmap[spriteByteIndex];
                    int spriteG = sprite.bitmap[spriteByteIndex + 1];
                    int spriteB = sprite.bitmap[spriteByteIndex + 2];
                    int spriteA = sprite.bitmap[spriteByteIndex + 3];
                    float alpha = spriteA / 255f;

                    int windowByteIndex = (tempY * Win.width + tempX) * 3; //perdiamo il pixel trasparente

                    int winR = Win.bitmap[windowByteIndex]; //anche byte andava bene
                    int winG = Win.bitmap[windowByteIndex + 1];
                    int winB = Win.bitmap[windowByteIndex + 2];

                    Win.bitmap[windowByteIndex] = (byte)((spriteR * alpha) + (winR * (1 - alpha)));
                    Win.bitmap[windowByteIndex + 1] = (byte)((spriteG * alpha) + (winG * (1 - alpha)));
                    Win.bitmap[windowByteIndex + 2] = (byte)((spriteB * alpha) + (winB * (1 - alpha)));
                }
            }
        }
    }
}
