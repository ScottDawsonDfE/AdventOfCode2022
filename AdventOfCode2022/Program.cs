// See https://aka.ms/new-console-template for more information

using AdventOfCode2022;
using AdventOfCode2022.Solutions;
using Microsoft.VisualBasic.FileIO;

Console.WriteLine("AoC 2022");
Console.WriteLine("--------");
Console.WriteLine("Enter day number");
var dayNumberString = Console.ReadLine();
if (dayNumberString?.Length == 1)
{
    dayNumberString = "0" + dayNumberString;
}

var baseAddress = $"{SpecialDirectories.MyDocuments}\\2022AdventOfCode\\{dayNumberString}";
if (!Directory.Exists(baseAddress))
{
    return;
}

var testFilepath = baseAddress + "\\test.txt";
var inputFilepath = baseAddress + "\\input.txt";

var solutions = new Dictionary<string, IAocSolution>()
{
    { "01", new Day01() },
    { "02", new Day02() },
    { "03", new Day03() },
    { "04", new Day04() },
    { "05", new Day05() },
    { "06", new Day06() },
    { "07", new Day07() },
    { "08", new Day08() },
    //{ "09", new Day09() },
    //{ "10", new Day10() },
    //{ "11", new Day11() },
    //{ "12", new Day12() },
    //{ "13", new Day13() },
    //{ "14", new Day14() },
    //{ "15", new Day15() },
    //{ "16", new Day16() },
    //{ "17", new Day17() },
    //{ "18", new Day18() },
    //{ "19", new Day19() },
    //{ "20", new Day20() },
    //{ "21", new Day21() },
    //{ "22", new Day22() },
    //{ "23", new Day23() },
    //{ "24", new Day24() },
    //{ "25", new Day25() },
};

var solution = solutions.GetValueOrDefault(dayNumberString ?? "0");

if (solution == null)
{
    return;
}

Console.WriteLine("Working...");

var testResult = solution.RunSolution(File.OpenText(testFilepath).ReadToEnd());
var mainResult = solution.RunSolution(File.OpenText(inputFilepath).ReadToEnd());

Console.WriteLine("----");
Console.WriteLine("Test Results");
Console.WriteLine($"First Result {testResult.Result1}");
Console.WriteLine($"Second Result {testResult.Result2}");
Console.WriteLine("----");
Console.WriteLine("Main Results");
Console.WriteLine($"First Result {mainResult.Result1}");
Console.WriteLine($"Second Result {mainResult.Result2}");
