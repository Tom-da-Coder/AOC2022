// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.RegularExpressions;

Console.WriteLine("05 dec, Hello, World!");

var input1 = File.ReadAllLines("input1.txt");



//var input2 = File.ReadAllLines("input2.txt");

Console.WriteLine(partA());
Console.WriteLine(partB());

string partA()
{
    var q = new Stack<char>[9];
    for (int i = 0; i < 9; i++) q[i] = new Stack<char>();
    for (int i = 7; i >= 0; i--)
    {
        for (int k = 0; k < 9; k++)
        {
            var c = input1[i][k * 4 + 1];
            if (c != ' ')
                q[k].Push(c);
        }
    }
    var re = new Regex(@"move (\d+) from (\d+) to (\d+)");
    foreach (var lin in input1)
    {
        var mtc = re.Match(lin);
        if (mtc.Success)
        {
            var cnt = int.Parse(mtc.Groups[1].Value);
            var from = int.Parse(mtc.Groups[2].Value) - 1;
            var to = int.Parse(mtc.Groups[3].Value) - 1;
            for (int i = 0; i < cnt; i++)
            {
                char w = q[from].Pop();
                q[to].Push(w);
            }
        }
    }
    return new string(q.Select(n => (char)n.Peek()).ToArray());
}



string partB()
{
    var q = new StringBuilder[9];
    for (int i = 0; i < 9; i++) q[i] = new StringBuilder();
    for (int i = 7; i >= 0; i--)
    {
        for (int k = 0; k < 9; k++)
        {
            var c = input1[i][k * 4 + 1];
            if (c != ' ')
                q[k].Append(c);
        }
    }
    var re = new Regex(@"move (\d+) from (\d+) to (\d+)");
    foreach (var lin in input1)
    {
        var mtc = re.Match(lin);
        if (mtc.Success)
        {
            var cnt = int.Parse(mtc.Groups[1].Value);
            var from = int.Parse(mtc.Groups[2].Value) - 1;
            var to = int.Parse(mtc.Groups[3].Value) - 1;
            var s = q[from].ToString();
            s = s.Substring(s.Length - cnt);
            q[to].Append(s);
            q[from].Remove(q[from].Length - cnt, cnt);
        }
    }
    return new string(q.Select(n => n.ToString().Last()).ToArray());

    //var q = new Stack<char>[9];
    //for (int i = 0; i < 9; i++) q[i] = new Stack<char>();
    //for (int i = 7; i >= 0; i--)
    //{
    //    for (int k = 0; k < 9; k++)
    //    {
    //        var c = input1[i][k * 4 + 1];
    //        if (c != ' ')
    //            q[k].Push(c);
    //    }
    //}
    //var re = new Regex(@"move (\d+) from (\d+) to (\d+)");
    //foreach (var lin in input1)
    //{
    //    var mtc = re.Match(lin);
    //    if (mtc.Success)
    //    {
    //        var cnt = int.Parse(mtc.Groups[1].Value);
    //        var from = int.Parse(mtc.Groups[2].Value) - 1;
    //        var to = int.Parse(mtc.Groups[3].Value) - 1;
    //        for (int i = 0; i < cnt; i++)
    //        {
    //            char w = q[from].Pop();
    //            q[to].Push(w);
    //        }
    //    }
    //}
    //return new string(q.Select(n => (char)n.Peek()).ToArray());
}