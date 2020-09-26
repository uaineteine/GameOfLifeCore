using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Uaine.Coord;

namespace Uaine.GameOfLife.Core
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
            if (p.x < 0)
            {
                p.x += width;
            }
            else if (p.x >= width)
            {
                p.x -= width;
            }
            if (p.y < 0)
            {
                p.y += height;
            }
            else if (p.y >= height)
            {
                p.y -= height;
            }
        }

        public void PlaceLine(coord p, Random rand, int len, int dir, bool posneg)
        {
            for (int j = 0; j < len; j++)
            {
                coord newp = new coord(p.x, p.y);
                if (dir == 0)
                {
                    if (posneg)
                        newp.x += j;
                    else
                        newp.x -= j;
                }
                else if (dir == 1)
                {
                    if (posneg)
                        newp.y += j;
                    else
                        newp.y -= j;
                }
                else
                {
                    if (posneg)
                        newp += j;
                    else
                        newp -= j;
                }
                        
                checkLoop(ref newp);
                if (!grid[newp.x][newp.y].alive)
                {
                    grid[newp.x][newp.y].update(true);
                    aliveCount += 1;
                }
            }
        }

        protected void ClearRow(int rown)
        {
            for (int x = 0; x < width; x++)
            {
                grid[x][rown].update(false);
            }
        }

        public void PlaceLine(coord p, Random rand, int len)
        {
            int dir = rand.Next() % 3;
            int posneg = rand.Next() % 2;
            if (posneg == 0)
            {
                PlaceLine(p, rand, len, dir, false);
            }
            else
            {
                PlaceLine(p, rand, len, dir, true);
            }
        }

        public void PlaceLine(coord p, Random rand)
        {
            PlaceLine(p, rand, rand.Next() % 7);
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

        protected int[,] AliveNeighbourMap()
        {
            int[,] neighs = new int[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    neighs[x, y] = countAliveNeighbours(new coord(x, y));
                }
            }
            return neighs;
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
