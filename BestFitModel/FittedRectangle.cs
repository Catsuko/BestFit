using System;

namespace BestFit.Model
{
    public class FittedRectangle
    {
        private readonly Rectangle _rectangle;
        private readonly double _x, _y;

        public FittedRectangle(Rectangle rectangle, double x, double y)
        {
            _rectangle = rectangle;
            _x = x;
            _y = y;
        }

        public FittedRectangle Offset (double x, double y)
        {
            return new FittedRectangle(_rectangle, _x + x, _y + y);
        }

        public FittedRectangle Rotate ()
        {
            return new FittedRectangle(_rectangle.Rotate(), _y, _x);
        }

        public void Print (Action<Rectangle, double, double> media)
        {
            media.Invoke(_rectangle, _x, _y);
        }

        public override string ToString()
        {
            return $"{_rectangle} at {_x}, {_y}";
        }
    }
}
