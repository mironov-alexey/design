using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;

namespace DependencyElimination
{
    public class PageLoader
    {
        private readonly Logger _logger;
        public PageLoader(Logger logger)
        {
            _logger = logger;
        }

        public IEnumerable<string> GetUrls(int start, int end)
        {
            for (var pageIndex = start; pageIndex < end + 1; pageIndex++)
                yield return "http://habrahabr.ru/top/page" + pageIndex;
        }
        public HttpResponseMessage GetResponse(string url)
        {
            using (var http = new HttpClient())
                return http.GetAsync(url).Result;
        }

        public Tuple<string, bool> GetPage(string url)
        {
            var response = GetResponse(url);
            string responseString;
            if (response.IsSuccessStatusCode)
                responseString = response.Content.ReadAsStringAsync().Result;
            else
                responseString = "Error: " + response.StatusCode + " " + response.ReasonPhrase;
            return Tuple.Create(responseString, response.IsSuccessStatusCode);
        }
    }
}