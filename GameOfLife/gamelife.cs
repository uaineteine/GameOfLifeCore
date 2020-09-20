using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class gamelife : cellautomata
    {
        public gamelife(int w, int h) : base(w, h)
        {
            //add things here
        }

        public void stepSimulate()
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
                            //do nothing
                        }
                        else                                //dies
                        {
                            grid[x][y].alive = false;
                        }
                    }
                    else  //dead cell at the mo
                    {
                        if (nalive == 3)
                        {
                            grid[x][y].alive = true;
                        }
                    }
                }
            }
        }

        public void Simulate(int noSteps)
        {
            for (int i = 0; i < noSteps; i++)
            {
                Print();
                stepSimulate();

                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
