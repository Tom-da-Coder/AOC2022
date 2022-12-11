// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

Console.WriteLine("11 dec, Hello, World!");

Console.WriteLine(partA());
//Console.WriteLine(partB());

int partA()
{
    using (var rd = new StreamReader("demo.txt"))
        while (!rd.EndOfStream)
        {
            var line = rd.ReadLine(); // Skip monkey number
            line = rd.ReadLine();
            var reItems = new Regex(@"\d+");
            var matches = reItems.Matches(line);
            var items = matches.Select(x => int.Parse(x.Value)).ToList();

            line = rd.ReadLine();
            var opmtch = Regex.Match(line, @"old ([*+]) (\d+|old)");
            bool isMul = opmtch.Groups[1].Value == "*";
            int opVal;
            if (opmtch.Groups[2].Value == "old")
                opVal = 0;
            else
                opVal = int.Parse(opmtch.Groups[2].Value);

            line = rd.ReadLine();
            int testDiv = int.Parse(Regex.Match(line, @"divisible by (\d+)").Groups[1].Value);
            int[] dests = new int[2];
            line = rd.ReadLine();
            dests[0] = int.Parse(Regex.Match(line, @"monkey (\d+)").Groups[1].Value);
            line = rd.ReadLine();
            dests[1] = int.Parse(Regex.Match(line, @"monkey (\d+)").Groups[1].Value);

            Monkey.monkeys.Add(new Monkey(items, isMul, opVal, testDiv, dests));
            if (!rd.EndOfStream)
                rd.ReadLine();
        }

    for (int round = 0; round < 20; round++)
    {
        foreach (var monkey in Monkey.monkeys)
            monkey.DoRound();
        Console.WriteLine($"Round {round + 1}: " + Monkey.monkeys.Select(m => m.Times.ToString()).Aggregate((a, b) => a + ", " + b));
    }
    return Monkey.Answer();
}


class Monkey
{
    public static List<Monkey> monkeys = new List<Monkey>();

    public List<int> Items = new List<int>();
    public bool OpIsMul;
    public int OpVal;
    public int TestDiv;
    public int[] Destinations;
    public int Times = 0;

    public Monkey(List<int> items, bool opsMul, int opVal, int testDiv, int[] destinations)
    {
        Items = items;
        OpIsMul = opsMul;
        OpVal = opVal;
        TestDiv = testDiv;
        Destinations = destinations;
    }

    public void AddItem(int item) => Items.Add(item);

    public void DoRound()
    {
        Times += Items.Count();
        foreach (var item in Items)
        {
            var val = item;
            if (OpIsMul)
                val *= (OpVal > 0 ? OpVal : val);
            else
                val += OpVal;
            //val /= 3;
            if (val % TestDiv == 0)
                monkeys[Destinations[0]].AddItem(val);
            else
                monkeys[Destinations[1]].AddItem(val);
        }
        Items.Clear();
    }

    public static int Answer() => monkeys.Select(m => m.Times).OrderDescending().Take(2).Aggregate((a, b) => a * b);
}