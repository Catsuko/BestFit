using System;
using System.Collections.Generic;
using System.Linq;

namespace BestFit
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.Clear();
                double[] tileSize;
                double[] floorSize;
                try
                {
                    Console.WriteLine("Tile Size?");
                    tileSize = Console.ReadLine().Replace(" ", "").Split('x').Select(str => double.Parse(str)).ToArray();
                    Console.WriteLine("Floor Size?");
                    floorSize = Console.ReadLine().Replace(" ", "").Split('x').Select(str => double.Parse(str)).ToArray();
                } catch (FormatException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid Input!");
                    Console.WriteLine("Please enter the size in the following format: [WIDTH] x [LENGTH]");
                    Console.WriteLine("For example, to make a Rectangle of Width 2 and Lenght 4, enter the following:");
                    Console.WriteLine("2x4 or 2 x 4");
                    Console.ReadKey();
                    continue;
                }
                Rectangle tile = new Rectangle(tileSize[0], tileSize[1]);
                Rectangle container = new Rectangle(floorSize[0], floorSize[1]);
                Console.WriteLine($"{tile} best fills {container} with the following:");
                var fillGrouping = tile.Fill(container).GroupBy((rect) => rect);
                foreach (var group in fillGrouping)
                    Console.WriteLine($"{group.Count()} x {group.Key}");
                Console.ReadKey();
            }
        }

        public class Rectangle
        {
            public readonly double Width, Length;

            public Rectangle(double width, double length)
            {
                Width = width;
                Length = length;
            }

            public double Remainder (double outerWidth, double outerLength)
            {
                var widthRemainder = Width > outerWidth ? Width : (outerWidth / Width) % 1;
                var lengthRemainder = Length > outerLength ? Length : (outerLength / Length) % 1;
                return widthRemainder + lengthRemainder;
            }

            public bool IsWider (double width)
            {
                return Width > width;
            }

            public bool IsLonger (double length)
            {
                return Length > length;
            }

            public bool CanFitIn (Rectangle other)
            {
                return other.CanContain(this);
            }

            public bool CanContain (Rectangle other)
            {
                return !other.IsWider(Width) && !other.IsLonger(Length);
            }

            public IEnumerable<Rectangle> Fill (Rectangle outerRectangle)
            {
                var bestFit = BestFit(outerRectangle);
                if (bestFit.CanFitIn(outerRectangle))
                {
                    int fits = (int)Math.Floor(outerRectangle.Width / bestFit.Width);
                    for (int i = 0; i < fits; i++)
                        yield return bestFit;
                    Rectangle above = new Rectangle(outerRectangle.Width, outerRectangle.Length - bestFit.Length);
                    Rectangle beside = new Rectangle(outerRectangle.Width - (fits * bestFit.Width), bestFit.Length);
                    foreach (var fit in Fill(above))
                        yield return fit;
                    foreach (var fit in Fill(beside))
                        yield return fit;
                }
            }

            public Rectangle BestFit(Rectangle outerRectangle)
            {
                var remainder = Remainder(outerRectangle.Width, outerRectangle.Length);
                var rotated = new Rectangle(Length, Width);
                var rotatedRemainder = rotated.Remainder(outerRectangle.Width, outerRectangle.Length);
                return remainder < rotatedRemainder ? this : rotated;
            }

            public override bool Equals(object obj)
            {
                Rectangle other = obj as Rectangle;
                return other != null && other.Width == Width && other.Length == Length;
            }

            public override int GetHashCode()
            {
                return Width.GetHashCode() * (Length.GetHashCode() * 31);
            }

            public override string ToString()
            {
                return $"Rectangle ({Width}x{Length})";
            }
        }
    }
}
