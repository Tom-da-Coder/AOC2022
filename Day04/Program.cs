// See https://aka.ms/new-console-template for more information

using System.ComponentModel;

Console.WriteLine("04 dec, Hello, World!");

var input1 = File.ReadAllLines("input1.txt");
//var input2 = File.ReadAllLines("input2.txt");

Console.WriteLine(partA());
Console.WriteLine(partB());

int partA()
{
    int result = 0;
    foreach (var line in input1)
    {
        var duo = line.Split(',');
        if (contains(duo[0], duo[1]) || contains(duo[1], duo[0]))
            result++;
    }
    return result;

    bool contains(string input1, string input2)
    {
        var d1 = input1.Split("-").Select(v => int.Parse(v)).ToList();
        var d2 = input2.Split("-").Select(v => int.Parse(v)).ToList();
        return d1[0] <= d2[0] && d1[1] >= d2[1];
    }
}



int partB()
{
    int result = 0;
    foreach (var line in input1)
    {
        var duo = line.Split(',');
        if (contains(duo[0], duo[1]) || contains(duo[1], duo[0]))
            result++;
    }
    return result;

    bool contains(string input1, string input2)
    {
        var d1 = input1.Split("-").Select(v => int.Parse(v)).ToList();
        var d2 = input2.Split("-").Select(v => int.Parse(v)).ToList();
        return d1[0] <= d2[0] && d1[1] >= d2[0];
    }
}