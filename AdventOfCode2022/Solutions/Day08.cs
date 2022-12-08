namespace AdventOfCode2022.Solutions
{
    internal class Day08 : IAocSolution
    {
        public AocResult RunSolution(string input)
        {
            var lines = FileHelper.SplitIntoLines(input);
            var columnCount = lines[0].Length;

            List<Tree> allTrees = new List<Tree>();
            List<List<Tree>> rowsOfTrees = new();

            foreach (var line in lines)
            {
                var rowOfTrees = line.ToCharArray().Select(x => new Tree(x)).ToList();
                rowsOfTrees.Add(rowOfTrees);

                foreach (var tree in rowOfTrees)
                {
                    allTrees.Add(tree);
                }
            }

            foreach (var rowOfTrees in rowsOfTrees)
            {
                SetVisibilityOfTrees(rowOfTrees);
                CalculateTreesVisible(rowOfTrees);
                var reversedRow = rowOfTrees.ToList();
                reversedRow.Reverse();
                SetVisibilityOfTrees(reversedRow);
                CalculateTreesVisible(reversedRow);
            }

            for (int i = 0; i < columnCount; i++)
            {
                var columnOfTrees = rowsOfTrees.Select(x => x[i]);

                SetVisibilityOfTrees(columnOfTrees);
                CalculateTreesVisible(columnOfTrees);
                var reversedColumn = columnOfTrees.Reverse();
                SetVisibilityOfTrees(reversedColumn);
                CalculateTreesVisible(reversedColumn);
            }

            var bestView = allTrees.OrderByDescending(x => x.TreesVisibleScore).FirstOrDefault();

            AocResult result = new() { Result1 = allTrees.Where(x => x.IsVisible).Count().ToString(), Result2 = bestView?.TreesVisibleScore.ToString() };
            return result;

        }

        private static void SetVisibilityOfTrees(IEnumerable<Tree> rowOfTrees)
        {
            int highestTree = -1;

            foreach (var tree in rowOfTrees)
            {
                if (tree.Height > highestTree)
                {
                    highestTree = tree.Height;
                    tree.IsVisible = true;
                }
            }
        }

        private static void CalculateTreesVisible(IEnumerable<Tree> rowOfTrees)
        {
            var trees = rowOfTrees.ToArray();
            for (int i = 0; i < rowOfTrees.Count(); i++)
            {
                var tree = trees[i];
                var treesInDirection = trees.Skip(i + 1).ToList();
                if (treesInDirection is null || treesInDirection.Count() == 0)
                {
                    tree.AddTreesVisible(0);
                }
                else
                {
                    int treesSeen = 0;
                    bool viewBlocked = false;
                    while (!viewBlocked && treesSeen < treesInDirection.Count())
                    {
                        viewBlocked = treesInDirection[treesSeen].Height >= tree.Height;
                        treesSeen++;
                    }
                    tree.AddTreesVisible(treesSeen);
                }
            }
        }

        internal class Tree
        {
            public Tree(char height)
            {
                Height = int.Parse(height.ToString());
            }

            public int Height { get; set; }
            public bool IsVisible { get; set; }

            public int TreesVisibleScore { get; private set; } = 1;
            private List<int> _treesVisible = new List<int>();

            public void AddTreesVisible(int number)
            {
                _treesVisible.Add(number);
                TreesVisibleScore = TreesVisibleScore * number;
            }
        }
    }
}
