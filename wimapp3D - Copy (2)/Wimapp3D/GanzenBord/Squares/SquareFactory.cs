using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Ganzenbord
{

    static class SquareFactory
    {
        public static iSquare Build(string chosenValue, int pos)
        {
            var y = Type.GetType($"Ganzenbord.{chosenValue}");//Ganzenbord is de namespace
            Object[] args = { pos };
            iSquare resultSquare = (iSquare)Activator.CreateInstance(y, args);
            return resultSquare;
        }
    }
}
