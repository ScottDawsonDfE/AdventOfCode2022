namespace AdventOfCode2022.Solutions
{
    internal class Day01 : IAocSolution
    {
        public AocResult RunSolution(string input)
        {
            string[] elfSplit = { "\r\n\r\n", "\n\n", "\r\r" };
            //string[] foodSplit = { "\r\n", "\n", "\r" };

            var elveStrings = input.Split(elfSplit, StringSplitOptions.RemoveEmptyEntries);

            var elves = new List<int>();

            foreach (var elveString in elveStrings)
            {
                elves.Add(FileHelper.SplitIntoLines(elveString).Select(int.Parse).Sum());
            }

            elves = elves.OrderByDescending(x => x).ToList();

            return new AocResult()
            {
                Result1 = elves.First().ToString(),
                Result2 = elves.Take(3).Sum().ToString()
            };
        }
    }
}
