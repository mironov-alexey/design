using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace DependencyElimination
{
    public class ResponseParser
    {
        public static string[] Parse(string response)
        {
            var matches = Regex.Matches(response, @"\Whref=[""'](.*?)[""'\s>]").Cast<Match>();
            return matches.Select(match => match.Groups[1].Value).ToArray();
//            return links;
        }
    }
}