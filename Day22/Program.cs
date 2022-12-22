// See https://aka.ms/new-console-template for more information
#define READALL
Console.WriteLine("15 dec, Hello, World!");

int result = 0;

var input1 = File.ReadAllLines("input1.txt");
int wid = Enumerable.Range(0, input1.Length - 2).Select(i => input1[i].Length).Max();
int hgt = input1.Length - 2;
char[,] map = new char[hgt, wid];
for (int r= 0; r < hgt; r++)
    for (int c=0; c < wid; c++)
        map[r,c] = c < input1[r].Length ? input1[r][c] : ' ';
var program = input1.Last();

dir curDir = 0;
Pos curPos = new Pos { r=0, c = input1[0].IndexOf('.') };

//PrintMap();
Console.WriteLine(partA());
//PrintMap();
Console.WriteLine(partB());

int partA()
{
    int steps = 0;
    foreach (var ch in program)
    {
        if (char.IsDigit(ch))
            steps = steps * 10 + ch - '0';
        else
        {
            Move(steps);
            steps = 0;
            if (ch == 'R')
            {
                curDir = (dir)(((int)curDir + 1) % 4);
            }
            else
            {
                curDir = (dir)(((int)curDir - 1 + 4) % 4);
            }
        }
    }
    Move(steps);

    return (curPos.r + 1) * 1000 + (curPos.c + 1) * 4 + (int)curDir;
}

void Move(int steps)
{
    while (steps-- > 0)
    {
        var np = NextPos();
        if (map[np.r, np.c] == '#')
            return;
        map[curPos.r, curPos.c] = ">v<^"[(int)curDir];
        curPos= np;
    }
}

Pos NextPos()
{
    var pos = curPos;
    switch (curDir)
    {
        case dir.r: do { pos.c++; } while (map[pos.r, pos.c % wid] == ' '); break;
        case dir.d: do { pos.r++; } while (map[pos.r % hgt, pos.c] == ' '); break;
        case dir.l: do { if (--pos.c < 0) pos.c += wid; } while (map[pos.r, pos.c] == ' '); break;
        case dir.u: do { if (--pos.r < 0) pos.r += hgt; } while (map[pos.r, pos.c] == ' '); break;
    }
    pos.r = pos.r % hgt;
    pos.c = pos.c % wid;
    return pos;
}

int partB()
{
    int result = 0;
    foreach (var line in input1)
    {
    }
    return result;
}


void PrintMap()
{
    var hgt = map.GetLength(0);
    var wid = map.GetLength(1);
    for (int y = 0; y < hgt; y++)
    {
        for (int x = 0; x < wid; x++)
            Console.Write(map[y, x]);
        Console.WriteLine();
    }
}

struct Pos
{
    public int r;
    public int c;
}

enum dir { r = 0, d = 1, l = 2, u = 3 };
