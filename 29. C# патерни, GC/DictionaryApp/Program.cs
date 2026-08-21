using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace DictionaryApp
{
    public class WordEntry
    {
        public string Word { get; set; } = string.Empty;
        public List<string> Translations { get; set; } = new List<string>();
    }

    public class LanguageDictionary
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Наприклад, "Англо-український"
        public List<WordEntry> Entries { get; set; } = new List<WordEntry>();
    }

    class Program
    {
        private static readonly string StorageFile = "dictionaries.json";
        private static List<LanguageDictionary> dictionaries = new List<LanguageDictionary>();
        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        static void Main(string[] args)
        {
            LoadData();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("========================================");
                Console.WriteLine("        ДОДАТОК «СЛОВНИКИ»");
                Console.WriteLine("========================================");
                Console.ResetColor();
                Console.WriteLine("1. Відкрити словник (робота зі словами)");
                Console.WriteLine("2. Створити новий словник");
                Console.WriteLine("3. Переглянути список усіх словників");
                Console.WriteLine("4. Видалити словник");
                Console.WriteLine("0. Вихід");
                Console.Write("\nОберіть дію: ");

                string choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1":
                        SelectDictionaryMenu();
                        break;
                    case "2":
                        CreateDictionary();
                        break;
                    case "3":
                        ListDictionaries();
                        break;
                    case "4":
                        DeleteDictionary();
                        break;
                    case "0":
                        SaveData();
                        return;
                    default:
                        ShowMessage("Невірний вибір!", ConsoleColor.Red);
                        break;
                }
            }
        }

        static void CreateDictionary()
        {
            Console.Clear();
            Console.WriteLine("--- Створення нового словника ---");
            Console.Write("Введіть назву словника: ");
            string name = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                ShowMessage("Назва не може бути порожньою!", ConsoleColor.Red);
                return;
            }

            if (dictionaries.Any(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                ShowMessage("Словник із такою назвою вже існує!", ConsoleColor.Red);
                return;
            }

            Console.Write("Введіть тип словника (наприклад, Англо-український): ");
            string type = Console.ReadLine()?.Trim();

            dictionaries.Add(new LanguageDictionary
            {
                Name = name,
                Type = string.IsNullOrWhiteSpace(type) ? "Загальний" : type
            });

            SaveData();
            ShowMessage("Словник успішно створено!", ConsoleColor.Green);
        }

        static void ListDictionaries()
        {
            Console.Clear();
            Console.WriteLine("--- Список словників ---");
            if (dictionaries.Count == 0)
            {
                Console.WriteLine("Немає жодного словника.");
            }
            else
            {
                for (int i = 0; i < dictionaries.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {dictionaries[i].Name} [{dictionaries[i].Type}] — Слів: {dictionaries[i].Entries.Count}");
                }
            }
            Pause();
        }

        static void DeleteDictionary()
        {
            Console.Clear();
            if (dictionaries.Count == 0)
            {
                ShowMessage("Немає доступних словників для видалення.", ConsoleColor.Yellow);
                return;
            }

            Console.WriteLine("--- Видалення словника ---");
            for (int i = 0; i < dictionaries.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dictionaries[i].Name} [{dictionaries[i].Type}]");
            }

            Console.Write("\nОберіть номер словника для видалення (або 0 для скасування): ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= dictionaries.Count)
            {
                string name = dictionaries[idx - 1].Name;
                dictionaries.RemoveAt(idx - 1);
                SaveData();
                ShowMessage($"Словник '{name}' успішно видалено!", ConsoleColor.Green);
            }
        }

        static void SelectDictionaryMenu()
        {
            if (dictionaries.Count == 0)
            {
                ShowMessage("Спочатку створіть хоча б один словник!", ConsoleColor.Yellow);
                return;
            }

            Console.Clear();
            Console.WriteLine("--- Оберіть словник ---");
            for (int i = 0; i < dictionaries.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dictionaries[i].Name} [{dictionaries[i].Type}]");
            }
            Console.WriteLine("0. Повернутися назад");
            Console.Write("\nВаш вибір: ");

            if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= dictionaries.Count)
            {
                DictionaryOperationsMenu(dictionaries[idx - 1]);
            }
        }

        static void DictionaryOperationsMenu(LanguageDictionary dict)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"=== Словник: {dict.Name} ({dict.Type}) ===");
                Console.ResetColor();
                Console.WriteLine("1. Переглянути всі слова");
                Console.WriteLine("2. Шукати переклад слова");
                Console.WriteLine("3. Додати нове слово і переклад");
                Console.WriteLine("4. Додати варіант перекладу до існуючого слова");
                Console.WriteLine("5. Замінити слово");
                Console.WriteLine("6. Замінити переклад слова");
                Console.WriteLine("7. Видалити слово");
                Console.WriteLine("8. Видалити варіант перекладу");
                Console.WriteLine("9. Експортувати слово у файл");
                Console.WriteLine("0. Повернутися до головного меню");
                Console.Write("\nОберіть дію: ");

                string choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1":
                        ShowAllWords(dict);
                        break;
                    case "2":
                        SearchWord(dict);
                        break;
                    case "3":
                        AddWord(dict);
                        break;
                    case "4":
                        AddTranslation(dict);
                        break;
                    case "5":
                        ReplaceWord(dict);
                        break;
                    case "6":
                        ReplaceTranslation(dict);
                        break;
                    case "7":
                        DeleteWord(dict);
                        break;
                    case "8":
                        DeleteTranslation(dict);
                        break;
                    case "9":
                        ExportWordToFile(dict);
                        break;
                    case "0":
                        return;
                    default:
                        ShowMessage("Невірний вибір!", ConsoleColor.Red);
                        break;
                }
            }
        }

        static void ShowAllWords(LanguageDictionary dict)
        {
            Console.Clear();
            Console.WriteLine($"--- Усі слова у словнику '{dict.Name}' ---");
            if (dict.Entries.Count == 0)
            {
                Console.WriteLine("Словник порожній.");
            }
            else
            {
                foreach (var entry in dict.Entries)
                {
                    Console.WriteLine($"• {entry.Word} -> {string.Join(", ", entry.Translations)}");
                }
            }
            Pause();
        }

        static void SearchWord(LanguageDictionary dict)
        {
            Console.Clear();
            Console.Write("Введіть слово для пошуку: ");
            string query = Console.ReadLine()?.Trim();

            var entry = dict.Entries.FirstOrDefault(e => e.Word.Equals(query, StringComparison.OrdinalIgnoreCase));
            if (entry != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nЗнайдено: {entry.Word}");
                Console.WriteLine($"Переклади: {string.Join(", ", entry.Translations)}");
                Console.ResetColor();
            }
            else
            {
                ShowMessage("Слово не знайдено у цьому словнику.", ConsoleColor.Red);
                return;
            }
            Pause();
        }

        static void AddWord(LanguageDictionary dict)
        {
            Console.Clear();
            Console.Write("Введіть нове слово: ");
            string word = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(word))
            {
                ShowMessage("Слово не може бути порожнім!", ConsoleColor.Red);
                return;
            }

            if (dict.Entries.Any(e => e.Word.Equals(word, StringComparison.OrdinalIgnoreCase)))
            {
                ShowMessage("Це слово вже є у словнику. Скористайтеся пунктом додавання перекладу.", ConsoleColor.Yellow);
                return;
            }

            Console.Write("Введіть переклад (якщо кілька, вкажіть через кому): ");
            string rawTrans = Console.ReadLine()?.Trim();
            var translations = rawTrans.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(t => t.Trim())
                                       .Where(t => !string.IsNullOrEmpty(t))
                                       .Distinct(StringComparer.OrdinalIgnoreCase)
                                       .ToList();

            if (translations.Count == 0)
            {
                ShowMessage("Потрібно вказати хоча б один варіант перекладу!", ConsoleColor.Red);
                return;
            }

            dict.Entries.Add(new WordEntry { Word = word, Translations = translations });
            SaveData();
            ShowMessage("Слово успішно додано!", ConsoleColor.Green);
        }

        static void AddTranslation(LanguageDictionary dict)
        {
            Console.Clear();
            Console.Write("Введіть слово, до якого треба додати переклад: ");
            string word = Console.ReadLine()?.Trim();

            var entry = dict.Entries.FirstOrDefault(e => e.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
            if (entry == null)
            {
                ShowMessage("Слово не знайдено!", ConsoleColor.Red);
                return;
            }

            Console.Write("Введіть новий варіант перекладу: ");
            string trans = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(trans))
            {
                ShowMessage("Переклад не може бути порожнім!", ConsoleColor.Red);
                return;
            }

            if (entry.Translations.Any(t => t.Equals(trans, StringComparison.OrdinalIgnoreCase)))
            {
                ShowMessage("Такий варіант перекладу вже існує!", ConsoleColor.Yellow);
                return;
            }

            entry.Translations.Add(trans);
            SaveData();
            ShowMessage("Переклад додано!", ConsoleColor.Green);
        }

        static void ReplaceWord(LanguageDictionary dict)
        {
            Console.Clear();
            Console.Write("Введіть слово, яке бажаєте замінити: ");
            string oldWord = Console.ReadLine()?.Trim();

            var entry = dict.Entries.FirstOrDefault(e => e.Word.Equals(oldWord, StringComparison.OrdinalIgnoreCase));
            if (entry == null)
            {
                ShowMessage("Слово не знайдено!", ConsoleColor.Red);
                return;
            }

            Console.Write("Введіть нове слово: ");
            string newWord = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(newWord))
            {
                ShowMessage("Нове слово не може бути порожнім!", ConsoleColor.Red);
                return;
            }

            entry.Word = newWord;
            SaveData();
            ShowMessage("Слово успішно замінено!", ConsoleColor.Green);
        }

        static void ReplaceTranslation(LanguageDictionary dict)
        {
            Console.Clear();
            Console.Write("Введіть слово: ");
            string word = Console.ReadLine()?.Trim();

            var entry = dict.Entries.FirstOrDefault(e => e.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
            if (entry == null)
            {
                ShowMessage("Слово не знайдено!", ConsoleColor.Red);
                return;
            }

            Console.WriteLine("Поточні переклади:");
            for (int i = 0; i < entry.Translations.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {entry.Translations[i]}");
            }

            Console.Write("\nОберіть номер перекладу для заміни: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= entry.Translations.Count)
            {
                Console.Write("Введіть новий переклад: ");
                string newTrans = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(newTrans))
                {
                    ShowMessage("Переклад не може бути порожнім!", ConsoleColor.Red);
                    return;
                }

                entry.Translations[idx - 1] = newTrans;
                SaveData();
                ShowMessage("Переклад замінено!", ConsoleColor.Green);
            }
        }

        static void DeleteWord(LanguageDictionary dict)
        {
            Console.Clear();
            Console.Write("Введіть слово для повного видалення: ");
            string word = Console.ReadLine()?.Trim();

            var entry = dict.Entries.FirstOrDefault(e => e.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
            if (entry == null)
            {
                ShowMessage("Слово не знайдено!", ConsoleColor.Red);
                return;
            }

            dict.Entries.Remove(entry);
            SaveData();
            ShowMessage($"Слово '{word}' та всі його переклади видалено!", ConsoleColor.Green);
        }

        static void DeleteTranslation(LanguageDictionary dict)
        {
            Console.Clear();
            Console.Write("Введіть слово: ");
            string word = Console.ReadLine()?.Trim();

            var entry = dict.Entries.FirstOrDefault(e => e.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
            if (entry == null)
            {
                ShowMessage("Слово не знайдено!", ConsoleColor.Red);
                return;
            }

            if (entry.Translations.Count <= 1)
            {
                ShowMessage("Неможливо видалити переклад: у слова залишився лише один варіант перекладу!", ConsoleColor.Red);
                return;
            }

            Console.WriteLine("Поточні переклади:");
            for (int i = 0; i < entry.Translations.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {entry.Translations[i]}");
            }

            Console.Write("\nОберіть номер перекладу для видалення: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= entry.Translations.Count)
            {
                entry.Translations.RemoveAt(idx - 1);
                SaveData();
                ShowMessage("Варіант перекладу видалено!", ConsoleColor.Green);
            }
        }

        static void ExportWordToFile(LanguageDictionary dict)
        {
            Console.Clear();
            Console.Write("Введіть слово для експорту: ");
            string word = Console.ReadLine()?.Trim();

            var entry = dict.Entries.FirstOrDefault(e => e.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
            if (entry == null)
            {
                ShowMessage("Слово не знайдено!", ConsoleColor.Red);
                return;
            }

            string fileName = $"export_{entry.Word}.txt";
            string content = $"Словник: {dict.Name} ({dict.Type})\n" +
                             $"Слово: {entry.Word}\n" +
                             $"Переклади:\n - {string.Join("\n - ", entry.Translations)}\n" +
                             $"Дата експорту: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

            try
            {
                File.WriteAllText(fileName, content);
                ShowMessage($"Дані успішно експортовано у файл '{fileName}'!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                ShowMessage($"Помилка запису файлу: {ex.Message}", ConsoleColor.Red);
            }
        }

        static void SaveData()
        {
            try
            {
                string json = JsonSerializer.Serialize(dictionaries, jsonOptions);
                File.WriteAllText(StorageFile, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка збереження даних: {ex.Message}");
            }
        }

        static void LoadData()
        {
            try
            {
                if (File.Exists(StorageFile))
                {
                    string json = File.ReadAllText(StorageFile);
                    dictionaries = JsonSerializer.Deserialize<List<LanguageDictionary>>(json, jsonOptions) ?? new List<LanguageDictionary>();
                }
            }
            catch
            {
                dictionaries = new List<LanguageDictionary>();
            }
        }

        static void ShowMessage(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"\n{msg}");
            Console.ResetColor();
            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
            Console.ReadKey(true);
        }
    }
}
