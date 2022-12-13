// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Security.Cryptography;

Console.WriteLine("12 dec, Hello, World!");

var input1 = File.ReadAllLines("input1.txt");

var Ysize = input1.Length;
var Xsize = input1[0].Length;
Pnt Start = new Pnt(0, 0);
Pnt End = new Pnt(0, 0);

Cell[,] map = new Cell[Ysize, Xsize];

for (int y = 0; y < Ysize; y++)
    for (int x = 0; x < Xsize; x++)
    {
        var ch = input1[y][x];
        map[y, x] = new Cell();
        switch (ch)
        {
            case 'S':
                Start = new Pnt(y, x);
                map[y, x].Height = 26;
                break;
            case 'E':
                End = new Pnt(y, x);
                map[y, x].Height = 26;
                map[y, x].steps = 0;
                break;
            default:
                map[y, x].Height = ch -'a';
                break;
        }
    }

partA();

//Console.WriteLine(partA());
//Console.WriteLine(partB());

void partA()
{
    var toCheck = new Stack<Pnt>();

    toCheck.Push(End);
    while (toCheck.Count > 0)
    {
        var curPos = toCheck.Pop();
        eval(curPos.X, curPos.Y, Cell.Direction.up);
        eval(curPos.X, curPos.Y, Cell.Direction.down);
        eval(curPos.X, curPos.Y, Cell.Direction.left);
        eval(curPos.X, curPos.Y, Cell.Direction.right);
    }

    for (int y = 0; y < Ysize; y++)
    {
        for (int x = 0; x < Xsize; x++)
        {
            Console.Write(".^>v<"[(int)map[y, x].dir]);
        }
        Console.WriteLine();
    }

    Console.WriteLine($"Steps: {(map[Start.Y, Start.X].steps)}");

    void eval(int y, int x, Cell.Direction dir)
    {
        var c0 = map[y, x];
        switch (dir)
        {
            case Cell.Direction.up: y = y + 1; break;
            case Cell.Direction.down: y = y - 1; break;
            case Cell.Direction.left: x = x + 1; break;
            case Cell.Direction.right: x = x - 1; break;
        }
        if (x < 0 || y < 0 || x >= Xsize || y >= Ysize)
            return;
        Cell c1 = map[y, x];
        int nextStep = (int)c0.steps + 1;
        var hgtDiff = c1.Height - c0.Height;
        
        if (hgtDiff >= -1)
        {
            if (!c1.steps.HasValue || c1.steps.Value > nextStep)
            {
                c1.steps = nextStep;
                c1.dir = dir;
                if (Start == new Pnt(y, x))
                    Debug.WriteLine($">>> {nextStep}");
                toCheck.Push(new Pnt(y, x));
            }
        }
    }
}



int partB()
{
    int result = 0;
    foreach (var line in input1)
    {
    }
    return result;
}

class Cell
{
    public int Height;
    public enum Direction { none = 0, up, right, down, left };
    public Direction dir;
    public int? steps;
}

public record Pnt
{
    public int X;
    public int Y;

    public Pnt(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Pnt(Pnt pnt)
    {
        X = pnt.X;
        Y = pnt.Y;
    }

    public static Pnt operator +(Pnt p1, Pnt p2) => new Pnt(p1.X + p2.X, p1.Y + p2.Y);
}
