using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Ganzenbord
{
    class SquareDeath : Square, iSquare
    {
        public override string Name { get; set; } = "Death";
        public override Vector Color { get; set; } = Vector.setNew(0.5, 0.0, 0.0);
        public override int SquarePos { get; set; }
        public SquareDeath(int pos) : base(pos)
        {
            SquarePos = pos;
        }
        public override void Actions(Player inPlayer, Game inGame)
        {
            inGame.GameOutput($"Player has arrived at {Name} square", Vector.setNew(0, 1, 1));
            inGame.GameOutput($"Oh damn, you're death and have to start your life over again", Vector.setNew(1, 1, 0));
            inPlayer.KeyFrames.Add(inPlayer.position);
            inPlayer.TweenPos.Add(0);
            inPlayer.position = 0;
            inPlayer.KeyFrames.Add(inPlayer.position);
            inPlayer.TweenPos.Add(0);
            inPlayer.turnsWaitedAtThisSquare = -1;
            inGame.MyPlayField[inPlayer.position].Actions(inPlayer, inGame);
        }
    }
}
