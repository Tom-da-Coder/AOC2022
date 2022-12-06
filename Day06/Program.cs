// See https://aka.ms/new-console-template for more information

using System.Runtime.ExceptionServices;

Console.WriteLine("06 dec, Hello, World!");

var inp = File.ReadAllText("input1.txt");



//var input2 = File.ReadAllLines("input2.txt");

Console.WriteLine(partA());
Console.WriteLine(partB());

int partA()
{
    int result = 0;
    for (int i = 4; i < inp.Length; i++)
    {
        if (check(inp[i], inp[i - 1], inp[i - 2], inp[i - 3]))
            return i + 1;
    }
    return -1;

    bool check(char A, char B, char C, char D)
    {
        return A != B && A != C && A != D && B != C && B != D && C != D;
    }
}



int partB()
{
    int n = 14;
    for (int i = n; i < inp.Length; i++)
    {
        if (test(i))
            return i + 1;
    }
    return -1;

    bool test(int i)
    {
        var bin = new bool[26];
        for (int k = i - n + 1; k <= i; k++)
        {
            var c = inp[k] - 'a';
            if (bin[c])
                return false;
            bin[c] = true;
        }
        return true;
    }
}