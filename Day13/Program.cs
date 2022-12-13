// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using System.Text.Json.Serialization;

Console.WriteLine("13 dec, Hello, World!");

int result = 0;
var input1 = File.ReadAllLines("input1.txt");


using (var sr = new StreamReader("input1.txt"))
{
    string line;
    while ((leftline = sr.ReadLine()) != null)
    {
        var rightline = sr.ReadLine();


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

Node parser(string input)
{

}
class Node
{
    public object Val;

    public static int Compare(object x, object y)
    {
        if (x is int && y is int)
            return (int)x - (int)y;
        if (x is Node && y is int)
            return Compare(x, new Node { Val = y });
        if (x is int && y is Node)
            return Compare(new Node { Val = x }, y);

        for (int i = 0; i < )
    }
    public void Convert() => Val = new Node() { Val = this.Val };
}