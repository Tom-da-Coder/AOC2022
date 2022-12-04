// See https://aka.ms/new-console-template for more information

Console.WriteLine("03 dec, Hello, World!");

int result = 0;
var input1 = File.ReadAllLines("input1.txt");

var nums = input1.Select(line => GetPoints(line)).ToList();
result = nums.Sum();

//var input2 = File.ReadAllLines("input2.txt");

Console.WriteLine(result);
Console.WriteLine(partB());

int GetPoints(string s)
{
    var ps = new bool[52];
    int n = s.Length;
    for (int i = 0; i < n / 2; i++)
    {
        ps[points(s[i])] = true;
    }
    for (int i = n / 2; i < n; i++)
        if (ps[points(s[i])])
            return points(s[i]) + 1;
    throw new Exception();
}

int points(char code)

{
    return Char.IsLower(code) ? code - 'a' : code - 'A' + 26;
}

int partB()
{
    var result = 0;
    var lno = 0;
    while (lno < input1.Length)
    {
        ulong mask1 = input1[lno++].Select(x => 1UL << points(x)).Aggregate((a, b) => a | b);
        ulong mask2 = input1[lno++].Select(x => 1UL << points(x)).Aggregate((a, b) => a | b);
        ulong mask3 = input1[lno++].Select(x => 1UL << points(x)).Aggregate((a, b) => a | b);
        mask1 = mask1 & mask2 & mask3;
        for (int i = 0; i < 52; i++)
            if ((mask1 ^ (1ul << i)) == 0)
                result += i + 1;
    }
    return result;
}