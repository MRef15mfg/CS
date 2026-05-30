using System;
using System.Threading;

class Program
{
    static void PrintRange(object obj)
    {
        var range = (Tuple<int, int>)obj;

        for (int i = range.Item1; i <= range.Item2; i++)
        {
            Console.WriteLine(i);
            Thread.Sleep(30);
        }
    }

    static void Main()
    {
        Console.WriteLine("=== TASK 2 ===");

        Console.Write("Введи початок діапазону: ");
        int start = int.Parse(Console.ReadLine());

        Console.Write("Введи кінець діапазону: ");
        int end = int.Parse(Console.ReadLine());

        Console.WriteLine($"Запуск потоку ({start} → {end})...");
        Console.WriteLine("Натисни Enter");
        Console.ReadLine();

        Thread t = new Thread(PrintRange);
        t.Start(Tuple.Create(start, end));
        t.Join();

        Console.WriteLine("Готово!");
    }
}