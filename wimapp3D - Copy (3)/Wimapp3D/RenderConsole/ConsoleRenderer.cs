using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Ganzenbord
{
    class ConsoleRenderer
    {
        public ConsoleRenderer(int inWindowWidth, int inWindowHeight)
        {
            Console.WindowWidth = inWindowWidth;
            Console.WindowHeight = inWindowHeight;
        }
        public string Render(Game inGame)
        {
            string result = "";
            int yOffset = 0;
            Console.Clear();
            for (int i = 0; i < inGame.MyPlayField.Count; i++)
            {
                if(i < 32)
                {
                    Console.SetCursorPosition(0, i + yOffset);
                }
                else
                {
                    Console.SetCursorPosition(35, i - 32 + yOffset);
                }
                Console.ForegroundColor = ColorConvert.RgbToConsole(inGame.MyPlayField[i].Color * 255);
                Console.Write(inGame.MyPlayField[i].Name);
            }
            for (int i = 0; i < inGame.MyPlayers.Count; i++)
            {
                int pos = inGame.MyPlayers[i].position;
                if (pos < 32)
                {
                    Console.SetCursorPosition(10 + i * 6, pos + yOffset);
                }
                else
                {
                    Console.SetCursorPosition(45 + i * 6, pos - 32 + yOffset);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(inGame.MyPlayers[i].Name);
            }
            
            Console.SetCursorPosition(0, 34);
            for (int i = 0; i < inGame.OutputText.Count; i++)
            {
                Console.ForegroundColor = ColorConvert.RgbToConsole(inGame.OutputColors[i] * 255);//inGame.OutputColors[i]);//ConsoleColor.Blue;
                Console.WriteLine(inGame.OutputText[i]);

            }
            Console.ForegroundColor = ConsoleColor.White;
            inGame.OutputColors.Clear();
            inGame.OutputText.Clear();
            Console.WriteLine("");
            Console.WriteLine("press enter for next player, q to quit");
            //result = Console.ReadLine();
            //Console.SetCursorPosition(0, 35);
            return result;
        }
    }
}
