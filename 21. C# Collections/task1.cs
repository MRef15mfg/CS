using System;

class Program
{
    static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }

    static void Main()
    {
        int x = 10, y = 20;

        Console.WriteLine($"До swap: x={x}, y={y}");
        Swap(ref x, ref y);
        Console.WriteLine($"Після swap: x={x}, y={y}");

        string a = "Hello";
        string b = "World";

        Console.WriteLine($"\nДо swap: a={a}, b={b}");
        Swap(ref a, ref b);
        Console.WriteLine($"Після swap: a={a}, b={b}");
    }
}