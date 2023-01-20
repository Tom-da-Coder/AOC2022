using System.Runtime.Serialization.Formatters.Binary;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("24 dec, Hello, World!");

        var ResultsList = new List<int>();
        var input1 = File.ReadAllLines("input1.txt");
        int Height = input1.Length - 2;
        int Width = input1[0].Length - 2;
        int[,,] DistMap = new int[1000, Height, Width];
        var ActivePoses = new List<Track>();
        var result = 0;

        prepDistMap();

        partA();

        void prepDistMap()
        {
            Array.Clear(DistMap);
        }

        int partA()
        {
            RunOnce(false, 1);
            var startStep = result + 1;
            result = 0;
            while (result == 0)
            {
                RunOnce(true, startStep);
                startStep++;
            }
            startStep = 568; // result + 1;
            result = 0;
            while (result == 0)
            {
                RunOnce(false, startStep);
                startStep++;
            }
            return 0;
        }

        void RunOnce(bool back, int start)
        {
            ActivePoses.Clear();
            ActivePoses.Add(new Track { R = back ? Height - 1 : 0, C = back ? Width - 1 : 0, Step = start });
            while (ActivePoses.Count > 0)
            {
                var next = ActivePoses.MinBy(p => p.Step);
                ActivePoses.Remove(next);
                if (CheckMoves(next, back))
                    break;
            }
        }

        bool CheckMoves(Track trk, bool returning)
        {
            (int gen, int r, int c) = (trk.Step, trk.R, trk.C);
            if (!returning && r == Height - 1 && c == Width - 1 || returning && r == 0 && c == 0)
            {
                if (returning) PrintTrack(trk);
                result = gen + 1;
                Console.WriteLine(gen + 1);
                return true;
            }

            CheckNextPosition(r, c + 1, gen + 1, trk);
            CheckNextPosition(r + 1, c, gen + 1, trk);
            CheckNextPosition(r, c - 1, gen + 1, trk);
            CheckNextPosition(r - 1, c, gen + 1, trk);
            CheckNextPosition(r, c, gen + 1, trk);
            return false;
        }

        bool CheckNextPosition(int r, int c, int gen, Track? from = null)
        {
            if (!InRange(r, c)) return false;
            if (input1[r + 1][WrapCol(c + gen) + 1] == '<') return false;
            if (input1[WrapRow(r + gen) + 1][c + 1] == '^') return false;
            if (input1[r + 1][WrapCol(c - gen) + 1] == '>') return false;
            if (input1[WrapRow(r - gen) + 1][c + 1] == 'v') return false;
            if (ActivePoses.All(t => t.Step != gen || t.R != r || t.C != c))
                ActivePoses.Add(new Track { Step = gen, R = r, C = c, ComesFrom = from });
            return true;
        }

        void PrintTrack(Track? track)
        {
            var steps = new List<Track>();
            while (track != null)
            {
                steps.Add(track);
                track = track.ComesFrom;
            }
            steps.Reverse();
            Console.WriteLine(string.Join(Environment.NewLine, steps.Select(s => $"{s.Step}, {s.R}, {s.C}")));
        }

        void SaveArray()
        {
            using (var strm = new FileStream("fil.bin", FileMode.Create))
                
            {
                var fmt = new BinaryFormatter();
                fmt.Serialize(strm, DistMap);
                strm.Flush();
            }
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
    public Track? ComesFrom;
}