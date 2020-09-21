using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class gamelife : cellautomata
    {
        public gamelife(int w, int h, bool wrap) : base(w, h, wrap)
        {
            //add things here
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
    }
}
