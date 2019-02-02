using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MemCrawler.WebCrawlerNorm;

namespace MemCrawler
{
    class Program
    {
        /// <summary>
        /// Adds a cache to a func.
        /// </summary>
        /// <typeparam name="T">In type.</typeparam>
        /// <typeparam name="R">Return type.</typeparam>
        /// <param name="f">Function to memoize.</param>
        /// <returns>A memoized function.</returns>
        static Func<T, R> Memoize<T, R>(Func<T, R> f)
        where T : IComparable
        {
            var cache = new Dictionary<T, R>();
            return key => { 
                if (cache.ContainsKey(key))
                {
                    return cache[key];
                }
                return (cache[key] = f(key));
            };
        }

        /// <summary>
        /// Adds a cache to a func.
        /// </summary>
        /// <typeparam name="T">In type.</typeparam>
        /// <typeparam name="R">Return type.</typeparam>
        /// <param name="f">Function to memoize.</param>
        /// <returns>A memoized function.</returns>
        static Func<T, R> MemoizeThreadSafe<T, R>(Func<T, R> f)
        where T : IComparable
        {
            var cache = new ConcurrentDictionary<T, R>();
            return key => cache.GetOrAdd(key, k => f(k));
        }

        // TODO: !! To run this, uncomment this main and do comment the main in Seculation. !!
        //static void Main()
        //{
        //    var urls = new List<string>
        //    {
        //        @"http://www.google.com",
        //        @"http://www.microsoft.com",
        //        @"http://www.bing.com",
        //        @"http://www.google.com"
        //    };
        //    var watch = new Stopwatch();
        //    Console.WriteLine($"Starting basic crawl");
        //    watch.Start();
        //    Test(urls, WebCrawler);
        //    watch.Stop();
        //    Console.WriteLine($"Finshed basic in {watch.ElapsedMilliseconds / 1000}.\n\n");
        //    Console.WriteLine($"Starting mem-ed crawl");
        //    watch.Reset();
        //    watch.Start();
        //    Test(urls, MemoizeThreadSafe<string, IEnumerable<string>>(WebCrawler));
        //    watch.Stop();
        //    Console.WriteLine($"Finshed mem-ed in {watch.ElapsedMilliseconds / 1000}");
        //}

        static void Test(List<string> urls, Func<string, IEnumerable<string>> func)
        {
            urls.AsParallel()
                .SelectMany(url => func(url))
                .Select(page => ExtractWebPageTitle(page))
                .ToList()
                .ForEach(Console.WriteLine);
        }
    }
}
