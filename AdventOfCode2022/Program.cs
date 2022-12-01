// See https://aka.ms/new-console-template for more information

using AdventOfCode2022;
using AdventOfCode2022.Solutions;

Console.WriteLine("Enter filepath");
var filepath = Console.ReadLine();

if (filepath is null)
{
    return;
}

var filestream = File.OpenText(filepath);
var str = filestream.ReadToEnd();

IAocSolution solution = new Day01();
var result = solution.RunSolution(str);

Console.WriteLine($"First Result {result.Result1}");
Console.WriteLine($"Second Result {result.Result2}");
