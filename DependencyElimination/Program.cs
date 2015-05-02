using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DependencyElimination
{
    internal class Program
	{
        private static void Main(string[] args)
        {
            var logger = new Logger(Console.Out);
            var sw = Stopwatch.StartNew();
            var loader = new PageLoader(logger);
            var allLinks = new List<string>();
            var urls = loader.GetUrls(1, 6).ToArray();
            foreach (var url in urls)
            {
                var page = loader.GetPage(url);
                if (!page.Item2)
                    logger.Log(page.Item1);
                else
                {
                    var responseString = page.Item1;
                    var links = ResponseParser.Parse(responseString);
                    allLinks.AddRange(links);
                    logger.Log(url);
                    logger.Log(String.Format("found {0} links", links.Length));
                }
            }
            File.WriteAllLines("links.txt", allLinks);
            logger.Log(String.Format("Total links found: {0}", allLinks.Count));
			logger.Log("Finished");
			logger.Log(sw.Elapsed.ToString());
		}
	}
}