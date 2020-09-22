using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class gamelife : cellautomata
    {
        public gamelife(int w, int h, bool wrap, float cStartAlive) : base(w, h, wrap, cStartAlive)
        {
            //add things here
        }

        //overriding
        public void stepSimulate()
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
                       if (nalive == 2 | nalive == 3)      //survives
                           {
                               //keep alive
                               grid[x][y].update(true);
                       }
                       else                                //dies
                           {
                           aliveCount -= 1;
                           grid[x][y].update(false);
                       }
                   }
                   else  //dead cell at the mo
                       {
                       if (nalive == 3)
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
