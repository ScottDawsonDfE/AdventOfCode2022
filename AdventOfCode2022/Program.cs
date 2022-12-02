// See https://aka.ms/new-console-template for more information

using AdventOfCode2022;
using AdventOfCode2022.Solutions;

Console.WriteLine("AoC 2022");
var testFilepath = @"C:\Users\sdawson1\OneDrive - Department for Education\Documents\2022AdventOfCode\test.txt";
var inputFilepath = @"C:\Users\sdawson1\OneDrive - Department for Education\Documents\2022AdventOfCode\input.txt";

//var testString = File.OpenText(testFilepath).ReadToEnd();
var mainString = File.OpenText(inputFilepath).ReadToEnd();

IAocSolution solution = new Day02();
var testResult = solution.RunSolution(File.OpenText(testFilepath).ReadToEnd());
var mainResult = solution.RunSolution(File.OpenText(inputFilepath).ReadToEnd());

Console.WriteLine("Test Results");
Console.WriteLine($"First Result {testResult.Result1}");
Console.WriteLine($"Second Result {testResult.Result2}");
Console.WriteLine("----");
Console.WriteLine("Main Results");
Console.WriteLine($"First Result {mainResult.Result1}");
Console.WriteLine($"Second Result {mainResult.Result2}");
