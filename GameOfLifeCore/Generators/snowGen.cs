using System;
using Uaine.CellularAutomata;
using Uaine.Coord;

namespace Uaine.CellularAutomata
{
    public class snowGen : CAmap
    {
        public snowGen(int w, int h, CASettings settings, Random rndm) : base(w, h, settings)
        {
            rand = rndm;
        }

        Random rand;
        int maxSnow = 4;

        protected void AddDropTop(int x)
        {
            aliveCount += 1;
            CMap.cells[x, 0] = (true);
            newBorn.Add(new coord(x, 0));
        }

        protected void shiftRowDown(int y)
        {
            if (boundCheck(new coord(0, y)))
            {
                for (int x = 0; x < Width; x++)
                {
                    if (CMap.cells[x, y])
                    {
                        //shift it down
                        CMap.cells[x, y+1] = (true);
                        CMap.cells[x, y] = (false);
                    }
                }
            }
        }

        public override void stepSimulate()
        {
            int noSnow = rand.Next() % maxSnow;

            //delete bottom row first
            ClearRow(Height - 1);

            for (int y = Height - 1; y > - 1; y--)
            {
                shiftRowDown(y);
            }

            for (int i = 0; i < noSnow; i++)
            {
                int x = rand.Next() % Width;
                AddDropTop(x);
            }
        }
    }
}
