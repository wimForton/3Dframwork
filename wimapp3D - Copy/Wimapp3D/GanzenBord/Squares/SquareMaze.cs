using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Ganzenbord
{
    class SquareMaze : Square, iSquare
    {
        public override string Name { get; set; } = "Maze";
        public override Vector Color { get; set; } = Vector.setNew(0.5, 0.5, 0.5);
        public SquareMaze(int pos) : base(pos)
        {
            SquarePos = pos;
        }
        public override void Actions(Player inPlayer, Game inGame)
        {
            inGame.GameOutput($"Player has arrived at {Name} square", Vector.setNew(0, 1, 1));
            inGame.GameOutput($"Is this the way out? Oh no it's place 39, could have been worse...", Vector.setNew(1, 1, 0));
            inPlayer.KeyFrames.Add(inPlayer.position);
            inPlayer.TweenPos.Add(0);
            inPlayer.position = 39;
            inPlayer.KeyFrames.Add(39);
            inPlayer.TweenPos.Add(0);
            inPlayer.turnsWaitedAtThisSquare = 0;
            inGame.MyPlayField[inPlayer.position].Actions(inPlayer, inGame);
        }
    }
}
