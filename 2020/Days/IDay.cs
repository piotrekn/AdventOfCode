namespace _2020.Days
{
    public interface IDay<out T>
    {
        public T Part1(string[] input);

        public T Part2(string[] input);
    }
    public interface IDay<out T, in U>
    {
        public T Part1(U input);

        public T Part2(U input);
    }
}
