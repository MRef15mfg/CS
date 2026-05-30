using System;

class Program
{
    static void Main()
    {
        string text = "   Hello   world   CSharp   is   cool   ";

        string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        string result = string.Join("*", words);

        Console.WriteLine(result);
    }
}