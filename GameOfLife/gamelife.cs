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

        public void SkipSimulate(int noSteps)
        {
            for (int i = 0; i < noSteps; i++)
            {
                stepSimulate();
            }
            Print();
        }
    }
}
