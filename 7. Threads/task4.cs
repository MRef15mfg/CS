using System;
using System.Threading;

class Program
{
    static int[] numbers = new int[10000];

    static int min, max;
    static double avg;

    static void Generate()
    {
        Random r = new Random();
        for (int i = 0; i < numbers.Length; i++)
            numbers[i] = r.Next(1, 1000);
    }

    static void FindMin()
    {
        min = numbers[0];
        foreach (var n in numbers)
            if (n < min) min = n;
    }

    static void FindMax()
    {
        max = numbers[0];
        foreach (var n in numbers)
            if (n > max) max = n;
    }

    static void FindAvg()
    {
        long sum = 0;
        foreach (var n in numbers)
            sum += n;

        avg = sum / (double)numbers.Length;
    }

    static void Main()
    {
        Console.WriteLine("=== TASK 4 ===");
        Console.WriteLine("Генерація 10000 чисел...");

        Thread t1 = new Thread(Generate);
        t1.Start();
        t1.Join();

        Console.WriteLine("Обробка даних потоками...");

        Thread t2 = new Thread(FindMin);
        Thread t3 = new Thread(FindMax);
        Thread t4 = new Thread(FindAvg);

        t2.Start();
        t3.Start();
        t4.Start();

        t2.Join();
        t3.Join();
        t4.Join();

        Console.WriteLine("\n=== РЕЗУЛЬТАТ ===");
        Console.WriteLine($"Min: {min}");
        Console.WriteLine($"Max: {max}");
        Console.WriteLine($"Avg: {avg:F2}");
    }
}