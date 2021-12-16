using System;

namespace AoC.Library.Models
{
    public struct Point : IEquatable<Point>
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(string couple, string delimiter)
        {
            var split = couple.Split(delimiter);

            X = int.Parse(split[0]);
            Y = int.Parse(split[1]);
        }

        public int X { get; }
        public int Y { get; }

        public bool Equals(Point other) => other.X == X && other.Y == Y;

        public Point MoveCloserTo(Point to) => new Point(GetOneCloser(X,to.X), GetOneCloser(Y, to.Y));

        private int GetOneCloser(int from, int to)
        {
            if (from < to)
                return from + 1;
            else if (from > to)
                return from - 1;
            else
                return from;
        }
    }
}
