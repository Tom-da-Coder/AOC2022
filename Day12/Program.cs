// See https://aka.ms/new-console-template for more information
#define A
#define JANNE

using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

Console.WriteLine("12 dec, Hello, World!");

var input1 = File.ReadAllLines("input1.txt");

#if JANNE
var jannesRoad = File.ReadAllLines("tp.txt").Select(l => new Pnt(int.Parse(l.Split(',')[1].Trim()), int.Parse(l.Split(',')[0].Trim()))).ToList();
#endif

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
                map[y, x].Height = 0;
#if B
                map[y, x].steps = 0;
#endif
                break;
            case 'E':
                End = new Pnt(y, x);
                map[y, x].Height = 25;
#if A
                map[y, x].steps = 0;
#endif
                break;
            default:
                map[y, x].Height = ch -'a';
                break;
        }
    }

#if A
partA();

var best = GetMapAsEnumerable().Where(c => c.Height == 0).Min(c => c.steps);
Console.WriteLine(best);

IEnumerable<Cell> GetMapAsEnumerable()
{
    for (int row = 0; row < Ysize; row++)
        for (int col = 0; col < Xsize; col++)
            yield return map[row, col];
}
#endif

#if B
partB();
#endif

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

    Pnt walker = new Pnt(Start);
    int steps = 0;
    while (walker != End)
    {
        steps++;
        map[walker.X, walker.Y].inpath = true;
        switch (map[walker.X, walker.Y].dir)
        {
            case Cell.Direction.up: walker.X--; break;
            case Cell.Direction.down: walker.X++; break;
            case Cell.Direction.left: walker.Y--; break;
            case Cell.Direction.right: walker.Y++; break;
        }
    }

#if JANNE
    foreach (var p in jannesRoad)
        map[p.X, p.Y].jannes = true;
#endif

    var colormap = new ConsoleColor[] { ConsoleColor.Black, ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Yellow };

    for (int y = 0; y < Ysize; y++)
    {
        for (int x = 0; x < Xsize; x++)
        {
            var c = map[y, x];
            var cv = (c.jannes ? 2 : 0) + (c.inpath ? 1 : 0);
            var col = colormap[cv];
            //if (c.jannes) Console.BackgroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor= col;
            if (c.jannes || c.inpath)
                Console.Write((char)('a' + c.Height));
            else
                Console.Write(".^>v<"[(int)c.dir]);
            //if (c.jannes) Console.ResetColor();
        }
        Console.WriteLine();
    }

    Console.WriteLine($"Steps walker: {steps}");
    Console.WriteLine($"Steps: {(map[Start.X, Start.Y].steps)}");

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



void partB()
{
    var toCheck = new Stack<Pnt>();

    toCheck.Push(Start);
    while (toCheck.Count > 0)
    {
        var curPos = toCheck.Pop();
        eval(curPos.X, curPos.Y, Cell.Direction.up);
        eval(curPos.X, curPos.Y, Cell.Direction.down);
        eval(curPos.X, curPos.Y, Cell.Direction.left);
        eval(curPos.X, curPos.Y, Cell.Direction.right);
    }


    Pnt walker = new Pnt(Start);
    int steps = 0;
    //while (walker != End)
    //{
    //    steps++;
    //    map[walker.X, walker.Y].inpath = true;
    //    switch (map[walker.X, walker.Y].dir)
    //    {
    //        case Cell.Direction.up: walker.X--; break;
    //        case Cell.Direction.down: walker.X++; break;
    //        case Cell.Direction.left: walker.Y--; break;
    //        case Cell.Direction.right: walker.Y++; break;
    //        case Cell.Direction.none: Debugger.Break(); break;
    //    }
    //}

    for (int y = 0; y < Ysize; y++)
    {
        for (int x = 0; x < Xsize; x++)
        {
            var c = map[y, x];
            if (c.inpath) Console.BackgroundColor = ConsoleColor.Yellow;
            if (y == Start.X && x == Start.Y)
                Console.Write("S");
            else if (y == End.X && x == End.Y)
                Console.Write("E");
            else
                Console.Write(".^>v<"[(int)c.dir]);
            if (c.inpath) Console.ResetColor();
        }
        Console.WriteLine();
    }

    Console.WriteLine($"Steps at End = {map[End.X, End.Y].steps}");

    void eval(int y, int x, Cell.Direction dir)
    {
        var c0 = map[y, x];
        if (c0.steps == 11) Debugger.Break();
        switch (dir)
        {
            case Cell.Direction.up: y = y - 1; break;
            case Cell.Direction.down: y = y + 1; break;
            case Cell.Direction.left: x = x - 1; break;
            case Cell.Direction.right: x = x + 1; break;
        }
        if (x < 0 || y < 0 || x >= Xsize || y >= Ysize) return; // Out of bounds?
        Cell c1 = map[y, x];
        int nextStep = c0.steps.Value + 1;
        var hgtDiff = c1.Height - c0.Height;

        if (hgtDiff <= 1)
        {
            if (!c1.steps.HasValue || nextStep < c1.steps.Value)
            {
                c1.steps = nextStep;
                c0.dir = dir;
                if (End == new Pnt(y, x))
                    Debug.WriteLine($">>> {nextStep}");
                else
                    toCheck.Push(new Pnt(y, x));
            }
        }
    }
}

class Cell
{
    public int Height;
    public enum Direction { none = 0, up, right, down, left };
    public Direction dir;
    public int? steps;
    public bool inpath = false;
    public bool jannes = false;
    public int? from;
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
