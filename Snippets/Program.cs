﻿using System;
using System.Linq;

namespace Snippets
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, string> f = i =>
            {
                var j = i % 1000;
                return Enumerable
                    .Range(0, 1000)
                    .Select(k => '0' + (char)(k * 2))
                    .ElementAt(j)
                    .ToString();
            };
            Func<string, int> g = s => int.Parse(s);
            var gof = f.Compose(g);
            Console.WriteLine(gof(333));
        }
    }

    public static class Exts
    {
        public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T1, T2> f, Func<T2, T3> g)
            => x => g(f(x));
    }
}
