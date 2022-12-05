namespace AdventOfCode2022.Solutions
{
    internal class Day03 : IAocSolution
    {
        public AocResult RunSolution(string input)
        {
            var backpacks = FileHelper.SplitIntoLines(input)
                .Select(x => new Backpack(x)).ToList();

            var numberOfGroups = backpacks.Count / 3;
            List<Group> groups = new List<Group>();

            for (int i = 0; i < numberOfGroups; i++)
            {
                var groupBackpacks = backpacks.Skip(i * 3).Take(3).ToList();

                groups.Add(new Group(groupBackpacks[0], groupBackpacks[1], groupBackpacks[2]));
            }

            var result = new AocResult()
            {
                Result1 = backpacks.Select(x => ((int)x.DuplicateItem)).Sum().ToString(),
                Result2 = groups.Select(x => ((int)x.CommonItem)).Sum().ToString(),
            };

            return result;
        }

        internal class Backpack
        {
            public Backpack(string input)
            {
                AllItems = new();

                foreach (var character in input.Trim())
                {
                    AllItems.Add((Item)Enum.Parse(typeof(Item), character.ToString()));
                }
                var pocketSize = AllItems.Count / 2;
                Pocket1 = AllItems.Take(pocketSize).Distinct().ToList();
                Pocket2 = AllItems.Skip(pocketSize).Distinct().ToList();

                DuplicateItem = Pocket1.Intersect(Pocket2).FirstOrDefault();
            }

            public List<Item> AllItems { get; private set; }
            public List<Item> Pocket1 { get; private set; }
            public List<Item> Pocket2 { get; private set; }
            public Item DuplicateItem { get; private set; }
        }

        internal enum Item
        {
            none = 0,
            a = 1, b, c, d, e, f, g, h, i, j, k, l,
            m, n, o, p, q, r, s, t, u, v, w, x, y, z,
            A = 27, B, C, D, E, F, G, H, I, J, K, L,
            M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
        }

        internal class Group
        {
            public Group(Backpack backpack1, Backpack backpack2, Backpack backpack3)
            {
                Backpack1 = backpack1;
                Backpack2 = backpack2;
                Backpack3 = backpack3;

                CommonItem = Backpack1.AllItems.Intersect(Backpack2.AllItems).Intersect(Backpack3.AllItems).FirstOrDefault();
            }

            public Backpack Backpack1 { get; private set; }
            public Backpack Backpack2 { get; private set; }
            public Backpack Backpack3 { get; private set; }
            public Item CommonItem { get; private set; }
        }
    }
}
