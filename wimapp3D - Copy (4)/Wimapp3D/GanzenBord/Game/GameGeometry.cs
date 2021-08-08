using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Ganzenbord
{
    class GameGeometry
    {
        public List<Vector> Player1Positions = new List<Vector>();
        public List<Vector> Player2Positions = new List<Vector>();
        public List<Vector> Player3Positions = new List<Vector>();
        public List<Vector> Player4Positions = new List<Vector>();
        //public IRenderableGeo playfield = new IRenderableGeo();

        public GameGeometry(Game inGame)
        {
            CreatePositions();
        }

        private void CreatePositions()
        {
            for (int i = 0; i < 64; i++)
            {

            }
        }
    }
}
