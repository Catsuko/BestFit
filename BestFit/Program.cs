using System;
using System.Linq;

namespace BestFit
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                double[] tileSize;
                double[] floorSize;

                try
                {
                    tileSize = CaptureDimensions("Tile Size?");
                    floorSize = CaptureDimensions("Floor Size?");
                }
                catch (FormatException ex)
                {
                    DisplayInvalidFormatMessage();
                    continue;
                }

                DisplayTilesPicked(tileSize, floorSize);
            }
        }

        private static double[] CaptureDimensions(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine().Replace(" ", "").Split('x').Select(str => double.Parse(str)).ToArray();
        }

        private static void DisplayInvalidFormatMessage ()
        {
            Console.WriteLine();
            Console.WriteLine("Invalid Input!");
            Console.WriteLine("Please enter the size in the following format: [WIDTH] x [LENGTH]");
            Console.WriteLine("For example, to make a Rectangle of Width 2 and Lenght 4, enter the following:");
            Console.WriteLine("2x4 or 2 x 4");
            Console.ReadKey();
        }

        private static void DisplayTilesPicked (double[] tileSize, double[] floorSize)
        {
            Rectangle tile = new Rectangle(tileSize[0], tileSize[1]);
            Rectangle container = new Rectangle(floorSize[0], floorSize[1]);
            Console.WriteLine($"{tile} best fills {container} with the following:");
            var fillGrouping = tile.Fill(container).GroupBy((rect) => rect);
            foreach (var group in fillGrouping)
                Console.WriteLine($"{group.Count()} x {group.Key}");
            Console.ReadKey();
        }
    }
}
