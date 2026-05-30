using System;
using System.IO;
using System.Threading;

class Statistics
{
    public int Words;
    public int Lines;
    public int Punctuation;

    public void Add(int words, int lines, int punctuation)
    {
        Interlocked.Add(ref Words, words);
        Interlocked.Add(ref Lines, lines);
        Interlocked.Add(ref Punctuation, punctuation);
    }
}

class Program
{
    static Statistics stats = new Statistics();

    static char[] signs =
    {
        '.', ',', ';', ':', '-', '—', '‒', '…', '!',
        '?', '"', '\'', '«', '»', '(', ')',
        '{', '}', '[', ']', '<', '>', '/'
    };

    static void AnalyzeFile(object obj)
    {
        string filePath = (string)obj;

        int words = 0;
        int lines = 0;
        int punctuation = 0;

        string[] textLines = File.ReadAllLines(filePath);
        lines = textLines.Length;

        foreach (string line in textLines)
        {
            string[] temp = line.Split(
                new char[] { ' ', '\t' },
                StringSplitOptions.RemoveEmptyEntries);

            words += temp.Length;
        }

        string text = File.ReadAllText(filePath);

        foreach (char c in text)
        {
            foreach (char sign in signs)
            {
                if (c == sign)
                {
                    punctuation++;
                    break;
                }
            }
        }

        stats.Add(words, lines, punctuation);

        Console.WriteLine("Файл: " + Path.GetFileName(filePath));
        Console.WriteLine("Слів: " + words);
        Console.WriteLine("Рядків: " + lines);
        Console.WriteLine("Розділових знаків: " + punctuation);
        Console.WriteLine();
    }

    static void Main()
    {
        try
        {
            Console.Write("Введіть шлях до папки: ");
            string path = Console.ReadLine();

            if (!Directory.Exists(path))
            {
                Console.WriteLine("Папку не знайдено.");
                return;
            }

            string[] files = Directory.GetFiles(path, "*.txt");

            Thread[] threads = new Thread[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                threads[i] = new Thread(AnalyzeFile);
                threads[i].Start(files[i]);
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }

            Console.WriteLine("===== Загальна статистика =====");
            Console.WriteLine("Слів: " + stats.Words);
            Console.WriteLine("Рядків: " + stats.Lines);
            Console.WriteLine("Розділових знаків: " + stats.Punctuation);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
    }
}