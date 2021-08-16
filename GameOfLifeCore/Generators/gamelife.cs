using System.Threading.Tasks;
using Uaine.Coord;
using Uaine.Random;

namespace Uaine.CellularAutomata
{
    public class gamelife : CAmap
    {
        public gamelife(int w, int h, CASettings settings, URandom rndm) : base(w, h, settings, rndm)
        {
            //add things here
        }

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
                       if (nalive == 2 | nalive == 3)      //survives
                       {
                           //keep alive
                       }
                       else                                //dies
                       {
                           aliveCount -= 1;
                           CMap.cells[x, y] = (false);
                           newDead.Add(new coord(x, y));
                       }
                   }
                   else  //dead cell at the mo
                       {
                       if (nalive == 3)
                       {
                           aliveCount += 1;
                           CMap.cells[x, y] = (true);
                           newBorn.Add(new coord(x, y));
                       }
                       else
                       {
                           //stay dead
                       }
                   }
               });
            }
        }
    }
}
