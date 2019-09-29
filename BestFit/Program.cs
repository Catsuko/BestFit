using BestFit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BestFit
{
    class Program
    {
        static void Main(string[] args)
        {
            var phone = new Rectangle(3, 2);
            var box = new Rectangle(6, 7);
            foreach (var fit in phone.Fill(box))
                Console.WriteLine(fit);
            Console.ReadKey();

            //while (true)
            //{
            //    Console.Clear();
            //    double[] phoneSize;
            //    double[] boxSize;

            //    try
            //    {
            //        phoneSize = CaptureDimensions("Phone Size?");
            //        boxSize = CaptureDimensions("Box Size?");
            //        if (phoneSize.Length != 3 || boxSize.Length != 3)
            //            throw new FormatException("Wrong Dimensions!");
            //    }
            //    catch (FormatException ex)
            //    {
            //        DisplayInvalidFormatMessage();
            //        continue;
            //    }

            //    DisplayBestFits(phoneSize, boxSize);
            //}
        }

        private static double[] CaptureDimensions(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine().Replace(" ", "").Split('x').Select(str => double.Parse(str)).ToArray();
        }

        private static void DisplayBestFits (double[] phone, double[] box)
        {
            Console.WriteLine();
            var best = DisplayPickedForOrientations(phone, box);
            foreach (var result in best)
                Console.WriteLine(result.ToString());

            Console.WriteLine($"TOTAL: {best.Sum(fr => fr.TotalPhones)}");
            Console.ReadKey();
        }

        private static IEnumerable<FitResults> DisplayPickedForOrientations (double[] phone, double[] box)
        {
            var allResults = new List<IEnumerable<FitResults>>();
            allResults.Add(DisplayAllPicked(new double[] { phone[0], phone[1], phone[2] }, box));
            allResults.Add(DisplayAllPicked(new double[] { phone[0], phone[2], phone[1] }, box));
            allResults.Add(DisplayAllPicked(new double[] { phone[1], phone[0], phone[2] }, box));
            allResults.Add(DisplayAllPicked(new double[] { phone[1], phone[2], phone[0] }, box));
            allResults.Add(DisplayAllPicked(new double[] { phone[2], phone[1], phone[0] }, box));
            allResults.Add(DisplayAllPicked(new double[] { phone[2], phone[0], phone[1] }, box));
            return allResults.OrderBy(r => r.Sum(fr => fr.TotalPhones)).Last();
        }

        private static IEnumerable<FitResults> DisplayAllPicked(double[] phone, double[] box)
        {
            var results = new List<FitResults>();
            var phoneRect = new Rectangle(phone[1], phone[2]);
            var boxRect = new Rectangle(box[0], box[1]);
            var fitResults = new FitResults(phone, box, phoneRect.Fill(boxRect));
            if(fitResults.TotalPhones > 0)
            {
                results.Add(fitResults);
                results.AddRange(DisplayPickedForOrientations(phone, fitResults.RemainingSpace));
            }

            return results;
        }

        private static void DisplayInvalidFormatMessage ()
        {
            Console.WriteLine();
            Console.WriteLine("Invalid Input!");
            Console.WriteLine("Please enter the size in the following format: [WIDTH] x [LENGTH] x [DEPTH]");
            Console.WriteLine("For example, to make a Rectangular Prism of Width 2, Lenght 4 and Depth 1, enter the following:");
            Console.WriteLine("2x4x1 or 2 x 4 x 1");
            Console.ReadKey();
        }

        public class FitResults
        {
            private readonly double[] _phone, _space;
            private readonly IEnumerable<FittedRectangle> _results;

            public int TotalPhones { get { return LayerCount * PhonesInLayer; } }
            public int PhonesInLayer { get { return _results.Count(); } }
            public int LayerCount { get { return (int)Math.Floor(_space[2] / _phone[0]); } }
            public double[] RemainingSpace { get { return new double[] { _space[0], _space[1], _space[2] - (LayerCount * _phone[0]) }; } }

            public FitResults(double[] phone, double[] space, IEnumerable<FittedRectangle> results)
            {
                _phone = phone;
                _space = space;
                _results = results;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                var remainingSpace = RemainingSpace;
                sb.AppendLine($"{_phone[0]}x{_phone[1]}x{_phone[2]} => {_space[0]}x{_space[1]}x{_space[2]}");
                sb.AppendLine($"Fitted with {LayerCount} layers of {PhonesInLayer}");
                sb.AppendLine();
                sb.AppendLine("Chosen Arrangements:");
                var fillGrouping = _results.GroupBy((rect) => rect);
                foreach (var group in fillGrouping)
                    sb.AppendLine($"     {group.Count()} x {group.Key}");
                sb.AppendLine();
                sb.AppendLine($"Total Phones {TotalPhones}");
                sb.AppendLine($"Remaining Distance from Top: {remainingSpace[2]}");
                sb.AppendLine();
                return sb.ToString();
            }
        }
    }
}