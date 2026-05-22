using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            ShowProcesses();

            Console.WriteLine();
            Console.WriteLine("Введіть PID процесу для завершення (або 0 для виходу):");

            if (!int.TryParse(Console.ReadLine(), out int pid))
                continue;

            if (pid == 0)
                break;

            KillProcess(pid);

            Console.WriteLine("\nНатисніть Enter для оновлення...");
            Console.ReadLine();
        }
    }

    static void ShowProcesses()
    {
        var processes = Process.GetProcesses()
                               .OrderBy(p => p.Id);

        Console.WriteLine("PID\tІМ'Я");

        foreach (var p in processes)
        {
            string name;

            try
            {
                name = p.ProcessName;
            }
            catch
            {
                name = "немає доступу";
            }

            Console.WriteLine($"{p.Id}\t{name}");
        }
    }

    static void KillProcess(int pid)
    {
        try
        {
            var process = Process.GetProcessById(pid);
            process.Kill();
            Console.WriteLine($"Процес {pid} завершено.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}