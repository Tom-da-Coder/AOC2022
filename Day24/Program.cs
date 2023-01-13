// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Reflection;
using System.Reflection.Metadata;

Console.WriteLine("24 dec, Hello, World!");

var ResultsList = new List<int>();
int minResult = int.MaxValue;
var input1 = File.ReadAllLines("input1.txt");
var blizzards = new List<Blizzard>();
Blizzard.Height = input1.Length - 2;
Blizzard.Width = input1[0].Length - 2;
for (int r = 0; r < input1.Length; r++)
{
    string line = input1[r];
    for (int c = 0; c < line.Length; c++)
    {
        var ch = line[c];
        var ix = ">v<^".IndexOf(line[c]);
        if (ix >= 0)
            blizzards.Add(new Blizzard { row = r - 1, col = c - 1, dir = (Blizzard.Direction)ix });
    }
}

partA();
Console.WriteLine(ResultsList.Min());

//Console.WriteLine(partB());

int partA()
{
    RecursiveMove(1, 0, 0);
    return 0;
}



int partB()
{
    int result = 0;
    foreach (var line in input1)
    {
    }
    return result;
}

void RecursiveMove(int gen, int r, int c)
{
    if (gen > minResult)
        return;
    if (r == input1.Length - 3 && c == input1[0].Length - 3)
    {
        ResultsList.Add(gen + 1);
        if (minResult > gen + 1)
            minResult= gen + 1;
        return;
        //Console.WriteLine(gen + 1);
        //throw new FinishedException();
    }

    if (CheckNextPosition(r, c + 1, gen + 1)) RecursiveMove(gen + 1, r, c + 1);
    if (CheckNextPosition(r + 1, c, gen + 1)) RecursiveMove(gen + 1, r + 1, c);
    if (CheckNextPosition(r, c - 1, gen + 1)) RecursiveMove(gen + 1, r, c - 1);
    if (CheckNextPosition(r - 1, c, gen + 1)) RecursiveMove(gen + 1, r - 1, c);
    if (CheckNextPosition(r, c, gen + 1)) RecursiveMove(gen + 1, r, c);
}

// Return true when free
//bool CheckNextPosition(int r, int c, int gen) {
//    return InRange(r, c) && blizzards.Where(b => b.row == r).All(w => w.IsWhere(gen).col != c) &&
//    blizzards.Where(b => b.col == c).All(w => w.IsWhere(gen).row != r);
//}

bool CheckNextPosition(int r, int c, int gen)
{
    if (!InRange(r, c)) return false;
    if (input1[r + 1][LimitCol(c + gen) + 1] == '<') return false;
    if (input1[LimitRow(r + gen) + 1][c + 1] == '^') return false;
    if (input1[r + 1][LimitCol(c - gen) + 1] == '>') return false;
    if (input1[LimitRow(r - gen) + 1][c + 1] == 'v') return false;
    return true;
}


int LimitRow(int r)
{
    while (r < 0) r += Blizzard.Height;
    r %= Blizzard.Height;
    return r;
}

int LimitCol(int c)
{
    while (c < 0) c += Blizzard.Width;
    c %= Blizzard.Width;
    return c;
}

bool InRange(int r, int c) => r >= 0 && r < Blizzard.Height && c >= 0 && c < Blizzard.Width;

public class Blizzard
{
    public static int Height, Width;

    public int row;
    public int col;
    public enum Direction { East, South, West, North };
    public Direction dir;

    public (int row, int col) IsWhere(int atGen)
    {
        switch (dir)
        {
            case Direction.South: return Limit(row + atGen, col);
            case Direction.North: return Limit(row - atGen, col);
            case Direction.East: return Limit(row, col + atGen);
            case Direction.West: return Limit(row, col - atGen);
        }
        return (-1, -1);

        (int r, int c) Limit(int r, int c) => ((r + 4 * Height) % Height, (c + 4 * Width) % Width);
    }
}

public class FinishedException : Exception { }
