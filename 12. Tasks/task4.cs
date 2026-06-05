using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        int[] numbers = { 10, 5, 7, 15, 25, 2, 30 };

        Task<int> minTask = Task.Run(() => numbers.Min());

        Task<int> maxTask = Task.Run(() => numbers.Max());

        Task<double> avgTask = Task.Run(() => numbers.Average());

        Task<int> sumTask = Task.Run(() => numbers.Sum());

        Task[] tasks =
        {
            minTask,
            maxTask,
            avgTask,
            sumTask
        };

        Task.WaitAll(tasks);

        Console.WriteLine($"Мінімум: {minTask.Result}");
        Console.WriteLine($"Максимум: {maxTask.Result}");
        Console.WriteLine($"Середнє арифметичне: {avgTask.Result}");
        Console.WriteLine($"Сума: {sumTask.Result}");
    }
}