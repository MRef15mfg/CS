using System;

delegate int StringDelegate(string text);

class Program
{
    static int CountVowels(string text)
    {
        string vowels = "аеєиіїоуюяAEIOUYaeiouyАЕЄИІЇОУЮЯ";
        int count = 0;

        foreach (char c in text)
        {
            if (vowels.Contains(c))
                count++;
        }

        return count;
    }

    static int CountConsonants(string text)
    {
        string vowels = "аеєиіїоуюяAEIOUYaeiouyАЕЄИІЇОУЮЯ";
        int count = 0;

        foreach (char c in text)
        {
            if (char.IsLetter(c) && !vowels.Contains(c))
                count++;
        }

        return count;
    }

    static int GetLength(string text)
    {
        return text.Length;
    }

    static void Main()
    {
        string str = "Hello World";

        StringDelegate del;

        del = CountVowels;
        Console.WriteLine("Голосних: " + del(str));

        del = CountConsonants;
        Console.WriteLine("Приголосних: " + del(str));

        del = GetLength;
        Console.WriteLine("Довжина: " + del(str));
    }
}