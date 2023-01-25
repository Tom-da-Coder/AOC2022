internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("24 dec, Hello, World!");

        var input1 = File.ReadAllLines("input1.txt");
        int Height = input1.Length - 2;
        int Width = input1[0].Length - 2;
        (int r, int c) StartPnt = (-1, 0);
        (int r, int c) EndPnt = (Height, Width - 1);
        (int r, int c) StopAt;
        var ActivePoses = new List<Track>();
        var result = 0;

        RunOnce(0, StartPnt, EndPnt);
        RunOnce(result, EndPnt, StartPnt);
        RunOnce(result, StartPnt, EndPnt);

        void RunOnce(int startStep, (int r, int c) startPos, (int, int) endPos)
        {
            result = 0;
            StopAt = endPos;
            ActivePoses.Clear();
            do
            {
                ActivePoses.Add(new Track { R = startPos.r, C = startPos.c, Step = startStep });
                while (ActivePoses.Count > 0)
                {
                    var next = ActivePoses.MinBy(p => p.Step);
                    ActivePoses.Remove(next);
                    CheckMoves(next);
                    if (result != 0)
                        break;
                }
                startStep++;
            } while (result == 0);
            Console.WriteLine(result);
        }

        bool CheckMoves(Track trk)
        {
            (int gen, int r, int c) = (trk.Step, trk.R, trk.C);
            CheckNextPosition(r, c + 1, gen + 1);
            CheckNextPosition(r + 1, c, gen + 1);
            CheckNextPosition(r, c - 1, gen + 1);
            CheckNextPosition(r - 1, c, gen + 1);
            CheckNextPosition(r, c, gen + 1);
            return false;
        }

        void CheckNextPosition(int r, int c, int gen)
        {
            if ((r, c) == StopAt) { result = gen; return; }
            if (!InRange(r, c)) return;
            if (input1[r + 1][WrapCol(c + gen) + 1] == '<') return;
            if (input1[WrapRow(r + gen) + 1][c + 1] == '^') return;
            if (input1[r + 1][WrapCol(c - gen) + 1] == '>') return;
            if (input1[WrapRow(r - gen) + 1][c + 1] == 'v') return;
            if (ActivePoses.All(t => t.Step != gen || t.R != r || t.C != c))
                ActivePoses.Add(new Track { Step = gen, R = r, C = c });
            return;
        }

        int WrapRow(int r) => Wrap(Height, r);

        int WrapCol(int c) => Wrap(Width, c);

        int Wrap(int range, int val) => (val % range < 0 ? range : 0) + val % range;

        bool InRange(int r, int c) => r >= 0 && r < Height && c >= 0 && c < Width;
    }
}

class Track
{
    public int R;
    public int C;
    public int Step;
}