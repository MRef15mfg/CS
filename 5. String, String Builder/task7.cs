using System;
using System.Text;

class Program
{
    static void Main()
    {
        StringBuilder sb = new StringBuilder();

        while (true)
        {
            string word = Console.ReadLine();

            if (sb.Length > 0)
                sb.Append(", ");

            sb.Append(word);

            if (word.EndsWith("."))
                break;
        }

        Console.WriteLine(sb.ToString());
    }
}