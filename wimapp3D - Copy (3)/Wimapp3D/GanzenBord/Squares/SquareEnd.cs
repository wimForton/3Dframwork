using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Ganzenbord
{
    class SquareEnd : Square, iSquare
    {
        string Winner { get; set; } = "";
        public override string Name { get; set; } = "End";
        public SquareEnd(int pos) : base(pos)
        {
            SquarePos = pos;
            Name = Convert.ToString(SquarePos);
        }
        public override void Actions(Player inPlayer, Game inGame)
        {
            if (Winner == "")
            {
                inGame.GameOutput($"{inPlayer.Name} HAS ARRIVED AND WINS", Vector.setNew(1, 1, 0.5));
                Winner = inPlayer.Name;
            }
            else if(Winner == inPlayer.Name)
            {
                inGame.GameOutput($"Where are all these losers stuck? I'm getting lonely up here", Vector.setNew(1, 1, 0.5));

            }
            else
            {
                inGame.GameOutput($"You're welcome but {Winner} was here before you", Vector.setNew(1, 1, 0.5));
            }
        }
    }
}
