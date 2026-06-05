using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Копіювання файлу ===");

        Console.Write("From (шлях до файлу): ");
        string sourceFile = Console.ReadLine();

        Console.Write("To (шлях до папки): ");
        string destinationFolder = Console.ReadLine();

        try
        {
            if (!File.Exists(sourceFile))
            {
                Console.WriteLine("Помилка: файл не знайдено!");
                return;
            }

            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            string destinationFile =
                Path.Combine(destinationFolder, Path.GetFileName(sourceFile));

            File.Copy(sourceFile, destinationFile, true);

            Console.WriteLine("Файл успішно скопійовано!");
            Console.WriteLine($"Новий шлях: {destinationFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }

        Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }
}