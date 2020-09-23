using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Coord;

namespace GameOfLife
{
    public class cellautomata : map
    {
        public cellautomata(int w, int h, bool wrap, float cStartAlive) : base(w, h, cStartAlive)
        {
            wrapping = wrap;
        }

        protected bool wrapping;    //if the edges should be wrapped

        public const int nNeighs = 8;
        coord[] neighlist = new coord[nNeighs] { new coord(-1, 0), new coord(0, 1), new coord(1, 0), new coord(0, -1),
            new coord(-1, -1), new coord(1, -1), new coord(-1, 1), new coord(1, 1)};

        void checkLoop(ref coord p)
        {
            if (p.x >= width)
            {
                p.x -= width;
            }
            if (p.y >= height)
            {
                p.y -= height;
            }
            if (p.x < 0)
            {
                p.x += width;
            }
            if (p.y < 0)
            {
                p.y += height;
            }
        }

        public void PlaceLine(coord p, Random rand)
        {
            int type = rand.Next() % 6;
            int len = rand.Next() % 7;
            for (int j = 0; j < len; j++)
            {
                coord newp = p;
                switch(type)
                {
                    case 0:
                        newp += j;
                        break;

                    case 1:
                        newp -= j;
                        break;

                    case 2:
                        newp.x += j;
                        break;

                    case 3:
                        newp.x -= j;
                        break;

                    case 4:
                        newp.y += j;
                        break;

                    case 6:
                        newp.y -= j;
                        break;
                }
                checkLoop(ref newp);
                grid[newp.x][newp.y].update(true);
            }
        }

        public float aliveFac()
        {
            return (float)aliveCount / (float)area;
        }

        protected int countAliveNeighbours(coord pos)
        {
            //0111
            //01x1
            //0111
            //0000

            int sum = 0;
            for (int i = 0; i < nNeighs; i++)
            {
                coord neigh = neighlist[i] + pos;
                checkLoop(ref neigh);
                if (grid[neigh.x][neigh.y].alive)
                    sum += 1;       //alive so count it
            }
            return sum;
        }

        public void Simulate(int noSteps, bool printChanges)
        {
            for (int i = 0; i < noSteps; i++)
            {
                Print(printChanges);
                stepSimulate();

                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }

        public void SkipSimulate(int noSteps, bool printChanges)
        {
            for (int i = 0; i < noSteps; i++)
            {
                stepSimulate();
            }
            Print(printChanges);
        }

        public bool CheckExtinct()
        {
            bool extinct = false;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (grid[x][y].alive)
                        return true;
                }
            }
            //no alive cells found
            return extinct;
        }

        public virtual void stepSimulate()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int nalive = countAliveNeighbours(new coord(x, y));
                    if (grid[x][y].alive)
                    {
                        if (nalive == 2 | nalive == 3)      //survives
                        {
                            //keep alive
                            grid[x][y].update(true);
                        }
                        else                                //dies
                        {
                            aliveCount -= 1;
                            grid[x][y].update(false);
                        }
                    }
                    else  //dead cell at the mo
                    {
                        if (nalive == 3)
                        {
                            aliveCount += 1;
                            grid[x][y].update(true);
                        }
                        else
                        {
                            grid[x][y].update(false);
                        }
                    }
                }
            }
        }
    }
}
