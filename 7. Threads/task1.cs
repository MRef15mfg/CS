using System;
using System.Threading;

class Program
{
    static void PrintNumbers()
    {
        for (int i = 0; i <= 50; i++)
        {
            Console.WriteLine(i);
            Thread.Sleep(50);
        }
    }

    static void Main()
    {
        Console.WriteLine("=== TASK 1 ===");
        Console.WriteLine("Запуск потоку, який виводить числа від 0 до 50...");
        Console.WriteLine("Натисни Enter для старту");
        Console.ReadLine();

        Thread t = new Thread(PrintNumbers);
        t.Start();
        t.Join();

        Console.WriteLine("Готово!");
    }
}