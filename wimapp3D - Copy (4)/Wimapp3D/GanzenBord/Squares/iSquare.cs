using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Ganzenbord
{
    interface iSquare
    {

        public string Name { get; set; }
        public Vector Color { get; set; }
        public void Actions(Player inPlayer, Game inGame);
    }
}
