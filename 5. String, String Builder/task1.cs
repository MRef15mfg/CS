using System;

class Program
{
    static void Main()
    {
        string s = "Hello World";
        string insert = "BIG ";
        int pos = 6;

        string result = s.Insert(pos, insert);

        Console.WriteLine(result);
    }
}