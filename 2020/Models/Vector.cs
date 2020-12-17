using System.Diagnostics;

namespace _2020.Models
{
    [DebuggerDisplay("{X},{Y},{Z},{W}")]
    public struct Vector
    {
        public int X;
        public int Y;
        public int Z;
        public int W;

        public Vector(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}