using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Ganzenbord
{
    class SquareGans : Square, iSquare
    {
        public override string Name { get; set; } = "Gans";
        public override Vector Color { get; set; } = Vector.setNew(0.5, 1, 1);
        public SquareGans(int pos) : base(pos)
        {
            SquarePos = pos;
        }
        public override void Actions(Player inPlayer, Game inGame)
        {
            inGame.GameOutput($"Player has arrived at {Name} square", Vector.setNew(0, 1, 1));
            inPlayer.KeyFrames.Add(inPlayer.position);
            inPlayer.TweenPos.Add(0);
            inPlayer.position += inPlayer.Dice[0] + inPlayer.Dice[1];
            if (inPlayer.position > 63)
            {
                inPlayer.KeyFrames.Add(63);
                inPlayer.TweenPos.Add(0);
                inPlayer.position = 63 - (inPlayer.position - 63);
                inGame.GameOutput($"Player {inPlayer.Name} bounces back", Vector.setNew(0, 1, 1));
                inPlayer.Dice[0] *= -1;
                inPlayer.Dice[1] *= -1;
            }
            inPlayer.KeyFrames.Add(inPlayer.position);
            inPlayer.TweenPos.Add(0);
            inGame.GameOutput($"Player {inPlayer.Name} dice was {inPlayer.Dice[0]} and {inPlayer.Dice[1]} and moves on to position {inPlayer.position}, {inGame.MyPlayField[inPlayer.position].Name}", Vector.setNew(0, 1, 1));
            inPlayer.turnsWaitedAtThisSquare = 0;
            inGame.MyPlayField[inPlayer.position].Actions(inPlayer, inGame);
        }
    }
}
