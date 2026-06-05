using System;
using System.Threading.Tasks;

class Program
{
    static void ShowDateTime()
    {
        Console.WriteLine($"Дата і час: {DateTime.Now}");
    }

    static void Main()
    {
        Task task1 = new Task(ShowDateTime);
        task1.Start();
        task1.Wait();

        Task task2 = Task.Factory.StartNew(ShowDateTime);
        task2.Wait();

        Task task3 = Task.Run(ShowDateTime);
        task3.Wait();

        Console.WriteLine("Усі завдання завершено.");
    }
}