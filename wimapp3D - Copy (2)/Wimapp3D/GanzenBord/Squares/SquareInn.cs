using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Ganzenbord
{
    class SquareInn : Square, iSquare
    {
        public override string Name { get; set; } = "Inn";
        public override Vector Color { get; set; } = Vector.setNew(1, 1, 0);
        public SquareInn(int pos) : base(pos)
        {
            SquarePos = pos;
        }
        public override void Actions(Player inPlayer, Game inGame)
        {
            if (inPlayer.turnsWaitedAtThisSquare < 2)
            {
                inGame.GameOutput($"Player has arrived at {Name} square", Vector.setNew(0, 1, 1));
                inGame.GameOutput($"I'm stuck here for {1 - inPlayer.turnsWaitedAtThisSquare} more turns", Vector.setNew(0, 1, 1));
                inPlayer.turnsWaitedAtThisSquare++;
            }
            else
            {
                inPlayer.ThrowDice(inGame);
                inGame.GameOutput($"Player {inPlayer.Name} has thrown {inPlayer.Dice[0]} and {inPlayer.Dice[1]} and moves to position {inPlayer.position}, {inGame.MyPlayField[inPlayer.position].Name}", Vector.setNew(0, 1, 1));
                inPlayer.turnsWaitedAtThisSquare = 0;
                inGame.MyPlayField[inPlayer.position].Actions(inPlayer, inGame);
            }
        }
    }
}
