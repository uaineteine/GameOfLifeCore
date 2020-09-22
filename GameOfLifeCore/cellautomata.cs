using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class cellautomata : map
    {
        public cellautomata(int w, int h, bool wrap, float cStartAlive) : base(w, h, cStartAlive)
        {
            wrapping = wrap;
        }

        protected bool wrapping;    //if the edges should be wrapped

        public const int nNeighs = 8;
        coord[] neighlist = new coord[nNeighs] { new coord(-1, 0), new coord(0, 1), new coord(1, 0), new coord(0, -1),
            new coord(-1, -1), new coord(1, -1), new coord(-1, 1), new coord(1, 1)};

        void checkLoop(ref coord p)
        {
            if (p.x >= width)
            {
                p.x -= width;
            }
            if (p.y >= height)
            {
                p.y -= height;
            }
            if (p.x < 0)
            {
                p.x += width;
            }
            if (p.y < 0)
            {
                p.y += height;
            }
        }

        public float aliveFac()
        {
            return (float)aliveCount / (float)area;
        }

        protected int countAliveNeighbours(coord pos)
        {
            //0111
            //01x1
            //0111
            //0000

            int sum = 0;
            for (int i = 0; i < nNeighs; i++)
            {
                coord neigh = neighlist[i] + pos;
                checkLoop(ref neigh);
                if (grid[neigh.x][neigh.y].alive)
                    sum += 1;       //alive so count it
            }
            return sum;
        }

        public void stepSimulate()
        {
            List<List<cell>> newMap = grid;
            for (int x = 0; x < width; x++)
            {
                Parallel.For(0, height,
                   y => {
                       int nalive = countAliveNeighbours(new coord(x, y));
                       if (grid[x][y].alive)
                       {
                           if (nalive == 2 | nalive == 3)      //survives
                           {
                               //keep alive
                               newMap[x][y].update(true);
                           }
                           else                                //dies
                           {
                               aliveCount -= 1;
                               newMap[x][y].update(false);
                           }
                       }
                       else  //dead cell at the mo
                       {
                           if (nalive == 3)
                           {
                               aliveCount += 1;
                               newMap[x][y].update(true);
                           }
                           else
                           {
                               newMap[x][y].update(false);
                           }
                       }
                   });
            }
            grid = newMap;
        }
    }
}
