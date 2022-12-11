// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

Console.WriteLine("11 dec, Hello, World!");

Console.WriteLine(partA());
//Console.WriteLine(partB());

ulong partA()
{
    using (var rd = new StreamReader("input1.txt"))
        while (!rd.EndOfStream)
        {
            var line = rd.ReadLine(); // Skip monkey number
            line = rd.ReadLine();
            var reItems = new Regex(@"\d+");
            var matches = reItems.Matches(line);
            var items = matches.Select(x => ulong.Parse(x.Value)).ToList();

            line = rd.ReadLine();
            var opmtch = Regex.Match(line, @"old ([*+]) (\d+|old)");
            bool isMul = opmtch.Groups[1].Value == "*";
            ulong opVal;
            if (opmtch.Groups[2].Value == "old")
                opVal = 0;
            else
                opVal = ulong.Parse(opmtch.Groups[2].Value);

            line = rd.ReadLine();
            ulong testDiv = ulong.Parse(Regex.Match(line, @"divisible by (\d+)").Groups[1].Value);
            int[] dests = new int[2];
            line = rd.ReadLine();
            dests[0] = int.Parse(Regex.Match(line, @"monkey (\d+)").Groups[1].Value);
            line = rd.ReadLine();
            dests[1] = int.Parse(Regex.Match(line, @"monkey (\d+)").Groups[1].Value);

            Monkey.monkeys.Add(new Monkey(items, isMul, opVal, testDiv, dests));
            if (!rd.EndOfStream)
                rd.ReadLine();
        }

    for (int round = 0; round < 10000; round++)
    {
        foreach (var monkey in Monkey.monkeys)
            monkey.DoRound();
        if (new[] {1, 20, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000 }.Contains(round + 1))
        Console.WriteLine($"Round {round + 1}: " + Monkey.monkeys.Select(m => m.Times.ToString()).Aggregate((a, b) => a + ", " + b));
    }
    return Monkey.Answer();
}


class Monkey
{
    public static List<Monkey> monkeys = new List<Monkey>();

    public List<ulong> Items;
    public bool OpIsMul;
    public ulong OpVal;
    public ulong TestDiv;
    public int[] Destinations;
    public ulong Times = 0;

    public Monkey(List<ulong> items, bool opsMul, ulong opVal, ulong testDiv, int[] destinations)
    {
        Items = items;
        OpIsMul = opsMul;
        OpVal = opVal;
        TestDiv = testDiv;
        Destinations = destinations;
    }

    public void AddItem(ulong item) => Items.Add(item);

    public void DoRound()
    {
        const ulong modulo = 23 * 19 * 13 * 17;
        Times += (ulong)Items.Count();
        foreach (var item in Items)
        {
            ulong val = item;
            if (OpIsMul)
                val *= (OpVal > 0 ? OpVal : val);
            else
                val += OpVal;
            //val /= 3;
            val = val % modulo;
            if (val % TestDiv == 0)
                monkeys[Destinations[0]].AddItem(val);
            else
                monkeys[Destinations[1]].AddItem(val);
        }
        Items.Clear();
    }

    public static ulong Answer() => monkeys.Select(m => m.Times).OrderDescending().Take(2).Aggregate((a, b) => a * b);
}