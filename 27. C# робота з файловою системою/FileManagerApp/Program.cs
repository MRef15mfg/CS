using System;
using System.IO;
using System.Linq;

namespace ConsoleFileManager
{
    class Program
    {
        static string currentDir = Directory.GetCurrentDirectory();
        static int selectedIndex = 0;

        static void Main()
        {
            Console.CursorVisible = false;

            while (true)
            {
                DrawInterface();
                ConsoleKeyInfo key = Console.ReadKey(true);

                var (entries, isDir) = GetCurrentEntries();
                int totalCount = entries.Length;

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedIndex > 0) selectedIndex--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectedIndex < totalCount - 1) selectedIndex++;
                        break;

                    case ConsoleKey.Enter:
                        if (totalCount > 0)
                        {
                            string target = entries[selectedIndex];
                            if (isDir[selectedIndex])
                            {
                                currentDir = target;
                                selectedIndex = 0;
                            }
                            else
                            {
                                ViewFile(target);
                            }
                        }
                        break;

                    case ConsoleKey.Backspace:
                        DirectoryInfo parent = Directory.GetParent(currentDir);
                        if (parent != null)
                        {
                            currentDir = parent.FullName;
                            selectedIndex = 0;
                        }
                        break;

                    case ConsoleKey.D:
                    case ConsoleKey.Delete:
                        if (totalCount > 0)
                        {
                            DeleteItem(entries[selectedIndex], isDir[selectedIndex]);
                        }
                        break;

                    case ConsoleKey.R:
                        if (totalCount > 0)
                        {
                            RenameItem(entries[selectedIndex], isDir[selectedIndex]);
                        }
                        break;

                    case ConsoleKey.Q:
                    case ConsoleKey.Escape:
                        Console.ResetColor();
                        Console.Clear();
                        Console.CursorVisible = true;
                        return;
                }
            }
        }

        static (string[] paths, bool[] isDir) GetCurrentEntries()
        {
            try
            {
                var dirs = Directory.GetDirectories(currentDir);
                var files = Directory.GetFiles(currentDir);

                var allPaths = dirs.Concat(files).ToArray();
                var isDirList = Enumerable.Repeat(true, dirs.Length)
                                          .Concat(Enumerable.Repeat(false, files.Length))
                                          .ToArray();

                return (allPaths, isDirList);
            }
            catch
            {
                return (new string[0], new bool[0]);
            }
        }

        static void DrawInterface()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine($" ПОТОЧНА ПАПКА: {currentDir}");
            Console.WriteLine("================================================================================");
            Console.ResetColor();

            var (entries, isDir) = GetCurrentEntries();

            if (entries.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  [Папка порожня або доступ обмежено]");
                Console.ResetColor();
            }
            else
            {
                for (int i = 0; i < entries.Length; i++)
                {
                    string name = Path.GetFileName(entries[i]);
                    string prefix = isDir[i] ? "[DIR] " : "      ";

                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($" > {prefix}{name,-65}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = isDir[i] ? ConsoleColor.Yellow : ConsoleColor.Gray;
                        Console.WriteLine($"   {prefix}{name,-65}");
                        Console.ResetColor();
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" [↑/↓] Навігація | [Enter] Відкрити | [Backspace] Назад | [R] Перейменувати");
            Console.WriteLine(" [D] Видалити    | [Q / Esc] Вихід");
            Console.ResetColor();
        }

        static void ViewFile(string filePath)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"=== Перегляд файлу: {Path.GetFileName(filePath)} (Будь-яка клавіша для повернення) ===");
            Console.ResetColor();

            try
            {
                string text = File.ReadAllText(filePath);
                Console.WriteLine(text);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Помилка читання файлу: {ex.Message}");
                Console.ResetColor();
            }

            Console.ReadKey(true);
        }

        static void DeleteItem(string path, bool isDirectory)
        {
            string name = Path.GetFileName(path);
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Видалити {name}? (y/n): ");
            Console.ResetColor();

            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Y)
            {
                try
                {
                    if (isDirectory)
                        Directory.Delete(path, true);
                    else
                        File.Delete(path);

                    if (selectedIndex > 0) selectedIndex--;
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        static void RenameItem(string path, bool isDirectory)
        {
            Console.CursorVisible = true;
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Введіть нове ім'я: ");
            Console.ResetColor();

            string newName = Console.ReadLine();
            Console.CursorVisible = false;

            if (!string.IsNullOrWhiteSpace(newName))
            {
                string destination = Path.Combine(currentDir, newName);
                try
                {
                    if (isDirectory)
                    {
                        Directory.Move(path, destination);
                    }
                    else
                    {
                        File.Move(path, destination);
                    }
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nПомилка: {message}");
            Console.ResetColor();
            Console.WriteLine("Натисніть клавішу для продовження...");
            Console.ReadKey(true);
        }
    }
}
