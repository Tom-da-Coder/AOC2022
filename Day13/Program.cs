// See https://aka.ms/new-console-template for more information

using System.Data.SqlTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

Console.WriteLine("13 dec, Hello, World!");

int result = 0;
var input1 = File.ReadAllLines("input1.txt");


using (var sr = new StreamReader("input1.txt"))
{
    int index = 0;
    string leftline;
    while ((leftline = sr.ReadLine()) != null)
    {
        index++;
        var rightline = sr.ReadLine();
        var t1 = ParseTree(new StringReader(leftline));
        var t2 = ParseTree(new StringReader(rightline));
        var cmp = CompareTree(t1, t2);
        if (cmp < 0)
            result += index;
        //Console.WriteLine($"{leftline}\r\n{rightline}\r\n{cmp}\r\n");

        if (sr.ReadLine() == null)
            break;
    }
    Console.WriteLine(result);
}

Console.WriteLine(partB());

long partB()
{
    var indata = input1.Where(s => s.Length> 0).ToList();
    indata.Add(@"[[2]]");
    indata.Add(@"[[6]]");

    var inTree = indata.Select(s => (Txt: s, Tree: ParseTree(new StringReader(s)))).ToList();
    inTree.Sort((a, b) => CompareTree(a.Tree, b.Tree));
    long idx1 = inTree.FindIndex(t => t.Txt == "[[2]]") + 1;
    long idx2 = inTree.FindIndex(t => t.Txt == "[[6]]") + 1;
    return idx1 * idx2;
}

int CompareTree(object a, object b)
{
    if (a is int x && b is int y)
        return Math.Sign(x - y);
    if (a is List<object> p && b is List<object> q)
    {
        for (int i = 0; i < Math.Min(p.Count, q.Count); i++)
        {
            int res = CompareTree(p[i], q[i]);
            if (res != 0)
                return res;
        }
        return Math.Sign(p.Count - q.Count);
    }
    if (a is int x2 && b is List<object> q2)
        return CompareTree(new List<object>( new[] { a }), q2);
    if (b is int y2 && a is List<object> p2)
        return CompareTree(p2, new List<object>(new[] { b }));
    return 0;
}

object ParseTree(StringReader sr)
{
    object ret = new List<object>();
    int ch = sr.Read();
    while (ch >= 0)
    {
        if (ch == '[')
        {
            var s = ParseTree(sr);
            ch = sr.Read();
            ret ??= new List<object>();
            ((List<object>)ret).Add(s);
        }
        else if (ch == ']')
            return ret;
        else if (ch == ',')
        {
            ch = sr.Read();
        }
        else if (char.IsDigit((char)ch))
        {
            int s = 0; while (char.IsDigit((char)ch)) { s = s * 10 + ch - '0'; ch = sr.Read(); }
            ret ??= new List<object>();
            ((List<object>)ret).Add(s);
        }
    }
    return ret;
}
