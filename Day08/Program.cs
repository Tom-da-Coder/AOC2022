// See https://aka.ms/new-console-template for more information

using System.Drawing;
using System.Transactions;

Console.WriteLine("08 dec, Hello, World!");

int result = 0;
var input1 = File.ReadAllLines("input1.txt");

var forest = new int[input1.Length, input1[0].Length];
for (int i = 0; i < input1.Length; i++)
    for (int k = 0; k < input1[0].Length; k++)
        forest[i, k] = input1[i][k] - '0';

var visfromout = new bool[input1.Length, input1[0].Length];

Console.WriteLine(partA());
Console.WriteLine(partB());

//int n = 0;
//for (int i = 0;  i< input1.Length; i++)
//{
//    for (int k = 0; k < input1[0].Length; k++)
//    {
//        var vis = visfromout[i, k];
//        Console.Write(vis ? '*' : '.');
//        if (vis)
//            n++;
//    }
//    Console.WriteLine();
//}
//Console.WriteLine(n);

int partA()
{
    int result = 0;
    for (int i = 0; i < input1.Length; i++)
    {
        int redge = input1[0].Length - 1;
        result += lookout(new Point(i, 0), new Point(0, 1), redge + 1);
        result += lookout(new Point(i, redge), new Point(0, -1), redge + 1);
    }
    for (int k = 0; k < input1[0].Length; k++)
    {
        int botedge = input1.Length - 1;
        result += lookout(new Point(0, k), new Point(1, 0), botedge + 1);
        result += lookout(new Point(botedge, k), new Point(-1, 0), botedge + 1);
    }
    return result;

    int lookout(Point start, Point incr, int count)
    {
        int cnt = 0;
        int h = -1;
        for (int i = 0; i < count; i++)
        {
            int t = forest[start.X, start.Y];
            if (t > h)
            {
                h = t;
                if (!visfromout[start.X, start.Y])
                {
                    cnt++;
                    visfromout[start.X, start.Y] = true;
                }
            }
            if (h == 9)
                break;
            start.Offset(incr);
        }
        return cnt;
    }
}



int partB()
{
    int result = 0;
    int hgt = input1.Length;
    int wid = input1[0].Length;
    for (int h = 0; h < hgt; h++)
        for (int w = 0; w < wid; w++)
        {
            int newres = score(new Point(h, w), new Point(0, 1)) *
                score(new Point(h, w), new Point(0, -1)) *
                score(new Point(h, w), new Point(1, 0)) *
                score(new Point(h, w), new Point(-1, 0));
            if (newres > result)
                result = newres;
        }
    return result;

    int score(Point pos, Point dir)
    {
        int h = forest[pos.X, pos.Y];
        int cnt = 0;
        while (pos.X > 0 && pos.X < hgt - 1 && pos.Y > 0 && pos.Y < wid - 1)
        {
            cnt++;
            pos.Offset(dir);
            if (h <= forest[pos.X, pos.Y])
                break;
        }
        return cnt;
    }
}
