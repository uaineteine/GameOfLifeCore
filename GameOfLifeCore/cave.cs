using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class cave : cellautomata
    {
        public cave(int w, int h, bool wrap, float cStartAlive, int birthlim, int deathlim) : base(w, h, wrap, cStartAlive)
        {
            birthlimit = birthlim;
            deathlimit = deathlim;
        }

        int birthlimit;
        int deathlimit;

        //overriding
        public override void stepSimulate()
        {
            int[,] neighs = new int[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    neighs[x, y] = countAliveNeighbours(new coord(x, y));
                }
            }
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
                   }
               });
            }
        }
    }
}
