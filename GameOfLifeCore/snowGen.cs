using Coord;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class snowGen : cellautomata
    {
        public snowGen(int w, int h, bool wrap, Random rndm) : base(w, h, wrap, 1f)
        {
            rand = rndm;
        }

        Random rand;
        int maxSnow = 4;

        protected void AddDropTop(int x)
        {
            aliveCount += 1;
            grid[x][0].update(true);
        }

        protected void shiftRowDown(int y)
        {
            if (boundCheck(new coord(0, y)))
            {
                for (int x = 0; x < width; x++)
                {
                    if (grid[x][y].alive)
                    {
                        //shift it down
                        grid[x][y + 1].update(true);
                        grid[x][y].update(false);
                    }
                }
            }
        }

        public override void stepSimulate()
        {
            int noSnow = rand.Next() % maxSnow;

            //delete bottom row first
            ClearRow(height - 1);

            for (int y = height - 1; y > - 1; y--)
            {
                shiftRowDown(y);
            }

            for (int i = 0; i < noSnow; i++)
            {
                int x = rand.Next() % width;
                AddDropTop(x);
            }
        }
    }
}
