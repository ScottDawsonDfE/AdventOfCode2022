using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Solutions
{
    internal class Day05 : IAocSolution
    {
        public AocResult RunSolution(string input)
        {
            var lines = FileHelper.SplitIntoLines(input, StringSplitOptions.RemoveEmptyEntries);

            var stackLines = lines.Where(x => x.Contains('[')).ToList();
            var stackNumbers = lines.Where(x => x.Contains(" 1   2")).FirstOrDefault();
            var instructions = lines.Where(x => x.Contains("move")).ToList();

            var stackCount = stackNumbers?.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.RemoveEmptyEntries).Length ?? 0;
            List<Stack<string>> stacks = new();

            for (var i = 0; i < stackCount; i++)
            {
                stacks.Add(GetStack(i, stackLines));
            }

            var stacks1 = CloneStacks(stacks);
            var stacks2 = CloneStacks(stacks);
            foreach(var instruction in instructions)
            {
                stacks1 = CarryOutInstruction(instruction, stacks1);
                stacks2 = CarryOutInstructionStacked(instruction, stacks2);
            }
            
            var result = new AocResult()
            {
                Result1 = StacksToString(stacks1),
                Result2 = StacksToString(stacks2),
            };
            return result;
        }

        private string StacksToString(List<Stack<string>> stacks)
        {

            StringBuilder stringBuilder = new();
            foreach (var stack in stacks)
            {
                if (stack.TryPeek(out var item))
                {
                    stringBuilder.Append(item);
                }
                else
                {
                    stringBuilder.Append(" ");
                }
            }
            return stringBuilder.ToString();
        }

        private Stack<string> GetStack(int stackIndex, IEnumerable<string> stackLines)
        {
            var upsideDownStack = new Stack<string>();

            foreach (var stackLine in stackLines)
            {
                var value = stackLine.PadRight(stackIndex * 4 + 4).Substring(stackIndex * 4, 4).Replace("[", string.Empty).Replace("]", string.Empty).Trim();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    upsideDownStack.Push(value);
                }
            }

            var correctWayRoundStack = new Stack<string>();
            while (upsideDownStack.Count > 0)
            {
                correctWayRoundStack.Push(upsideDownStack.Pop());
            }

            return correctWayRoundStack;
        }

        private List<Stack<string>> CarryOutInstruction(string instruction, List<Stack<string>> stacks)
        {
            var workingStacks = CloneStacks(stacks);

            var instructionParts = instruction.Split(' ');
            int itemsToMove = int.Parse(instructionParts[1]);
            int moveFromStackIndex = int.Parse(instructionParts[3]) - 1;
            int moveToStackIndex = int.Parse(instructionParts[5]) - 1;

            for(var i = 0; i < itemsToMove; i++)
            {
                if (workingStacks[moveFromStackIndex].Count > 0)
                {
                    workingStacks[moveToStackIndex].Push(workingStacks[moveFromStackIndex].Pop());
                }
                else
                {
                    Debug.Print("OhNo!");
                }
            }
            return workingStacks;
        }
        private List<Stack<string>> CarryOutInstructionStacked(string instruction, List<Stack<string>> stacks)
        {
            //clone the stack
            var workingStacks = CloneStacks(stacks);

            var instructionParts = instruction.Split(' ');
            int itemsToMove = int.Parse(instructionParts[1]);
            int moveFromStackIndex = int.Parse(instructionParts[3]) - 1;
            int moveToStackIndex = int.Parse(instructionParts[5]) - 1;

            Stack<string> temporaryInvertedStack = new Stack<string>();

            for (var i = 0; i < itemsToMove; i++)
            {
                if (workingStacks[moveFromStackIndex].Count > 0)
                {
                    temporaryInvertedStack.Push(workingStacks[moveFromStackIndex].Pop());
                }
                else
                {
                    Debug.Print("OhNo!");
                }
            }

            while(temporaryInvertedStack.Count > 0)
            {
                workingStacks[moveToStackIndex].Push(temporaryInvertedStack.Pop());
            }

            return workingStacks;
        }

        public List<Stack<string>> CloneStacks(List<Stack<string>> stacks)
        {
            var result = new List<Stack<string>>();
            foreach (var stack in stacks)
            {
                result.Add(new Stack<string>(stack.Reverse()));
            }
            return result;
        }
    }

}
