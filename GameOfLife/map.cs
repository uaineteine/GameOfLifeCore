using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class map
    {
        public map(int w, int h)
        {
            width = w;
            height = h;
            grid = new List<List<cell>>();
            for (int x = 0; x < w; x++)
            {
                List<cell> col = new List<cell>();
                for (int y = 0; y < h; y++)
                {
                    col.Add(new cell(false));
                }
                grid.Add(col);
            }

            randomiseMap();
        }
        protected int width;
        protected int height;
        protected List<List<cell>> grid;

        protected void randomiseMap()
        {
            Random rndm = new Random();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int val = rndm.Next() % 2;
                    if (val == 0)
                        grid[x][y].alive = false;
                    else
                        grid[x][y].alive = true;
                }
            }
        }

        public bool boundCheck(int x, int y)
        {
            if (x < width && y < height)
            {
                if (x > -1 && y > -1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public bool boundCheck(coord p)
        {
            return boundCheck(p.x, p.y);
        }

        public void Print(bool printChanges)
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if (grid[x][y].alive)
                    {
                        if (printChanges)
                            if (grid[x][y].HasChanged())
                                Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(1);
                    }
                    else
                    {
                        if (printChanges)
                            if (grid[x][y].HasChanged())
                                Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(0);
                    }
                }
                Console.Write(System.Environment.NewLine);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}