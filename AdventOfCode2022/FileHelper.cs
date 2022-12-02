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
