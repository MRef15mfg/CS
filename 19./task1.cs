using System;
using System.Linq;

delegate int[] ArrayDelegate(int[] array);

class Program
{
    static int[] GetEven(int[] array)
    {
        return array.Where(x => x % 2 == 0).ToArray();
    }

    static int[] GetOdd(int[] array)
    {
        return array.Where(x => x % 2 != 0).ToArray();
    }

    static bool IsPrime(int n)
    {
        if (n < 2)
            return false;

        for (int i = 2; i <= Math.Sqrt(n); i++)
            if (n % i == 0)
                return false;

        return true;
    }

    static int[] GetPrime(int[] array)
    {
        return array.Where(IsPrime).ToArray();
    }

    static bool IsFibonacci(int n)
    {
        int a = 0, b = 1;

        while (b < n)
        {
            int temp = a + b;
            a = b;
            b = temp;
        }

        return n == 0 || b == n;
    }

    static int[] GetFibonacci(int[] array)
    {
        return array.Where(IsFibonacci).ToArray();
    }

    static void Main()
    {
        int[] arr = { 2, 3, 4, 5, 8, 13, 21, 22, 25 };

        ArrayDelegate del;

        del = GetEven;
        Console.WriteLine("Парні:");
        Console.WriteLine(string.Join(" ", del(arr)));

        del = GetOdd;
        Console.WriteLine("Непарні:");
        Console.WriteLine(string.Join(" ", del(arr)));

        del = GetPrime;
        Console.WriteLine("Прості:");
        Console.WriteLine(string.Join(" ", del(arr)));

        del = GetFibonacci;
        Console.WriteLine("Фібоначчі:");
        Console.WriteLine(string.Join(" ", del(arr)));
    }
}