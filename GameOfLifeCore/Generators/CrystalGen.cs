using System.Threading.Tasks;
using Uaine.Coord;
using Uaine.Random;

namespace Uaine.CellularAutomata
{
    public class CrystalGen : LineGenerator
    {
        protected int variation;

        public CrystalGen(int w, int h, CASettings settings, int nLines, int var, URandom rndm) : base(w, h, settings, nLines, rndm)
        {
            //add things here
            base.stepSimulate();

            variation = var;
        }

        public override void stepSimulate()
        {
            if (variation == 1)
            {
                base.stepSimulate();
            }

            int[,] neighs = AliveNeighbourMap();
            for (int x = 0; x < Width; x++)
            {
                Parallel.For(0, Height,
               y =>
               {
                   int nalive = neighs[x, y];
                   if (!CMap.cells[x, y])
                   {
                       if (nalive == 2)
                       {
                           aliveCount += 1;
                           CMap.cells[x, y] = (true);
                           newBorn.Add(new coord(x, y));
                       }
                       else
                       {
                           CMap.cells[x, y] = (false);
                       }
                   }
               });
            }
        }
    }
}
