namespace AdventOfCode2022
{
    internal static class FileHelper
    {
        public static string[] SplitIntoLines(string input)
        {
            input.Trim();
            string[] lineBreaks = { "\r\n", "\n", "\r" };
            return input.Split(lineBreaks, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }
    }
}
