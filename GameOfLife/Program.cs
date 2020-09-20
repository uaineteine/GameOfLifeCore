﻿using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //load it
            gamelife ca = new gamelife(20, 20);

            //get user input
            int noSteps = 0;
            Console.WriteLine("Give number of steps to simulate:");
            Console.WriteLine();
            string res = Console.ReadLine();
            noSteps = Convert.ToInt32(res);

            //simulate
            ca.Simulate(noSteps);

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
