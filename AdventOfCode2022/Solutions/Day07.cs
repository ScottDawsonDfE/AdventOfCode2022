using System.Text.RegularExpressions;

namespace AdventOfCode2022.Solutions
{
    internal partial class Day07 : IAocSolution
    {
        private const int _totalFileSpace = 70000000;
        private const int _requiredSpace = 30000000;
        private const string _changeDirUpCommand = @"$ cd ..";
        private const string _changeDirRoot = @"$ cd /";
        private const string _changeDirCommand = @"$ cd";
        private const string _listCommand = @"$ ls";
        private Regex _dirRegex = GetDirectoryRegex();
        private Regex _fileRegex = GetFileRegex();

        public AocResult RunSolution(string input)
        {
            var lines = FileHelper.SplitIntoLines(input);
            List<Directory> directories = new();
            Directory workingDirectory;
            Directory rootDirectory = new(@"/");
            directories.Add(rootDirectory);
            workingDirectory = rootDirectory;

            foreach (var line in lines)
            {
                if (line == _changeDirUpCommand)
                {
                    workingDirectory = workingDirectory.ParentDirectory ?? rootDirectory;
                }
                else if (line == _changeDirRoot)
                {
                    workingDirectory = rootDirectory;
                }
                else if (line.StartsWith(_changeDirCommand))
                {
                    var newDirName = line[_changeDirCommand.Length..].Trim();
                    workingDirectory = workingDirectory.ChildDirectories.Where(x => x.Name == newDirName).FirstOrDefault() ?? throw new Exception();
                }
                else if (line.StartsWith(_listCommand))
                {
                    //do nothing
                }
                else if (_dirRegex.Match(line).Success)
                {
                    var sections = line.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                    directories.Add(workingDirectory.CreateChildDirectory(sections[1]));
                }
                else if (_fileRegex.Match(line).Success)
                {
                    var sections = line.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                    workingDirectory.AddFile(new File(sections[1], int.Parse(sections[0])));
                }
            }

            var result1 = directories.Where(x => x.Size < 100000).Select(x => x.Size).Sum();

            var spaceTaken = rootDirectory.Size;
            var spaceRemaining = _totalFileSpace - spaceTaken;
            var extraSpaceRequired = _requiredSpace - spaceRemaining;

            var bestDirectoryToDelete = directories.Where(x => x.Size >= extraSpaceRequired).OrderBy(x => x.Size).FirstOrDefault() ?? throw new Exception();


            return new AocResult { Result1 = result1.ToString(), Result2 = bestDirectoryToDelete.Size.ToString() };

        }

        internal class Directory
        {
            public Directory(string name, Directory? parentDirectory = null)
            {
                Name = name;
                ParentDirectory = parentDirectory;
                Files = new List<File>();
                ChildDirectories = new List<Directory>();
            }

            public string Name { get; internal set; }
            public Directory? ParentDirectory { get; internal set; }
            public List<File> Files { get; private set; }
            public List<Directory> ChildDirectories { get; private set; }
            public int Size { get; private set; }

            public event EventHandler? RaiseSizeChangedEvent;

            private void OnRaiseSizeChanged(EventArgs e)
            {
                RaiseSizeChangedEvent?.Invoke(this, e);
            }

            private void HandleChildSizeChanged(object? sender, EventArgs e)
            {
                RecalculateSize();
            }

            private void RecalculateSize()
            {
                var filesSize = Files.Select(x => x.Size).Sum();
                var directoriesSize = ChildDirectories.Select(x => x.Size).Sum();
                Size = filesSize + directoriesSize;
                OnRaiseSizeChanged(new EventArgs());
            }

            public void AddFile(File file)
            {
                Files.Add(file);
                RecalculateSize();
            }

            public Directory CreateChildDirectory(string name)
            {
                Directory result = new Directory(name, this);
                ChildDirectories.Add(result);
                result.RaiseSizeChangedEvent += HandleChildSizeChanged;
                return result;
            }
        }

        public record File(string Name, int Size);

        [GeneratedRegex("^dir \\w+", RegexOptions.Compiled)]
        private static partial Regex GetDirectoryRegex();
        [GeneratedRegex("^\\d+ \\w+(.\\w+)?")]
        private static partial Regex GetFileRegex();
    }
}
