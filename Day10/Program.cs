// See https://aka.ms/new-console-template for more information

using System.Text;
Console.WriteLine("10 dec, Hello, World!");

int result = 0;
var input1 = File.ReadAllLines("input1.txt");
var cycles = new List<int>(300);


//var input2 = File.ReadAllLines("input2.txt");

Console.WriteLine(partA());
partB();

int partA()
{
    int result = 0;
    int sum = 1;

    foreach (var line in input1)
    {
        switch (line.Substring(0, 4))
        {
            case "noop":
                cycles.Add(sum);
                break;
            case "addx":
                cycles.Add(sum);
                cycles.Add(sum);
                sum += int.Parse(line.Substring(5));
                break;
        }
    }
    result = new []{ 20, 60,100,140,180,220 }.Select(q => cycles[q - 1] * q).Sum();
    return result;
}



void partB()
{
    var screen = new StringBuilder[6];
    for (int i = 0; i < 6; i++)
    {
        screen[i] = new StringBuilder();
        for (int x = 0; x < 40; x++)
        {
            var spriteX = cycles[x + 40 * i];
            bool show = Math.Abs(spriteX - x) <= 1;
            screen[i].Append(show ? "#" : ".");
        }
    }
    foreach (var s in screen) 
        Console.WriteLine(s);
}
