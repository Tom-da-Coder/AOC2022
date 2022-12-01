// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

int day = 1;
Console.WriteLine("01 dec, Hello, World!");

var input1 = File.ReadAllLines("input1.txt");
var elves = new List<int>();

int sum = 0;
foreach (var elve in input1)
{
    if (elve.Length == 0)
    {
        elves.Add(sum);
        sum = 0;
    }
    else
    {
        sum += int.Parse(elve);
    }
}
Console.WriteLine(elves.Max());
Console.WriteLine(elves.OrderByDescending(c => c).Take(3).Sum());

//var input2 = File.ReadAllLines("input2.txt");
