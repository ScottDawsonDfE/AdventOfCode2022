namespace AdventOfCode2022
{
    internal static class FileHelper
    {
        public static string[] SplitIntoLines(string input, 
            StringSplitOptions options = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
        {
            input.Trim();
            string[] lineBreaks = { "\r\n", "\n", "\r" };
            return input.Split(lineBreaks, options);
        }
    }
}
