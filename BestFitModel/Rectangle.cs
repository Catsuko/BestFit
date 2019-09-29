using System;
using System.Collections.Generic;

namespace BestFit.Model
{
    public class Rectangle
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

        public IEnumerable<FittedRectangle> Fill(Rectangle outerRectangle)
        {
            var bestFit = BestFit(outerRectangle, outerRectangle.Rotate());
            if (bestFit.Value.CanFitIn(bestFit.Key))
            {
                int fits = (int)Math.Floor(bestFit.Key.Width / bestFit.Value.Width);
                var vertical = bestFit.Key != outerRectangle;
                for (int i = 0; i < fits; i++)
                {
                    var x = vertical ? 0 : i * bestFit.Value.Width;
                    var y = vertical ? i * bestFit.Value.Width : 0;
                    var rect = vertical ? bestFit.Value.Rotate() : bestFit.Value;
                    yield return new FittedRectangle(rect, x, y);
                }
                Rectangle above = new Rectangle(outerRectangle.Width, outerRectangle.Length - (vertical ? bestFit.Value.Width * fits : bestFit.Value.Length));
                Rectangle beside = new Rectangle(outerRectangle.Width - ( vertical ? bestFit.Value.Length : fits * bestFit.Value.Width), outerRectangle.Length);
                foreach (var fit in Fill(above)) yield return vertical ? fit.Offset(bestFit.Value.Width * fits, 0) : fit.Offset(0, bestFit.Value.Length);
                foreach (var fit in Fill(beside)) yield return vertical ? fit.Offset(bestFit.Value.Length, 0) : fit.Offset(bestFit.Value.Width * fits, 0);
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
