using System.Diagnostics;

namespace _2020.Models
{
    [DebuggerDisplay("{Position} {Sign}")]
    public class Pocket
    {
        public bool IsActive { get; set; }
        public char Sign => IsActive ? '#' : '.';

        public Vector Position { get; set; }

        public Pocket(bool isActive, Vector position)
        {
            IsActive = isActive;
            Position = position;
        }

        public Pocket(in char isActive, Vector position) : this(isActive == '#', position)
        {
        }
    }
}