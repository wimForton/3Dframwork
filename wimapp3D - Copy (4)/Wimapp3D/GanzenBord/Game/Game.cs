using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Ganzenbord
{
    class Game
    {
        public int CurrentPlayer { get; set; } = 0;
        public List<Player> MyPlayers { get; set; } = new List<Player>(); ///(size will be NumberOfPlayers)
        public List<iSquare> MyPlayField { get; set; } = new List<iSquare>();
        public List<string> OutputText { get; set; } = new List<string>();
        public List<Vector> OutputColors { get; set; } = new List<Vector>();
        public Game()
        {
            CreatePlayers();
            CreatePlayfield();
        }


        public void PlayTurn(string input)
        {
            int cheatNumber = 0;
            bool cheatPlease = false;
            cheatPlease = Int32.TryParse(input, out cheatNumber);
            int playerIndex = CurrentPlayer % MyPlayers.Count;
            if (cheatPlease)
            {
                MyPlayers[playerIndex].position = cheatNumber;
                MyPlayers[playerIndex].turnsWaitedAtThisSquare = 0;
                MyPlayField[cheatNumber].Actions(MyPlayers[playerIndex], this);
                GameOutput($"Cheated with number {cheatNumber}", Vector.setNew(1,0,0));
            }
            else
            {
                MyPlayField[MyPlayers[playerIndex].position].Actions(MyPlayers[playerIndex], this);
            }
            CurrentPlayer++;
        }
        public void GameOutput(string inString, Vector inRgb)
        {
            OutputText.Add(inString);
            OutputColors.Add(inRgb);
        }
        private void CreatePlayers()
        {
            int howManyPlayers = 0;
            //bool isValid = false;
            string name = "P";
            string avatar = "Default";
            //do///////////////////////////////////////////////  method maken
            //{
            //    Console.WriteLine("How Many Players? (Max 4)");
            //    isValid = Int32.TryParse(Console.ReadLine(), out howManyPlayers);
            //    if (howManyPlayers < 1 || howManyPlayers > 4) isValid = false;
            //} while (!isValid);
            howManyPlayers = 4;
            for (int i = 0; i < howManyPlayers; i++)
            {
                Console.WriteLine($"player number {i + 1} name:");
                name = Console.ReadLine();
                //Console.WriteLine($"player number {i + 1} avatar:");
                //avatar = Console.ReadLine();
                Player inPlayer = new Player(name, avatar, i);
                MyPlayers.Add(inPlayer);
            }
        }
        private void CreatePlayfield()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(1, "SquareBridge");
            dictionary.Add(2, "SquareDeath");
            dictionary.Add(3, "SquareDefault");
            dictionary.Add(4, "SquareEnd");
            dictionary.Add(5, "SquareGans");
            dictionary.Add(6, "SquareInn");
            dictionary.Add(7, "SquareMaze");
            dictionary.Add(8, "SquarePrison");
            dictionary.Add(9, "SquareStart");
            dictionary.Add(10, "SquareWell");
            int[] playFieldArray = {9,3,3,3,3,5,1,3,3,5,3,3,3,3,5,3,3,3,5,6,3,3,3,5,3,3,3,5,3,3,3,10,5,3,3,3,5,3,3,3,3,5,7,3,3,5,3,3,3,3,5,3,8,3,5,3,3,3,2,5,3,3,3,4 };
            for (int i = 0; i < playFieldArray.Length; i++)
            {
                string squareType = "";
                dictionary.TryGetValue(playFieldArray[i], out squareType);
                MyPlayField.Add(SquareFactory.Build(squareType, i));
            }
        }

    }
}

