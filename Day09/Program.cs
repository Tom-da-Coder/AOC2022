// See https://aka.ms/new-console-template for more information

Console.WriteLine("09 dec, Hello, World!");

var input1 = File.ReadAllLines("input1.txt");

Console.WriteLine(partA());

int partA()
{
    var T = new List<Pnt>();
    for (int i = 0; i <= 9; i++)
        T.Add(new Pnt(0, 0));

    var TailPos = new List<Pnt>();
    foreach (var line in input1)
    {
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
        }
    }
    return TailPos.Distinct().Count();
}

void Follow(Pnt Head, Pnt Tail)
{
    var dx = Head.X - Tail.X;
    var dy = Head.Y - Tail.Y;
    if (Math.Abs(dx) < 2 && Math.Abs(dy) < 2)
        return;
    Tail.X += Math.Sign(dx);
    Tail.Y += Math.Sign(dy);
}

public record class Pnt
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
}
