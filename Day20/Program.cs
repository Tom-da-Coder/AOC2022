// See https://aka.ms/new-console-template for more information
#define READALL
Console.WriteLine("20 dec, Hello, World!");

int result = 0;

List<Number> input1 = File.ReadAllLines("input1.txt").Select(l => new Number(l)).ToList();

Console.WriteLine(partA());
Console.WriteLine(partB());

long partA()
{
    List<Number> currentList = new List<Number>(input1);
    foreach (Number number in input1)
        MoveNumber(number);

    var zeroPos = currentList.FindIndex(n => n.Value == 0);
    long p1 = currentList[(zeroPos + 1000) % currentList.Count].Value;
    long p2 = currentList[(zeroPos + 2000) % currentList.Count].Value;
    long p3 = currentList[(zeroPos + 3000) % currentList.Count].Value;
    return p1 + p2 + p3;

    void MoveNumber(Number number)
    {
        var p = currentList.IndexOf(number);
        long steps = number.Value;
        currentList.Remove(number);
        while (steps < 0) steps += currentList.Count;
        currentList.Insert((int)((p + steps + currentList.Count) % currentList.Count), number);
    }
}



long partB()
{
    List<Number> currentList = new List<Number>(input1);
    foreach (var n in input1) n.Value *= 811589153;
    for (int i = 0; i < 10; i++)
        foreach (Number number in input1)
            MoveNumber(number);

    var zeroPos = currentList.FindIndex(n => n.Value == 0);
    long p1 = currentList[(zeroPos + 1000) % currentList.Count].Value;
    long p2 = currentList[(zeroPos + 2000) % currentList.Count].Value;
    long p3 = currentList[(zeroPos + 3000) % currentList.Count].Value;
    return p1 + p2 + p3;

    void MoveNumber(Number number)
    {
        var p = currentList.IndexOf(number);
        long steps = number.Value;
        currentList.Remove(number);
        if (steps < 0) steps += ((-steps / currentList.Count) + 1) * currentList.Count;
        currentList.Insert((int)((p + steps + currentList.Count) % currentList.Count), number);
    }
}

public class Number
{
    public long Value;

    public Number(string line)
    {
        Value = int.Parse(line);
    }
}