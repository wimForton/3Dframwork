using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Ganzenbord
{
    class SquareBridge : Square, iSquare
    {
        public override string Name { get; set; } = "Bridge";
        public override Vector Color { get; set; } = Vector.setNew(0.2, 0.5, 1);
        
        public SquareBridge(int pos) : base(pos)
        {
            SquarePos = pos;
        }
        public override void Actions(Player inPlayer, Game inGame)
        {
            inGame.GameOutput($"Player has arrived at {Name} square", Vector.setNew(0,1,1));
            inGame.GameOutput($"Bridges are so easy, I don't have to swim this swamp and go straight to 12", Vector.setNew(1, 1, 0));
            inPlayer.KeyFrames.Add(inPlayer.position);
            inPlayer.TweenPos.Add(0);
            inPlayer.position = 12;
            inPlayer.KeyFrames.Add(inPlayer.position);
            inPlayer.TweenPos.Add(0);
            inPlayer.turnsWaitedAtThisSquare = 0;
            inGame.MyPlayField[inPlayer.position].Actions(inPlayer, inGame);
        }
    }
}
