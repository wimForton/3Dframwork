using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ganzenbord
{
    class SquareDefault: Square, iSquare
    {
        public override string Name { get; set; } = "Default";
        public SquareDefault(int pos) : base(pos)
        {
            SquarePos = pos;
            Name = Convert.ToString(SquarePos);
        }

    }
}
