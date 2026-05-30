using System;

class Program
{
    static void Main()
    {
        string s = "level";

        s = s.ToLower().Replace(" ", "");

        int i = 0, j = s.Length - 1;
        bool ok = true;

        while (i < j)
        {
            if (s[i] != s[j])
            {
                ok = false;
                break;
            }
            i++;
            j--;
        }

        Console.WriteLine(ok ? "Palindrome" : "Not palindrome");
    }
}