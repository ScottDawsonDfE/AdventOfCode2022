namespace AdventOfCode2022.Solutions
{
    internal class Day06 : IAocSolution
    {
        public AocResult RunSolution(string input)
        {

            bool isMarkerFound = false;
            bool isMessageFound = false;
            int markerIndex = 0;
            int messageIndex = 0;

            for (int i = 0; !isMarkerFound; i++)
            {
                markerIndex = i;
                isMarkerFound = IsAllUniqueAtIndex(input, i, 4);
            }

            for (int i = 0; !isMessageFound; i++)
            {
                messageIndex = i;
                isMessageFound = IsAllUniqueAtIndex(input, i, 14);
            }

            return new AocResult() { Result1 = (markerIndex + 4).ToString(), Result2 = (messageIndex + 14).ToString() };
        }

        private static bool IsAllUniqueAtIndex(string input, int index, int length)
        {
            var characters = input.Substring(index, length).ToList().Distinct();
            return characters.Count() == length;
        }
    }
}
