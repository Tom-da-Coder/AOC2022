// See https://aka.ms/new-console-template for more information
#define READALL
using System.Diagnostics;
using System.Text.RegularExpressions;

Console.WriteLine("15 dec, Hello, World!");

int result = 0;

var input1 = File.ReadAllLines("input1.txt");
var apeDictionary = input1.Select(line => { var s = line.Split(':'); return (Name: s[0], Func: s[1]); }).ToDictionary(q => q.Name, q => q.Func);
Regex re = new Regex(@"((\d+)|(\w{4}) ([-*/+]) (\w{4}))");


//BuildTree(apeDictionary);

Console.WriteLine(partA());
Console.WriteLine(partB());

long partA()
{
    long result = Calc("root");
    return result;
}

long Calc(string name)
{
    long val;
    var raw = apeDictionary[name];
    var mtc = re.Match(raw);
    if (mtc.Groups[2].Success)
        val = long.Parse(mtc.Groups[2].Value);
    else
    {
        long left = Calc(mtc.Groups[3].Value);
        long right = Calc(mtc.Groups[5].Value);
        val = mtc.Groups[4].Value switch
        {
            "+" => left + right,
            "-" => left - right,
            "*" => left * right,
            "/" => left / right,
        };
    }
    return val;
}

long partB()
{
    List<Term> PathToHumn = new List<Term>();
    long? left = Calc2("tlpd", PathToHumn);
    long? right = Calc2("jjmw", PathToHumn);
    long result = ReverseCalc(right.Value, PathToHumn);
    return result;
}

long ReverseCalc(long goal, List<Term> path)
{
    path.Reverse();
    foreach (var term in path)
    {
        switch (term.oper)
        {
            case '+':
                goal -= (long)(term.left ?? term.right);
                break;
            case '-':
                if (term.left.HasValue)
                    goal = term.left.Value - goal;
                else
                    goal = goal + term.right.Value;
                break;
            case '*':
                goal = goal / (long)(term.left ?? term.right);
                break;
            case '/':
                if (term.left.HasValue)
                    goal = term.left.Value / goal;
                else
                    goal = goal * term.right.Value;
                break;
        }
    }
    return goal;
}

long? Calc2(string name, List<Term> pathToHumn)
{
    long val;
    if (name == "humn")
        return null;
    var raw = apeDictionary[name];
    var mtc = re.Match(raw);
    if (mtc.Groups[2].Success)
        val = long.Parse(mtc.Groups[2].Value);
    else
    {
        long? left = Calc2(mtc.Groups[3].Value, pathToHumn);
        long? right = Calc2(mtc.Groups[5].Value, pathToHumn);
        if (left == null || right == null)
        {
            pathToHumn.Add(new Term { left = left, oper = mtc.Groups[4].Value[0], right = right });
            return null;
        }
        val = mtc.Groups[4].Value switch
        {
            "+" => left.Value + right.Value,
            "-" => left.Value - right.Value,
            "*" => left.Value * right.Value,
            "/" => left.Value / right.Value,
        };
    }
    return val;
}

class Term
{
    public long? left;
    public long? right;
    public char oper;
}

//class Ape
//{
//    public string Raw;
//    public long Value;

//    public Ape Left;
//    public Ape Right;

//    public Ape(string raw)
//    {
//        var mtc = re.Match(raw);
//        if (mtc.Groups[2].Success)
//            Value = int.Parse(mtc.Groups[2].Value);
//        else
//        {
//            var leftName = mtc.Groups[3].Value;
//            var rightName = mtc.Groups[5].Value;
//        }
//    }
//}
