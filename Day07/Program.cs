// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

Console.WriteLine("07 dec, Hello, World!");

int result = 0;
var input1 = File.ReadAllLines("input1.txt");
//var input1 = File.ReadAllLines("demo.txt");



//var input2 = File.ReadAllLines("input2.txt");
    dir curDir = new dir { name= "/" };

Console.WriteLine(partA());
Console.WriteLine(partB());

int partA()
{
    int result = 0;
    Stack<dir> dirStack = new Stack<dir>();
    var re = new Regex(@"((\d+) ([a-z0-9.]+))|(\$ cd ((\.\.)|(\w+)|(/)))|(dir (\w+))|(ls)");
    foreach (var line in input1)
    {
        var m = re.Match(line);
        if (!m.Success)
            throw new Exception();
        if (m.Groups[2].Success)
        {
            curDir.filesSize += int.Parse(m.Groups[2].Value);
        } else if (m.Groups[6].Success)
        {
            curDir = dirStack.Pop();
        } else if (m.Groups[7].Success)
        {
            dirStack.Push(curDir);
            curDir = curDir.subDirs.FirstOrDefault(d => d.name == m.Groups[7].Value) ?? new dir { name = m.Groups[7].Value };
        } else if (m.Groups[10].Success)
        {
            curDir.subDirs.Add(new dir { name = m.Groups[10].Value });
        }
    }
    while (dirStack.Count > 0) curDir = dirStack.Pop();
    curDir.GetDirTotal();
    return curDir.GetSum100000();
}



int partB()
{
    var toDelete = curDir.totalSize - 40_000_000;
    var sizeList = new List<int>();
    curDir.FindBigEnought(sizeList, toDelete);
    return sizeList.Min();
}

class dir
{
    public string name;
    public int filesSize;
    public int totalSize;
    public List<string> files = new List<string>();
    public List<dir> subDirs = new List<dir>();

    public int GetDirTotal()
    {
        return totalSize = filesSize + subDirs.Select(d => d.GetDirTotal()).Sum();
    }

    public int GetSum100000()
    {
        var subsum = totalSize <= 100000 ? totalSize : 0;
        foreach (var dir in subDirs)
        {
            subsum += dir.GetSum100000();
        }
        return subsum;
    }

    public void FindBigEnought(List<int> sizes, int todelete)
    {
        if (totalSize > todelete)
            sizes.Add(totalSize);
        subDirs.ForEach(sd => sd.FindBigEnought(sizes, todelete));
    }
}
