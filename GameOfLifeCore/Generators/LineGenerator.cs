using System;
using System.Threading.Tasks;
using Uaine.Coord;

namespace Uaine.CellularAutomata
{
    public class LineGenerator : CAmap
    {
        protected int numLines;
        protected int MinLen = 4;
        protected int MaxLen = 14;
        protected Random r;
        public LineGenerator(int w, int h, CASettings settings, int nLines) : base(w, h, settings)
        {
            r = new Random();
            numLines = nLines;
            //needs grid that is blank to begin with
            for (int x = 0; x < Width; x++)
            {
                Parallel.For(0, Height,
                  y =>
                  {
                      CMap.cells[x, y] = (false);
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
                coord p = new coord(r.Next() % Width, r.Next() % Height);
                if (!CMap.cells[p.x, p.y])
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
