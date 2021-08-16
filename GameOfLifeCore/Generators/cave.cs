using System;
using System.Threading.Tasks;
using Uaine.Coord;
using Uaine.Random;

namespace Uaine.CellularAutomata
{
    public class cave : CAmap
    {
        public cave(int w, int h, CASettings settings, int birthlim, int deathlim, URandom rndm) : base(w, h, settings, rndm)
        {
            birthlimit = birthlim;
            deathlimit = deathlim;
            rand = rndm;
        }

        int birthlimit;
        int deathlimit;
        int treasureHiddenLimit = 5;

        //overriding
        public override void stepSimulate()
        {
            int[,] neighs = AliveNeighbourMap();
            for (int x = 0; x < Width; x++)
            {
                Parallel.For(0, Height,
               y =>
               {
                   int nalive = neighs[x, y];
                   if (CMap.cells[x, y])
                   {
                       if (nalive < deathlimit)            //dies
                       {
                           aliveCount -= 1;
                           CMap.cells[x, y] = (false);
                           newDead.Add(new coord(x, y));
                       }
                       else                                //survives
                       {
                           CMap.cells[x, y] = (true);
                       }
                   }
                   else  //dead cell at the mo
                   {
                       if (nalive > birthlimit)
                       {
                           aliveCount += 1;
                           CMap.cells[x, y] = (true);
                           newBorn.Add(new coord(x, y));
                       }
                       else
                       {
                           CMap.cells[x, y] = (false);
                       }
                       if (nalive >= treasureHiddenLimit)
                       {
                           //place treasure
                           PlaceLine(new coord(x, y));
                       }
                   }
               });
            }
        }
    }
}
