using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Ganzenbord
{
    class Square: iSquare
    {
        public virtual string Name { get; set; }
        public virtual Vector Color { get; set; } = Vector.setNew(1,1,1);
        public virtual int SquarePos { get; set; }
        public Square(int pos)
        {
            SquarePos = pos;
        }
        public virtual void Actions(Player inPlayer, Game inGame) {
            if (inPlayer.turnsWaitedAtThisSquare == 0)
            {
                inGame.GameOutput($"Player has arrived at {Name} square", Vector.setNew(0, 1, 1));
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
