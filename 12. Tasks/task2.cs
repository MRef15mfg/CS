using System;
using System.Threading.Tasks;

class Program
{
    static bool IsPrime(int number)
    {
        if (number < 2)
            return false;

        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
                return false;
        }

        return true;
    }

    static void Main()
    {
        Task task = Task.Run(() =>
        {
            Console.WriteLine("Прості числа:");

            for (int i = 0; i <= 1000; i++)
            {
                if (IsPrime(i))
                    Console.Write(i + " ");
            }
        });

        task.Wait();

        Console.WriteLine("\n\nЗавдання завершено.");
    }
}