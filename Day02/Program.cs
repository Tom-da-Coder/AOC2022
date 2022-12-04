// See https://aka.ms/new-console-template for more information

Console.WriteLine("02 dec, Hello, World!");

int result = 0;
var input1 = File.ReadAllLines("input1.txt");

foreach (var line in input1)
{
    switch (line)
    {
        case "A X": result += 3 + 0; break;
        case "A Y": result += 1 + 3; break;
        case "A Z": result += 2 + 6; break;

        case "B X": result += 1 + 0; break;
        case "B Y": result += 2 + 3; break;
        case "B Z": result += 3 + 6; break;

        case "C X": result += 2 + 0; break;
        case "C Y": result += 3 + 3; break;
        case "C Z": result += 1 + 6; break;

    }
}


//var input2 = File.ReadAllLines("input2.txt");

Console.WriteLine(result);
