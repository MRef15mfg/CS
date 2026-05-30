using System;

class Program
{
    static void Main()
    {
        string text = "Hello WORLD CSharp";

        int upper = 0, lower = 0, total = text.Length;

        foreach (char c in text)
        {
            if (char.IsUpper(c)) upper++;
            else if (char.IsLower(c)) lower++;
        }

        Console.WriteLine("Uppercase: " + (upper * 100.0 / total) + "%");
        Console.WriteLine("Lowercase: " + (lower * 100.0 / total) + "%");
    }
}