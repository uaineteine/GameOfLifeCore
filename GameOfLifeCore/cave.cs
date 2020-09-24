using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Coord;

namespace GameOfLife
{
    public class cave : cellautomata
    {
        public cave(int w, int h, bool wrap, float cStartAlive, int birthlim, int deathlim, Random rndm) : base(w, h, wrap, cStartAlive)
        {
            birthlimit = birthlim;
            deathlimit = deathlim;
            rand = rndm;
        }

        Random rand;
        int birthlimit;
        int deathlimit;
        int treasureHiddenLimit = 5;

        //overriding
        public override void stepSimulate()
        {
            int[,] neighs = AliveNeighbourMap();
            for (int x = 0; x < width; x++)
            {
                Parallel.For(0, height,
               y =>
               {
                   int nalive = neighs[x, y];
                   if (grid[x][y].alive)
                   {
                       if (nalive < deathlimit)            //dies
                       {
                           aliveCount -= 1;
                           grid[x][y].update(false);
                       }
                       else                                //survives
                       {
                           grid[x][y].update(true);
                       }
                   }
                   else  //dead cell at the mo
                   {
                       if (nalive > birthlimit)
                       {
                           aliveCount += 1;
                           grid[x][y].update(true);
                       }
                       else
                       {
                           grid[x][y].update(false);
                       }
                       if (nalive >= treasureHiddenLimit)
                       {
                           //place treasure
                           PlaceLine(new coord(x, y), rand);
                       }
                   }
               });
            }
        }
    }
}
