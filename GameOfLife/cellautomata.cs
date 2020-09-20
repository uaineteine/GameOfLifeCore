using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class cellautomata : map
    {
        public cellautomata(int w, int h) : base(w, h)
        {
            //other stuff here
        }

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
    }
}
