using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Solutions
{
    internal class Day02 : IAocSolution
    {
        public AocResult RunSolution(string input)
        {
            List<Round> roundsV1 = new List<Round>();
            List<Round> roundsV2 = new List<Round>();
            var lines = FileHelper.SplitIntoLines(input);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split(" ");
                roundsV1.Add(new Round(ConvertToRps(parts[0]), ConvertToRps(parts[1])));
                roundsV2.Add(new Round(ConvertToRps(parts[0]), ConvertToRoundResult(parts[1])));
            }

            AocResult result = new() 
            { 
                Result1 = roundsV1.Sum(x => x.Score).ToString(), 
                Result2 = roundsV2.Sum(x => x.Score).ToString()
            };
            return result;
        }

        private enum RPS
        {
            None = 0,
            Rock = 1,
            Paper = 2, 
            Scissors = 3,
        }

        private static RPS ConvertToRps(string value)
        {
            var RockOpponent = "A";
            var PaperOpponent = "B";
            var ScissorsOpponent = "C";
            var RockMe = "X";
            var PaperkMe = "Y";
            var ScissorsMe = "Z";

            if (value == RockOpponent || value == RockMe)
            {
                return RPS.Rock;
            }
            if (value == PaperOpponent || value == PaperkMe)
            {
                return RPS.Paper;
            }
            if (value == ScissorsOpponent || value == ScissorsMe)
            {
                return RPS.Scissors;
            }
            return RPS.None;
        }

        private static RoundResult ConvertToRoundResult(string value)
        {
            var win = "Z";
            var lose = "X";
            var draw = "Y";

            if (value == win)
            {
                return RoundResult.Win;
            }
            if (value == lose)
            {
                return RoundResult.Loss;
            }
            if (value == draw)
            {
                return RoundResult.Draw;
            }
            return RoundResult.Loss;
        }



        private enum RoundResult
        {
            Loss = 0,
            Draw = 3,
            Win = 6,
        }


        private class Round
        {
            public Round(RPS opponentMove, RPS myMove)
            {
                OpponentMove= opponentMove;
                MyMove= myMove;
                RoundResult = CalculateRoundResult(opponentMove, myMove);
                Score = ((int)RoundResult) + ((int)MyMove);
            }

            public Round(RPS opponentMove, RoundResult roundResult)
            {
                OpponentMove = opponentMove;
                RoundResult = roundResult;
                MyMove = CalculateMyMove(opponentMove, roundResult);
                Score = ((int)RoundResult) + ((int)MyMove);
            }

            public RPS OpponentMove { get; private set; }
            public RPS MyMove { get; private set; }
            public RoundResult RoundResult { get; private set; }
            public int Score { get; private set; }

            private RoundResult CalculateRoundResult(RPS opponentMove, RPS myMove)
            {
                if (opponentMove == RPS.Rock)
                {
                    if (myMove == RPS.Rock)
                    {
                        return RoundResult.Draw;
                    }
                    else if (MyMove == RPS.Paper)
                    {
                        return RoundResult.Win;
                    }
                    else if (MyMove == RPS.Scissors)
                    {
                        return RoundResult.Loss;
                    }
                }
                else if (opponentMove == RPS.Paper)
                {
                    if (myMove == RPS.Rock)
                    {
                        return RoundResult.Loss;
                    }
                    else if (MyMove == RPS.Paper)
                    {
                        return RoundResult.Draw;
                    }
                    else if (MyMove == RPS.Scissors)
                    {
                        return RoundResult.Win;
                    }
                }
                else if (opponentMove == RPS.Scissors )
                {
                    if (myMove == RPS.Rock)
                    {
                        return RoundResult.Win;
                    }
                    else if (MyMove == RPS.Paper)
                    {
                        return RoundResult.Loss;
                    }
                    else if (MyMove == RPS.Scissors)
                    {
                        return RoundResult.Draw;
                    }
                }
                return RoundResult.Loss;
            }

            private RPS CalculateMyMove(RPS opponentMove, RoundResult roundResult) 
            {
                if (opponentMove == RPS.Rock)
                {
                    if (roundResult == RoundResult.Win)
                    {
                        return RPS.Paper;
                    }
                    else if (roundResult == RoundResult.Loss)
                    {
                        return RPS.Scissors;
                    }
                    else if (roundResult == RoundResult.Draw)
                    {
                        return RPS.Rock;
                    }
                }
                else if (opponentMove == RPS.Paper)
                {
                    if (roundResult == RoundResult.Win)
                    {
                        return RPS.Scissors;
                    }
                    else if (roundResult == RoundResult.Loss)
                    {
                        return RPS.Rock;
                    }
                    else if (roundResult == RoundResult.Draw)
                    {
                        return RPS.Paper;
                    }
                }
                else if (opponentMove == RPS.Scissors)
                {
                    if (roundResult == RoundResult.Win)
                    {
                        return RPS.Rock;
                    }
                    else if (roundResult == RoundResult.Loss)
                    {
                        return RPS.Paper;
                    }
                    else if (roundResult == RoundResult.Draw)
                    {
                        return RPS.Scissors;
                    }
                }
                return RPS.None;
            }
        }
    }


}
