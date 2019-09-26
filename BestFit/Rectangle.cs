using System;
using System.Collections.Generic;

namespace BestFit
{
    internal class Rectangle
    {
        public readonly double Width, Length;

        public Rectangle(double width, double length)
        {
            Width = width;
            Length = length;
        }

        public double Remainder(double outerWidth, double outerLength)
        {
            return Width > outerWidth || Length > outerLength ? Width : (outerWidth / Width) % 1;
        }

        public bool IsWider(double width)
        {
            return Width > width;
        }

        public bool IsLonger(double length)
        {
            return Length > length;
        }

        public bool CanFitIn(Rectangle other)
        {
            return other.CanContain(this);
        }

        public bool CanContain(Rectangle other)
        {
            return !other.IsWider(Width) && !other.IsLonger(Length);
        }

        public IEnumerable<Rectangle> Fill(Rectangle outerRectangle, Action<string> log = null)
        {
            var bestFit = BestFit(outerRectangle, outerRectangle.Rotate());
            if (bestFit.Value.CanFitIn(bestFit.Key))
            {
                int fits = (int)Math.Floor(bestFit.Key.Width / bestFit.Value.Width);
                log?.Invoke($"Fitted {fits} {bestFit.Value} into {bestFit.Key}");
                for (int i = 0; i < fits; i++) yield return bestFit.Value;
                Rectangle above = new Rectangle(bestFit.Key.Width, bestFit.Key.Length - bestFit.Value.Length);
                Rectangle beside = new Rectangle(bestFit.Key.Width - (fits * bestFit.Value.Width), bestFit.Value.Length);
                foreach (var fit in Fill(above, log)) yield return fit;
                foreach (var fit in Fill(beside, log)) yield return fit;
            }
        }

        public KeyValuePair<Rectangle, Rectangle> BestFit(params Rectangle[] outerRectangles)
        {
            double lowestRemainder = Width + Length;
            List<KeyValuePair<Rectangle, Rectangle>> winningFits = new List<KeyValuePair<Rectangle, Rectangle>>();

            foreach(var outer in outerRectangles)
            {
                var remainder = Remainder(outer.Width, outer.Length);
                var rotated = Rotate();
                var rotatedRemainder = rotated.Remainder(outer.Width, outer.Length);
                var lowerRemainder = Math.Min(remainder, rotatedRemainder);
                if(lowerRemainder <= lowestRemainder)
                {
                    lowestRemainder = lowerRemainder;
                    winningFits.Add(new KeyValuePair<Rectangle, Rectangle>(outer, remainder < rotatedRemainder ? this : rotated));
                }
            }

            return winningFits[winningFits.Count - 1];
        }

        public Rectangle Rotate ()
        {
            return new Rectangle(Length, Width);
        }

        public override bool Equals(object obj)
        {
            return obj is Rectangle other && other.Width == Width && other.Length == Length;
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
