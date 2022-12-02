using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal static class FileHelper
    {
        public static string[] SplitIntoLines(string input)
        {
            string[] lineBreaks = { "\r\n", "\n", "\r" };
            return input.Split(lineBreaks, StringSplitOptions.TrimEntries);
        }
    }
}
