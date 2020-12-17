using _2020.Models;

namespace _2020.Extensions
{
    public static class VectorExtensions
    {
        public static Vector Offset(this Vector source, Vector offsetValue)
        {
            return new Vector(
                source.X + offsetValue.X,
                source.Y + offsetValue.Y,
                source.Z + offsetValue.Z,
                source.W + offsetValue.W
            );
        }
    }
}