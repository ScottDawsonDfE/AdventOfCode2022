namespace AdventOfCode2022.Solutions
{
    internal class Day09 : IAocSolution
    {
        public AocResult RunSolution(string input)
        {
            var lines = FileHelper.SplitIntoLines(input);
            var shortSnake = new Snake();
            var longSnake = new Snake(9);
            foreach (var line in lines)
            {
                var instructionParts = line.Split(' ');
                shortSnake.MoveSnake(instructionParts[0], int.Parse(instructionParts[1]));
                longSnake.MoveSnake(instructionParts[0], int.Parse(instructionParts[1]));
            }

            return new AocResult()
            {
                Result1 = shortSnake.RearTailCoordinates.Distinct().Count().ToString(),
                Result2 = longSnake.RearTailCoordinates.Distinct().Count().ToString()
            };
        }

        internal class Snake
        {
            private int _tailLength;
            public Snake(int tailLength = 1)
            {
                _tailLength = tailLength;
                CurrentTailCoordinates = new List<TailCoordinate>();
                for (int i = 0; i < tailLength; i++)
                {
                    CurrentTailCoordinates.Add(new TailCoordinate(0, 0));
                }
                RearTailCoordinates.Add(CurrentTailCoordinates[_tailLength - 1]);
            }

            public HeadCoordinate CurrentHeadCoordinate { get; set; } = new(0, 0);
            public List<TailCoordinate> CurrentTailCoordinates { get; set; }

            public ICollection<Coordinate> RearTailCoordinates { get; set; } = new List<Coordinate>();

            public void MoveSnake(string direction, int distance)
            {
                for (int i = 0; i < distance; i++)
                {
                    switch (direction)
                    {
                        case "U":
                            CurrentHeadCoordinate = CurrentHeadCoordinate.MoveUp();
                            break;
                        case "D":
                            CurrentHeadCoordinate = CurrentHeadCoordinate.MoveDown();
                            break;
                        case "R":
                            CurrentHeadCoordinate = CurrentHeadCoordinate.MoveRight();
                            break;
                        case "L":
                            CurrentHeadCoordinate = CurrentHeadCoordinate.MoveLeft();
                            break;
                    }
                    for (int m = 0; m < _tailLength; m++)
                    {
                        int x, y;
                        if (m == 0)
                        {
                            x = CurrentHeadCoordinate.X;
                            y = CurrentHeadCoordinate.Y;
                        }
                        else
                        {
                            x = CurrentTailCoordinates[m - 1].X;
                            y = CurrentTailCoordinates[m - 1].Y;
                        }
                        CurrentTailCoordinates[m] = CurrentTailCoordinates[m].Chase(x, y);
                    }
                    RearTailCoordinates.Add(CurrentTailCoordinates[_tailLength - 1]);
                }
            }
        }

        internal record Coordinate
        {
            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; private set; }
            public int Y { get; private set; }
        }

        internal record HeadCoordinate : Coordinate
        {
            public HeadCoordinate(int x, int y) : base(x, y)
            {
            }

            public HeadCoordinate MoveUp() { return new HeadCoordinate(X, Y + 1); }
            public HeadCoordinate MoveDown() { return new HeadCoordinate(X, Y - 1); }
            public HeadCoordinate MoveRight() { return new HeadCoordinate(X + 1, Y); }
            public HeadCoordinate MoveLeft() { return new HeadCoordinate(X - 1, Y); }
        }

        internal record TailCoordinate : Coordinate
        {
            public TailCoordinate(int x, int y) : base(x, y)
            {
            }

            public TailCoordinate Chase(int x, int y)
            {
                var xDiff = x - X;
                var yDiff = y - Y;

                if (((xDiff >= -1) && (xDiff <= 1)) && ((yDiff >= -1) && (yDiff <= 1)))
                {
                    return new TailCoordinate(X, Y);
                }
                var x1 = X;
                var y1 = Y;
                if (xDiff < 0)
                {
                    x1--;
                }
                else if (xDiff > 0)
                {
                    x1++;
                }
                if (yDiff < 0)
                {
                    y1--;
                }
                else if (yDiff > 0)
                {
                    y1++;
                }
                return new TailCoordinate(x1, y1);
            }
        }
    }
}
