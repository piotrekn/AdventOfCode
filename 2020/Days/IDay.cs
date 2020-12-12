namespace _2020.Days
{
    /*
     * Bunch of interfaces for day bootstrapping
     */
    public interface IDay<out T>
    {
        public T Part1(string[] input);

        public T Part2(string[] input);
    }

    public interface IDay<out T, in TData>
    {
        public T Part1(TData input);

        public T Part2(TData input);
    }

    public interface IDayWithParameter<out T, in TData, in TParam>
    {
        public T Part1(TData input, TParam parameter);

        public T Part2(TData input, TParam parameter);
    }
}
