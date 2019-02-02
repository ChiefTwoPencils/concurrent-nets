using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MemCrawler
{
    class SimResult
    {
        public SimResult(string word, int distance)
        {
            Word = word;
            Distance = distance;
        }
        public string Word { get; set; }
        public int Distance { get; set; }
    }

    class Speculation
    {
        public static string FuzzyMatch(List<string> words, string word)
        {
            var wordSet = new HashSet<string>(words);
            var bestMatch = words.AsParallel()
                .Select(w => RateSimilarity(w, word))
                .OrderByDescending(r => r.Distance)
                .Select(r => r.Word)
                .FirstOrDefault();
            return bestMatch;
        }

        public static Func<string, string> PartialFuzzyMatch(List<string> words)
        {
            var wordSet = new HashSet<string>(words);
            return word =>
            {
                var bestMatch = words.AsParallel()
                    .Select(w => RateSimilarity(w, word))
                    .OrderByDescending(r => r.Distance)
                    .Select(r => r.Word)
                    .FirstOrDefault();
                return bestMatch;
            };
        }

        static SimResult RateSimilarity(string search, string word)
        {
            return new SimResult(word, search.CompareTo(word));
        }

        public static void Main()
        {
            var words = File.ReadAllLines(@"../../words.txt").ToList();
            var watch = new Stopwatch();
            watch.Start();
            words.ForEach(w =>
            {
                var r = FuzzyMatch(words, w);
                //Console.WriteLine(r);
            });
            watch.Stop();
            Console.WriteLine($"Finished basic in {watch.ElapsedMilliseconds / 1000} seconds.");
            watch.Restart();
            var fastFuzzy = PartialFuzzyMatch(words);
            words.ForEach(w =>
            {
                var r = fastFuzzy(w);
                //Console.WriteLine(r);
            });
            watch.Stop();
            Console.WriteLine($"Finished speculative in {watch.ElapsedMilliseconds / 1000} seconds.");
        }
    }
}
