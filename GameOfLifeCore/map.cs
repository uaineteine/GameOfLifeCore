using System;
using System.Collections.Generic;
using System.Text;
using Uaine.Coord;

namespace Uaine.GameOfLife.Core
{
    public class map
    {
        public map(int w, int h, float cStrtAl)     //chance to start alive
        {
            width = w;
            height = h;
            area = width * height;
            aliveCount = 0;
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
            cStartAlive = cStrtAl;

            randomiseMap();
        }
        protected int width;
        protected int height;
        private float cStartAlive;
        protected List<List<cell>> grid;
        protected int aliveCount;
        protected int area = 1;
        public bool isAlive(coord p)
        {
            return grid[p.x][p.y].alive;
        }

        public bool isNew(coord p)
        {
            if (grid[p.x][p.y].HasChanged())
            {
                if (isAlive(p))
                {
                    return true;
                }
            }
            return false;
        }

        protected void randomiseMap()
        {
            aliveCount = 0;
            Random rand = new Random();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float val = (float)rand.NextDouble();
                    if (val < cStartAlive)
                    {
                        grid[x][y].alive = false;
                    }
                    else
                    { 
                        grid[x][y].alive = true;
                        aliveCount += 1;
                    }
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