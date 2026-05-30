using System;
using System.Threading;

class Program
{
    static int[] numbers = new int[10000];

    static void Generate()
    {
        Random r = new Random();
        for (int i = 0; i < numbers.Length; i++)
            numbers[i] = r.Next(1, 100);
    }

    static void Print()
    {
        Console.WriteLine("Вивід масиву:");
        foreach (var n in numbers)
            Console.Write(n + " ");
    }

    static void Main()
    {
        Console.WriteLine("=== TASK 5 ===");
        Console.WriteLine("Генерація масиву...");

        Thread t1 = new Thread(Generate);
        t1.Start();
        t1.Join();

        Console.WriteLine("Запуск потоку виводу...");
        Console.ReadLine();

        Thread t2 = new Thread(Print);
        t2.Start();
        t2.Join();

        Console.WriteLine("\nГотово!");
    }
}