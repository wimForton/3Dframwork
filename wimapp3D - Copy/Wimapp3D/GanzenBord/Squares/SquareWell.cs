using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Ganzenbord
{
    class SquareWell : Square, iSquare
    {
        private List<int> Guests { get; set; } = new List<int>();
        public override string Name { get; set; } = "Well";
        public override Vector Color { get; set; } = Vector.setNew(1, 0.5, 1);
        public SquareWell(int pos) : base(pos)
        {
            SquarePos = pos;
        }
        public override void Actions(Player inPlayer, Game inGame)
        {
            if (inPlayer.turnsWaitedAtThisSquare == 0)
            {
                inGame.GameOutput($"Player has arrived at {Name} square", Vector.setNew(0, 1, 1));
                inGame.GameOutput($"Hmm, there's a well, let's see if nobody is in there", Vector.setNew(1, 1, 0));
                Guests.Add(inPlayer.Id);
                inPlayer.turnsWaitedAtThisSquare++;
            }
            else if (Guests.IndexOf(inPlayer.Id) + 1 < Guests.Count)
            {
                inGame.GameOutput($"{inPlayer.Name} is free to go!", Vector.setNew(1, 1,0));
                Guests.Remove(inPlayer.Id);
                inPlayer.ThrowDice(inGame);
                inGame.GameOutput($"Player {inPlayer.Name} has thrown {inPlayer.Dice[0]} and {inPlayer.Dice[1]} and moves to position {inPlayer.position}, {inGame.MyPlayField[inPlayer.position].Name}", Vector.setNew(0, 1, 1));
                inPlayer.turnsWaitedAtThisSquare = 0;
                inGame.MyPlayField[inPlayer.position].Actions(inPlayer, inGame);
            }
            else
            {
                inPlayer.turnsWaitedAtThisSquare++;
                inGame.GameOutput($"{inPlayer.Name} is stuck here allready {inPlayer.turnsWaitedAtThisSquare} times, nobody to save you?", Vector.setNew(1, 1, 0));
            }
        }

    }
}

