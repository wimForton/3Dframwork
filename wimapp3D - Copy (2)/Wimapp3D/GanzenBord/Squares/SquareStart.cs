using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Ganzenbord
{
    class SquareStart : Square, iSquare
    {
        public override string Name { get; set; } = "Start";
        public SquareStart(int pos) : base(pos)
        {
            SquarePos = pos;
        }
        public override void Actions(Player inPlayer, Game inGame)
        {
            if(inPlayer.turnsWaitedAtThisSquare == 0)
            {
                inPlayer.ThrowDice(inGame);
                if (inPlayer.Dice[0] == 5 && inPlayer.Dice[1] == 4 || inPlayer.Dice[0] == 4 && inPlayer.Dice[1] == 5)
                {
                    inPlayer.position = 26;
                    inPlayer.KeyFrames.Add(26);
                    inPlayer.TweenPos.Add(0);
                    inGame.GameOutput($"Player {inPlayer.Name} has thrown {inPlayer.Dice[0]} and {inPlayer.Dice[1]} and the rules say {inPlayer.Name} moves to position {inPlayer.position}, {inGame.MyPlayField[inPlayer.position].Name}", Vector.setNew(0, 1, 1));
                }
                else if (inPlayer.Dice[0] == 6 && inPlayer.Dice[1] == 3 || inPlayer.Dice[0] == 3 && inPlayer.Dice[1] == 6)
                {
                    inPlayer.position = 53;
                    inPlayer.KeyFrames.Add(53);
                    inPlayer.TweenPos.Add(0);
                    inGame.GameOutput($"Player {inPlayer.Name} has thrown {inPlayer.Dice[0]} and {inPlayer.Dice[1]} and the rules say {inPlayer.Name} moves to position {inPlayer.position}, {inGame.MyPlayField[inPlayer.position].Name}", Vector.setNew(0, 1, 1));
                }
                else
                {
                    inGame.GameOutput($"Player {inPlayer.Name} has thrown {inPlayer.Dice[0]} and {inPlayer.Dice[1]} and moves to position {inPlayer.position}, {inGame.MyPlayField[inPlayer.position].Name}", Vector.setNew(0, 1, 1));

                }
                inPlayer.turnsWaitedAtThisSquare = 0;
                inGame.MyPlayField[inPlayer.position].Actions(inPlayer, inGame);
            }
            else
            {
                inPlayer.turnsWaitedAtThisSquare = 0;
            }

        }
    }
}
