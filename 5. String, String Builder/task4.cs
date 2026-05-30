using System;

class Program
{
    static void Main()
    {
        string[] words = { "hello", "world", "programming", "cat" };
        int len = 5;

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length == len && words[i].Length >= 3)
            {
                words[i] = words[i].Substring(0, words[i].Length - 3) + "$$$";
            }
        }

        Console.WriteLine(string.Join(" ", words));
    }
}