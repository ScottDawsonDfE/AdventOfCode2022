using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Solutions
{
    internal class Day04 : IAocSolution
    {
        public AocResult RunSolution(string input)
        {
            List<ElfPair> elfPairs = FileHelper.SplitIntoLines(input).Select(x => new ElfPair(x)).ToList();

            AocResult result = new()
            {
                Result1 = elfPairs.Where(x => x.IsFullyContainingOneRange).Count().ToString(),
                Result2 = elfPairs.Where(x => x.IsPartialllyOverlapping).Count().ToString(),
            };
            return result;
        }

        internal class ElfPair
        {
            public ElfPair(string input)
            {
                var elfRangesString = input.Split(',');
                Elf1Range = SetRangeFromString(elfRangesString[0]);
                Elf2Range = SetRangeFromString(elfRangesString[1]);

                IsFullyContainingOneRange = ((Elf1Range.Start <= Elf2Range.Start) && (Elf1Range.End >= Elf2Range.End))
                    || ((Elf2Range.Start <= Elf1Range.Start) && (Elf2Range.End >= Elf1Range.End));

                IsPartialllyOverlapping = ((Elf1Range.Start <= Elf2Range.Start) && (Elf1Range.End >= Elf2Range.Start))
                    || ((Elf1Range.Start <= Elf2Range.End) && (Elf1Range.End >= Elf2Range.End))
                    || ((Elf2Range.Start <= Elf1Range.Start) && (Elf2Range.End >= Elf1Range.Start))
                    || ((Elf2Range.Start <= Elf1Range.End) && (Elf2Range.End >= Elf1Range.End));
            }

            private ElfRange SetRangeFromString(string input)
            {
                var values = input.Split('-');
                ElfRange result = new(int.Parse(values[0]), int.Parse(values[1]));
                return result;
            }

            public ElfRange Elf1Range { get; private set; }
            public ElfRange Elf2Range { get; private set; }
            public bool IsPartialllyOverlapping { get; private set; }
            public bool IsFullyContainingOneRange { get; private set; }
        }

        internal record ElfRange(int Start, int End);
    }
}
