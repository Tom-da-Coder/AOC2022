// See https://aka.ms/new-console-template for more information
#define READALL
using System.Text;

Console.WriteLine("25 dec, Hello, World!");

int result = 0;

var input1 = File.ReadAllLines("input1.txt");

Console.WriteLine(partA());
Console.WriteLine(partB());

string partA()
{
    long result = input1.Select(ReadSNAFU).Sum();

    return ToSNAFU(result);
}



int partB()
{
    int result = 0;
    foreach (var line in input1)
    {
    }
    return result;
}

long ReadSNAFU(string input)
{
    long result = 0;
    foreach (var ch in input)
        result = result * 5 + 2 - "210-=".IndexOf(ch);
    return result;
}

string ToSNAFU(long val)
{
    List<int> digits = new List<int>();
    while (val > 0)
    {
        digits.Add((int)(val % 5));
        val /= 5;
    }
    digits.Add(0);
    for (int i = 0; i < digits.Count; i++)
    {
        if (digits[i] > 2)
        {
            digits[i] -= 5;
            digits[i + 1]++;
        }
    }
    var sb = new StringBuilder();
    digits.Reverse();
    if (digits[0] == 0) digits.RemoveAt(0);
    digits.Select(c => "=-012"[c + 2]).ToList().ForEach(c => sb.Append(c));
    return sb.ToString();
}