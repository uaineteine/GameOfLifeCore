using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //get user input
            Console.WriteLine("Wrap so borders are looped? (y/n)");
            Console.WriteLine();
            string res = Console.ReadLine();
            bool wraped = true;
            if (res == "n")
                wraped = false;
            int noSteps = 0;
            Console.WriteLine("Give number of steps to simulate:");
            Console.WriteLine();
            res = Console.ReadLine();
            noSteps = Convert.ToInt32(res);
            Console.WriteLine("Print changes? (y/n)");
            Console.WriteLine();
            res = Console.ReadLine();
            bool printChanges = false;
            if (res == "y")
                printChanges = true;
            Console.WriteLine();

            //load it
            gamelife ca = new gamelife(35, 35, wraped, 0.2f);

            //simulate
            //ca.SkipSimulate(noSteps, printChanges);
            ca.Simulate(noSteps, printChanges);

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
