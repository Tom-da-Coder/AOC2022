// See https://aka.ms/new-console-template for more information

Console.WriteLine("09 dec, Hello, World!");

int result = 0;
//var input1 = File.ReadAllLines("input1.txt");
var input1 = File.ReadAllLines("demo.txt");


//var input2 = File.ReadAllLines("input2.txt");

Console.WriteLine(partA());
Console.WriteLine(partB());

int partA()
{
    //var Head = new Pnt(0, 0);
    var T = new List<Pnt>();
    for (int i = 0; i <= 9; i++)
        T.Add(new Pnt(0, 0));

    var TailPos = new List<Pnt>();
    foreach (var line in input1)
    {
        Console.WriteLine(line);
        var n = int.Parse(line.Substring(2));
        for (int i = 0; i < n; i++)
        {
            switch (line[0])
            {
                case 'U': T[0].Y += 1; break;
                case 'D': T[0].Y -= 1; break;
                case 'L': T[0].X -= 1; break;
                case 'R': T[0].X += 1; break;
            }
            for (int k = 0; k < 9; k++)
                Follow(T[k], T[k + 1]);
            TailPos.Add(new Pnt(T[9]));
            print();
        }
    }
    result = TailPos.Distinct().Count();
    return result;

    void print()
    {
        for (int i = 4; i >= 0; i--)
        {
            for (int k = 0; k < 5; k++) { 
                var q = T.FindIndex(t => t.Y == i && t.X == k);
                Console.Write(q < 0 ? "." : (q == 0 ? "H" : q));
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}


int partB()
{
    return 0;
}

void Follow(Pnt Head, Pnt Tail)
{
    var dx = Head.X - Tail.X;
    var dy = Head.Y - Tail.Y;
    if (dx > 1)
    {
        Tail.X = Head.X - 1;
        Tail.Y = Head.Y;
    }
    if (dx < -1)
    {
        Tail.X = Head.X + 1;
        Tail.Y = Head.Y;
    }
    if (dy > 1)
    {
        Tail.Y = Head.Y - 1;
        Tail.X = Head.X;
    }
    if (dy < -1)
    {
        Tail.Y = Head.Y + 1;
        Tail.X = Head.X;
    }
}

public record class Pnt // : IEquatable<Pnt>
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

    //public bool Equals(Pnt? other)
    //{
    //    if (other is null) return false;
    //    return X == other.X && Y == other.Y;
    //}
}
