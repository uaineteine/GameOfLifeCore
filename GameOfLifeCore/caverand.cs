using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class caverand : cave
    {
        public caverand(int w, int h, bool wrap, float cStartAlive, int birthlim, int deathlim, Random rndm) : base(w, h, wrap, cStartAlive, birthlim, deathlim)
        {
            rand = rndm;
        }

        int randomCells = 40;
        Random rand;

        //override the previous but add something new
        public override void stepSimulate()
        {
            base.stepSimulate();

            for (int i = 0; i < randomCells; i++)
            {
                int x = rand.Next() % width;
                int y = rand.Next() % height;

                if (grid[x][y].alive)
                {
                    aliveCount -= 1;
                    grid[x][y].update(false);
                }
                else
                {
                    aliveCount += 1;
                    grid[x][y].update(true);
                }
            }
        }
    }
}
