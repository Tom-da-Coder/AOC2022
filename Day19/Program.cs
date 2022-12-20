// See https://aka.ms/new-console-template for more information
#define READALL
using System.Text.RegularExpressions;

Console.WriteLine("15 dec, Hello, World!");

int result = 0;

var bluePrints = File.ReadAllLines("input1.txt").Select(l => new Blueprint(l)).ToList();


Console.WriteLine(partA());
Console.WriteLine(partB());

int partA()
{
    int result = 0;
    return result;
}



int partB()
{
    int result = 0;
    return result;
}

class Blueprint
{
    static Regex lineParser = new Regex(@"Blueprint (\d+): Each ore robot costs (\d+) ore. Each clay robot costs (\d+) ore. Each obsidian robot costs (\d+) ore and (\d+) clay. Each geode robot costs (\d+) ore and (\d+) obsidian");

    public int BpNo;
    public int OreCost;
    public int ClayCost;
    public int ObsidianCostOre;
    public int ObsidianCostClay;
    public int GeodeCostOre;
    public int GeodeCostObsidian;

    public Blueprint(string line)
    {
        var matc = lineParser.Match(line);
        BpNo = getNum(1);
        OreCost = getNum(2);
        ClayCost = getNum(3);
        ObsidianCostOre = getNum(4);
        ObsidianCostClay = getNum(5);
        GeodeCostOre = getNum(6);
        GeodeCostObsidian = getNum(7);

        int getNum(int index) => int.Parse(matc.Groups[index].Value);
    }
}