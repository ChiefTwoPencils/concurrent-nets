using System;
using System.Linq;

namespace Snippets
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine((int) '0');
            Func<int, string> f = i =>
            {
                var j = i % 10;
                var collection = Enumerable
                    .Range(0, 10)
                    .Select(k => '0' + (char)k)
                    .ToList()
                    .ElementAt(j)
                    .ToString();
                return collection;
            };

            Func<string, int> g = s => int.Parse(s);

            var gof = f.Compose(g);
            Console.WriteLine(gof(110));
        }
    }

    public static class Exts
    {
        public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T1, T2> f, Func<T2, T3> g)
            => x => g(f(x));
    }
}
