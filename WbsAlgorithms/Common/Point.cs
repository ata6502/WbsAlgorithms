using System;

namespace WbsAlgorithms.Common
{
    public struct Point : IEquatable<Point>
    {
        public double X { get; }
        public double Y { get; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
            => $"({X:N1},{Y:N1})";

        public override bool Equals(object other)
        {
            if (!(other is Point)) 
                return false;
            return Equals((Point)other);
        }

        public bool Equals(Point other)
            => X == other.X && Y == other.Y;

        public override int GetHashCode()
            => HashCode.Combine(X, Y);

        public static bool operator ==(Point p1, Point p2) 
            => p1.Equals(p2);

        public static bool operator !=(Point p1, Point p2) 
            => !p1.Equals(p2);
    }
}
