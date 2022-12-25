// See https://aka.ms/new-console-template for more information

Console.WriteLine("25 dec, Hello, World!");

var input1 = File.ReadAllLines("input1.txt");

Console.WriteLine(ToSNAFU(input1.Select(ReadSNAFU).Sum()));

long ReadSNAFU(string input) => input.Select(ch => (long)(2 - "210-=".IndexOf(ch))).Aggregate<long>((a, b) => a * 5 + b);

string ToSNAFU(long val)
{
    var str = "";
    while (val > 0)
    {
        int dig = (int)(val % 5);
        val /= 5;
        if (dig > 2) { dig -= 5; val++; }
        str = ("=-012"[dig + 2]) + str;
    }
    return str;
}