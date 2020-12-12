using System;
using System.Drawing;

namespace _2020.Extensions
{
    public static class PointExtensions
    {
        private const double DegToRad = Math.PI / 180;

        public static Point Rotate(this Point v, int degrees)
        {
            return v.RotateRadians(degrees * DegToRad);
        }

        public static Point RotateRadians(this Point v, double radians)
        {
            var ca = Math.Cos(radians);
            var sa = Math.Sin(radians);
            return new Point(Convert.ToInt32(ca * v.X - sa * v.Y), Convert.ToInt32(sa * v.X + ca * v.Y));
        }
    }
}