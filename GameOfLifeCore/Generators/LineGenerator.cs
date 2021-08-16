using System;
using System.Threading.Tasks;
using Uaine.Coord;
using Uaine.Random;

namespace Uaine.CellularAutomata
{
    public class LineGenerator : CAmap
    {
        protected int numLines;
        protected int MinLen = 4;
        protected int MaxLen = 14;
        public LineGenerator(int w, int h, CASettings settings, int nLines, URandom rndm) : base(w, h, settings, rndm)
        {
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
                len += rand.Next() % (MaxLen - MinLen);
                coord p = new coord(rand.Next() % Width, rand.Next() % Height);
                if (!CMap.cells[p.x, p.y])
                {
                    PlaceLine(p, len);
                    i += 1;
                }
                k += 1;
                if (k > 1000)   //breakout failsafe
                    break;
            }
        }
    }
}
