﻿// See https://aka.ms/new-console-template for more information
#define READALL
using System.Diagnostics;
using System.Drawing;
using System.Runtime;
using System.Text.RegularExpressions;

Console.WriteLine("15 dec, Hello, World!");

int result = 0;

#if READALL
var input1 = File.ReadAllLines("input1.txt");
#else
using (var sr = new StreamReader("input1.txt"))
{
    string line;
    while ((line = sr.ReadLine()) != null){

    }
}
#endif


Console.WriteLine(partA());
Console.WriteLine(partB());

int partA()
{
    int result = 0;
    var sensors = input1.Select(l => new sensor(l)).ToList();
    var runs = sensors.Where(s => s.touchLine != null).Select(s => s.touchLine).OrderBy(t => t.P1).ToList();
    var merged = mergeruns(runs);
    var beacons = sensors.Where(s => s.beacon.Y == 2_000_000).Select(s => s.beacon.X).ToList();
    beacons = beacons.Distinct().ToList();
    result = merged.Select(r => r.P2 - r.P1 + 1).Sum();
    return result - beacons.Count;

    List<Run> mergeruns(List<Run> runs)
    {
        var cur = runs.First(); 
        var ret = new List<Run>();
        ret.Add(cur);
        foreach (var c in runs)
        {
            if (c.P1 - 1 <= cur.P2)
            {
                if (c.P2 > cur.P2)
                    cur.P2 = c.P2; }
            else
            {
                cur = new Run(c.P1, c.P2);
                ret.Add(cur);
            }
        }
        return ret;
    }
}



int partB()
{
    int result = 0;
    var sensors = input1.Select(l => new sensor(l)).ToList();
    var edgePoints = new Dictionary<Pnt, int>();
    foreach (var s in sensors)
    {
        for (int i = 0; i < s.manDist; i++)
            Add4Pnt(s.pos, i, s.manDist);
        edgePoints.Add(new Pnt() { X = s.pos.X, Y = s.pos.Y }, 1);
    }
    var possible = edgePoints.Where(p => p.Value >= 4).ToList();
    return result;

    void Add4Pnt(Point sense, int offset, int mandist)
    {
        AddIncPnt(new Pnt { X = sense.X + offset, Y = sense.Y + mandist - offset });
        AddIncPnt(new Pnt { X = sense.X + mandist - offset, Y = sense.Y - offset });
        AddIncPnt(new Pnt { X = sense.X - offset, Y = sense.Y - mandist + offset });
        AddIncPnt(new Pnt { X = sense.X - mandist + offset, Y = sense.Y + offset });
    }

    void AddIncPnt(Pnt pnt)
    {
        if (edgePoints.ContainsKey(pnt))
            edgePoints[pnt] += 1;
        else
            edgePoints.Add(pnt, 1);
    }
}

class Run
{
    public int P1;
    public int P2;

    public Run(int p1, int p2)
    {
        P1 = p1;
        P2 = p2;
    }
}

record Pnt
{
    public int X;
    public int Y;
}

class sensor
{
    const int goalRow = 2_000_000;
    public Point pos;
    public Point beacon;
    public int manDist;
    public Run touchLine;
    static Regex re = new Regex(@"Sensor at x=(-?\d+), y=(-?\d+).*beacon is at x=(-?\d+), y=(-?\d+)");
    public sensor(string line)
    {
        var mtch = re.Match(line);
        if (!mtch.Success)
        {
            Console.WriteLine(line);
            Debugger.Break();
        }
        pos = new Point(int.Parse(mtch.Groups[1].Value), int.Parse(mtch.Groups[2].Value));
        beacon = new Point(int.Parse(mtch.Groups[3].Value), int.Parse(mtch.Groups[4].Value));
        manDist = Manhattan(beacon);
        var goalDist = Math.Abs(goalRow - pos.Y);
        if (goalDist <= manDist)
        {
            var spread = manDist - goalDist;
            touchLine = new Run(pos.X - spread, pos.X + spread);
        }
    }

    public Run GetRunAt(int lineno)
    {
        var goalDist = Math.Abs(lineno - pos.Y);
        if (goalDist <= manDist)
        {
            var spread = manDist - goalDist;
            return new Run(pos.X - spread, pos.X + spread);
        }
        return null;
    }

    public int Manhattan(Point p)
    {
        return Math.Abs(pos.X - p.X) + Math.Abs(pos.Y - p.Y);
    }
}

//T[,] ReadAndCreateMap<T>(Func<char, T> remap) where T : new()
//{
//    T[,] ret = new T[input1.Length, input1[0].Length];
//    for (int r = 0; r < input1.Length; r++)
//        for (int c = 0; c < input1[0].Length; c++)
//    {
//            ret[r, c] = remap(input1[r][c]);
//    }
//    return ret;
//}