// See https://aka.ms/new-console-template for more information
#define READALL
Console.WriteLine("15 dec, Hello, World!");

int result = 0;

#if READALL
var input1 = File.ReadAllLines("input1.txt");
#else
using (var sr = new StreamReader("input1.txt"))
{
    string line;
    while ((line = sr.ReadLine()) != null){

    }
}
#endif


Console.WriteLine(partA());
Console.WriteLine(partB());

int partA()
{
    int result = 0;
    foreach (var line in input1)
    {
    }
    return result;
}



int partB()
{
    int result = 0;
    foreach (var line in input1)
    {
    }
    return result;
}

T[,] ReadAndCreateMap<T>(Func<char, T> remap) where T : new()
{
    T[,] ret = new T[input1.Length, input1[0].Length];
    for (int r = 0; r < input1.Length; r++)
        for (int c = 0; c < input1[0].Length; c++)
    {
            ret[r, c] = remap(input1[r][c]);
    }
}