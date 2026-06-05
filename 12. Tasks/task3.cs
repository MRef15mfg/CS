using System;
using System.Collections.Generic;
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
        int start = 100;
        int end = 1000;

        Task<List<int>> task = Task.Run(() =>
        {
            List<int> primes = new List<int>();

            for (int i = start; i <= end; i++)
            {
                if (IsPrime(i))
                    primes.Add(i);
            }

            return primes;
        });

        task.Wait();

        Console.WriteLine("Прості числа:");
        foreach (int number in task.Result)
        {
            Console.Write(number + " ");
        }

        Console.WriteLine($"\n\nКількість простих чисел: {task.Result.Count}");
    }
}