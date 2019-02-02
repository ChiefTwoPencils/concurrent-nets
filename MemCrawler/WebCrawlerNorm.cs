using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MemCrawler
{
    class WebCrawlerNorm
    {
        public static IEnumerable<string> WebCrawler(string url)
        {
            var content = GetWebContent(url);
            yield return content;
            foreach (var item in AnalyzeHtmlContent(content))
            {
                yield return GetWebContent(item);
            }
        }

        public static string GetWebContent(string url)
        {
            try
            {
                using (var wc = new WebClient())
                {
                    return wc.DownloadString(new Uri(url));
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static IEnumerable<string> AnalyzeHtmlContent(string text)
        {
            foreach(var url in regexLink.Matches(text))
            {
                yield return url.ToString();
            }
        }

        public static string ExtractWebPageTitle(string pageText)
        {
            if (regexTitle.IsMatch(pageText))
            {
                return regexTitle.Match(pageText).Groups["title"].Value;
            }
            return "No Page Title Found!";
        }

        public static readonly Regex regexLink =
            new Regex(@"(?<=href=('|""))https?://.*?(?=\1)");

        public static readonly Regex regexTitle =
            new Regex("<title>(?<title>.*?)<\\/title>", RegexOptions.Compiled);
    }
}
