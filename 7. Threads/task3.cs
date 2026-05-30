using System;
using System.Threading;

class Program
{
    static void Worker(object obj)
    {
        var data = (Tuple<int, int, int, int>)obj;

        int start = data.Item1;
        int end = data.Item2;
        int id = data.Item3;
        int threads = data.Item4;

        for (int i = start + id; i <= end; i += threads)
        {
            Console.WriteLine($"T{id}: {i}");
        }
    }

    static void Main()
    {
        Console.WriteLine("=== TASK 3 ===");

        Console.Write("Початок: ");
        int start = int.Parse(Console.ReadLine());

        Console.Write("Кінець: ");
        int end = int.Parse(Console.ReadLine());

        Console.Write("Кількість потоків: ");
        int threadsCount = int.Parse(Console.ReadLine());

        Console.WriteLine("Запуск потоків...");
        Console.WriteLine();

        Thread[] threads = new Thread[threadsCount];

        for (int i = 0; i < threadsCount; i++)
        {
            threads[i] = new Thread(Worker);
            threads[i].Start(Tuple.Create(start, end, i, threadsCount));
        }

        for (int i = 0; i < threadsCount; i++)
            threads[i].Join();

        Console.WriteLine("\nГотово!");
    }
}