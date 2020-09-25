using Coord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class CrystalGen : LineGenerator
    {
        protected int variation;

        public CrystalGen(int w, int h, bool wrap, float cStartAlive, int nLines, int var) : base(w, h, wrap, cStartAlive, nLines)
        {
            //add things here
            base.stepSimulate();

            variation = var;
        }

        public override void stepSimulate()
        {
            if (variation == 1)
            {
                base.stepSimulate();
            }

            int[,] neighs = AliveNeighbourMap();
            for (int x = 0; x < width; x++)
            {
                Parallel.For(0, height,
               y =>
               {
                   int nalive = neighs[x, y];
                   if (!grid[x][y].alive)
                   {
                       if (nalive == 2)
                       {
                           aliveCount += 1;
                           grid[x][y].update(true);
                       }
                       else
                       {
                           grid[x][y].update(false);
                       }
                   }
               });
            }
        }
    }
}
