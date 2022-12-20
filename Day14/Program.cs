// See https://aka.ms/new-console-template for more information
#define READALL
using System.Drawing;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

Console.WriteLine("14 dec, Hello, World!");

int result = 0;
List<Wall> Walls = new List<Wall>();

var input1 = File.ReadAllLines("input1.txt");
foreach (var line in input1)
{
    var mtch = Regex.Matches(line, @"(\d+),(\d+)");
    Point p1 = makePoint(mtch[0]);
    for (int i = 1; i < mtch.Count; i++)
    {
        Point p2 = makePoint(mtch[i]);
        Walls.Add(new Wall { P1 = p1, P2 = p2 });
        p1 = p2;
    }
}
int MinX = Walls.SelectMany(w => new int[] { w.P1.X, w.P2.X }).Min();
int MaxX = Walls.SelectMany(w => new int[] { w.P1.X, w.P2.X }).Max();
int MinY = Walls.SelectMany(w => new int[] { w.P1.Y, w.P2.Y }).Min();
int MaxY = Walls.SelectMany(w => new int[] { w.P1.Y, w.P2.Y }).Max();

char[,] Map = MakeMap(MinX - 200, 0, MaxX - MinX + 400, MaxY + 2);

//Map[500 - (MinX - 200), 0] = '+';
//PrintMap();
//Console.WriteLine(partA());
PrintMap();
Console.WriteLine(partB());
//PrintMap();

int partA()
{
    int result = 0;
    bool stopped = false;
    do
    {
        RippleOneStone();
        result++;
    } while (stopped);    
    return result;

    void RippleOneStone()
    {
        stopped = false;
        Point p = new Point(500 - (MinX - 200), 0);

        while (p.Y <= MaxY)
        {
            if (Map[p.X, p.Y + 1] == '.')
                p.Y += 1;
            else if (Map[p.X - 1, p.Y + 1] == '.')
            {
                p.X--; p.Y++;
            }
            else if (Map[p.X + 1, p.Y + 1] == '.')
            {
                p.X++; p.Y++;
            }
            else
            {
                stopped = true;
                Map[p.X, p.Y] = 'O';
                break;
            }
        }
    }
}



int partB()
{
    int result = 0;
    bool stopped = false;
    do
    {
        RippleOneStone();
        result++;
    } while (Map[500 - (MinX - 200), 0] == '.');
    return result;

    void RippleOneStone()
    {
        stopped = false;
        Point p = new Point(500 - (MinX - 200), 0);

        while (true)
        {
            if (p.Y == MaxY + 1)
            {
                stopped = true;
                Map[p.X, p.Y] = 'O';
                break;
            }
            else if (Map[p.X, p.Y + 1] == '.')
                p.Y += 1;
            else if (Map[p.X - 1, p.Y + 1] == '.')
            {
                p.X--; p.Y++;
            }
            else if (Map[p.X + 1, p.Y + 1] == '.')
            {
                p.X++; p.Y++;
            }
            else
            {
                stopped = true;
                Map[p.X, p.Y] = 'O';
                break;
            }
        }
    }
}

void PrintMap()
{
    var wid = Map.GetLength(0);
    var hgt = Map.GetLength(1);
    for (int y = 0; y < hgt; y++)
    {
        for (int x = 0; x < wid; x++)
            Console.Write(Map[x, y]);
        Console.WriteLine();
    }
}

char[,] MakeMap(int sx, int sy, int w, int h)
{
    char[,] ret = new char[w, h];
    for (int x = 0; x < w; x++)
        for (int y = 0; y < h; y++)
            ret[x, y] = '.';
    foreach (var wall in Walls)
    {
        int x = wall.P1.X;
        int y = wall.P1.Y;
        int dx = wall.P2.X - x;
        int dy = wall.P2.Y - y;
        int n = Math.Max(Math.Abs(dx), Math.Abs(dy));
        dx = Math.Sign(dx);
        dy = Math.Sign(dy);
        ret[x - sx, y - sy] = '#';
        while (n-- > 0)
        {
            x += dx;
            y += dy;
            ret[x - sx, y - sy] = '#';
        }
    }
    return ret;
}

Point makePoint(Match m) => new Point(int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value));

class Wall
{
    public Point P1;
    public Point P2; 
}
