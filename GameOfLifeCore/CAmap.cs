using System;
using System.Collections.Generic;
using System.Text;
using Uaine.Coord;
using Uaine.Objects.Maps;
using Uaine.Objects.Primitives.Shapes;
using Uaine.Random;

namespace Uaine.CellularAutomata
{
    public class CAmap : IntRectangle
    {
        public URandom rand;
        public BoolMap CMap;
        public CASettings Settings;
        protected List<coord> newBorn = new List<coord>();
        protected List<coord> newDead = new List<coord>();
        protected int aliveCount = 0;

        internal const int nNeighs = 8;
        internal static readonly coord[] neighlist = new coord[nNeighs] { new coord(-1, 0), new coord(0, 1), new coord(1, 0), new coord(0, -1),
            new coord(-1, -1), new coord(1, -1), new coord(-1, 1), new coord(1, 1)};
        public CAmap(int width, int height, CASettings settings, URandom rnd) : base(width, height)
        {
            CMap = new BoolMap(width, height, false);
            Settings = settings;
            rand = rnd;
            Initalise();
        }
        public void Initalise() //called on const
        {
            aliveCount = 0;
            //consider chance to start alive
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    float val = (float)rand.NextDouble();
                    if (val < Settings.ChanceStartAlive)
                    {
                        CMap.cells[x,y] = false;
                    }
                    else
                    {
                        CMap.cells[x, y] = true;
                        aliveCount += 1;
                    }
                }
            }
        }
        void checkLoop(ref coord p)
        {
            if (p.x < 0)
            {
                p.x += Width;
            }
            else if (p.x >= Width)
            {
                p.x -= Width;
            }
            if (p.y < 0)
            {
                p.y += Height;
            }
            else if (p.y >= Height)
            {
                p.y -= Height;
            }
        }
        protected bool boundCheck(int x, int y)
        {
            if (x < Width && y < Height)
            {
                if (x > -1 && y > -1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        protected bool boundCheck(coord p)
        {
            return boundCheck(p.x, p.y);
        }
        protected int[,] AliveNeighbourMap()
        {
            int[,] neighs = new int[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    neighs[x, y] = countAliveNeighbours(x, y);
                }
            }
            return neighs;
        }
        public void Print(bool printChanges)
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if (printChanges)
                    {
                            if (NewlyBorn(x, y))
                                Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        if (printChanges)
                            if (NewlyDead(x, y))
                                Console.ForegroundColor = ConsoleColor.Red;
                    }
                    printCell(x, y);
                }
                Console.Write(System.Environment.NewLine);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        private void printCell(int x, int y)
        {
            if (CMap.cells[x, y])
                Console.Write("1");
            else
                Console.Write("0");
        }
        private bool NewlyDead(int x, int y)
        {
            foreach (coord item in newDead)
            {
                if (item.x == x && item.y == y)
                    return true;
            }
            return false;
        }
        private bool NewlyBorn(int x, int y)
        {
            foreach (coord item in newBorn)
            {
                if (item.x == x && item.y == y)
                    return true;
            }
            return false;
        }

        protected int countAliveNeighbours(int x, int y)
        {
            //0111
            //01x1
            //0111
            //0000

            int sum = 0;
            coord p = new coord(x, y);
            for (int i = 0; i < nNeighs; i++)
            {
                coord neigh = neighlist[i] + p;
                checkLoop(ref neigh);
                if (CMap.cells[neigh.x, neigh.y])
                    sum += 1;       //alive so count it
            }
            return sum;
        }
        public bool CheckExtinct()
        {
            bool extinct = false;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (CMap.cells[x,y])
                        return true;
                }
            }
            //no alive cells found
            return extinct;
        }
        public void PlaceLine(coord p, int len, int dir, bool posneg)
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
                if (!CMap.cells[newp.x, newp.y])
                {
                    CMap.cells[newp.x, newp.y] = true;
                    newBorn.Add(newp);
                    aliveCount += 1;
                }
            }
        }
        protected void ClearRow(int rown)
        {
            for (int x = 0; x < Width; x++)
            {
                CMap.cells[x, rown] = (false);
            }
        }

        public void PlaceLine(coord p, int len)
        {
            int dir = rand.Next() % 3;
            int posneg = rand.Next() % 2;
            if (posneg == 0)
            {
                PlaceLine(p, len, dir, false);
            }
            else
            {
                PlaceLine(p, len, dir, true);
            }
        }

        public void PlaceLine(coord p)
        {
            PlaceLine(p, rand.Next() % 7);
        }

        public void Simulate(int noSteps, bool printChanges)
        {
            for (int i = 0; i < noSteps; i++)
            {
                Print(printChanges);
                //clear history
                ClearHistory();
                //simstep
                stepSimulate();

                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }

        public virtual void stepSimulate()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int nalive = countAliveNeighbours(x, y);
                    if (CMap.cells[x, y])
                    {
                        if (nalive == 2 | nalive == 3)      //survives
                        {
                            //keep alive
                            CMap.cells[x, y] =(true);
                        }
                        else                                //dies
                        {
                            aliveCount -= 1;
                            CMap.cells[x, y] =(false);
                            newDead.Add(new coord(x, y));
                        }
                    }
                    else  //dead cell at the mo
                    {
                        if (nalive == 3)
                        {
                            aliveCount += 1;
                            CMap.cells[x, y]=(true);
                            newBorn.Add(new coord(x, y));
                        }
                        else
                        {
                            CMap.cells[x, y]=(false);
                        }
                    }
                }
            }
        }

        private void ClearHistory()
        {
            newBorn = new List<coord>();
            newDead = new List<coord>();
        }

        public void SkipSimulate(int noSteps, bool printChanges)
        {
            for (int i = 0; i < noSteps; i++)
            {
                stepSimulate();
            }
            Print(printChanges);
        }
    }
}
