using System;

class Program
{
    static void Main()
    {
        string text = "CSharp is very powerful language";
        int number = 2;

        string[] words = text.Split(' ');

        Console.WriteLine(words[number][0]);
    }
}