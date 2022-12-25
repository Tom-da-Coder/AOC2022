// See https://aka.ms/new-console-template for more information

using System.Data.SqlTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

Console.WriteLine("13 dec, Hello, World!");

int result = 0;
var input1 = File.ReadAllLines("input1.txt");


using (var sr = new StreamReader("test.txt"))
{
    string leftline;
    while ((leftline = sr.ReadLine()) != null)
    {
        var rightline = sr.ReadLine();
        //Console.WriteLine($"{leftline}\r\n{rightline}\r\n{Compare(leftline, rightline)}\r\n");
        var t1 = ParseTree(new StringReader(leftline));
        var t2 = ParseTree(new StringReader(rightline));

        if (sr.ReadLine() == null)
            break;
    }
}

//var input2 = File.ReadAllLines("input2.txt");

Console.WriteLine(partA());
Console.WriteLine(partB());

int partA()
{
    int result = 0;
    foreach (var line in input1)
    {
    }
    return result;
}



int partB()
{
    int result = 0;
    foreach (var line in input1)
    {
    }
    return result;
}

//List<string> ParseTree(string input)
//{
//    StringReader sr = new StringReader(input);
//    if (input[0] == '[')

//}

object ParseTree(StringReader sr)
{
    object ret = null;
    int ch = sr.Read();
    while (ch >= 0)
    {
        if (ch == '[')
        {
            var s = ParseTree(sr);
            ch = sr.Read();
            if (ret is List<object> ls)
                ls.Add(s);
            else
                ret = s;
        }
        else if (ch == ']')
            return ret;
        else if (ch == ',')
        {
            if (ret is not List<object>)
                ret = new List<object>(new[] { ret });
            ch = sr.Read();
        }
        else if (char.IsDigit((char)ch))
        {
            int s = 0; while (char.IsDigit((char)ch)) { s = s * 10 + ch - '0'; ch = sr.Read(); }
            if (ret is List<object> ls)
                ls.Add(s);
            else
                ret = s;
        }
    }
    return ret;
}

static int Compare(string x, string y)
{
    if (x == y) return 0;
    int i = 0; int k = 0;
    char c1 = x[i++];
    char c2 = y[k++];
    while (true)
    {
        if (i == x.Length && k == y.Length)
            return 0;
        if (i == x.Length)
            return -1;
        if (k == y.Length)
            return 1;
        if (c1 == c2 && @"[],".Contains(c1))
        {
            c1 = x[i++];
            c2 = y[k++];
            continue;
        }
        if (char.IsDigit(c1) && char.IsDigit(c2))
        {
            int s1 = 0; while (char.IsDigit(c1 = x[i++])) s1 = s1 * 10 + c1 - '0';
            int s2 = 0; while (char.IsDigit(c2 = y[k++])) s2 = s2 * 10 + c2 - '0';
            if (s1 < s2) return -1;
            if (s1 > s2) return 1;
            continue;
        }
        if (c1 == ',' && c2 == ']')
            return 1;
        if (c1 == ']' && c2 == ',')
            return -1;
    }
}
