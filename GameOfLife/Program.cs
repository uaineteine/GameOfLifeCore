using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //load it
            gamelife ca = new gamelife(20, 20);
            ca.Simulate(10);

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
