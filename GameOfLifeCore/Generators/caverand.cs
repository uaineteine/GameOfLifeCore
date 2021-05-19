using System;
using Uaine.Coord;

namespace Uaine.CellularAutomata
{
    public class caverand : cave
    {
        public caverand(int w, int h, CASettings settings, int birthlim, int deathlim, Random rndm) : base(w, h, settings, birthlim, deathlim, rndm)
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
                int x = rand.Next() % Width;
                int y = rand.Next() % Height;

                if (CMap.cells[x, y])
                {
                    aliveCount -= 1;
                    CMap.cells[x, y] = (false);
                    newDead.Add(new coord(x, y));
                }
            }
        }
    }
}
