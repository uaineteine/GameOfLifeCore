using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uaine.Coord;

namespace Uaine.GameOfLife.Core
{
    public class LineGenerator : cellautomata
    {
        protected int numLines;
        protected int MinLen = 4;
        protected int MaxLen = 14;
        protected Random r;
        public LineGenerator(int w, int h, bool wrap, float cStartAlive, int nLines) : base(w, h, wrap, cStartAlive)
        {
            r = new Random();
            numLines = nLines;
            //needs grid that is blank to begin with
            for (int x = 0; x < width; x++)
            {
                Parallel.For(0, height,
                  y =>
                  {
                      grid[x][y] = new cell(false);
                  });
            }
        }

        public override void stepSimulate()
        {
            int i = 0;
            int k = 0;
            while(i < numLines)
            { 
                int len = MinLen;
                len += r.Next() % (MaxLen - MinLen);
                coord p = new coord(r.Next() % width, r.Next() % height);
                if (!grid[p.x][p.y].alive)
                {
                    PlaceLine(p, r, len);
                    i += 1;
                }
                k += 1;
                if (k > 1000)   //breakout failsafe
                    break;
            }
        }
    }
}
