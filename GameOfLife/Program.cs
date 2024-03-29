﻿using System;
using Uaine.CellularAutomata;
using Uaine.Random;
using Version = Uaine.VersionController.Version;

namespace Uaine.GameOfLife.ConsoleApp
{
    class Program
    {
        static Version ver = new Version(new int[] { 2, 0 }, "Beta", false);
        static void Main(string[] args)
        {

            Console.WriteLine(ver.ToStr());

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
            CASettings set = new CASettings(wraped, 0.4f);
            URandom rand = new URandom();
            gamelife ca = new gamelife(35, 35, set, rand);

            //simulate
            //ca.SkipSimulate(noSteps, printChanges);
            ca.Simulate(noSteps, printChanges);

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
