using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        int[] array = { 5, 3, 8, 1, 3, 7, 8, 2, 5, 9 };
        int searchValue = 7;

        Task<int[]> removeDuplicatesTask = Task.Run(() =>
        {
            Console.WriteLine("Видалення дублікатів...");
            return array.Distinct().ToArray();
        });

        Task<int[]> sortTask = removeDuplicatesTask.ContinueWith(task =>
        {
            Console.WriteLine("Сортування...");
            int[] result = task.Result;
            Array.Sort(result);
            return result;
        });

        Task searchTask = sortTask.ContinueWith(task =>
        {
            Console.WriteLine("Бінарний пошук...");

            int index = Array.BinarySearch(task.Result, searchValue);

            Console.WriteLine("Відсортований масив:");
            foreach (int item in task.Result)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();

            if (index >= 0)
                Console.WriteLine($"Число {searchValue} знайдено на позиції {index}");
            else
                Console.WriteLine($"Число {searchValue} не знайдено");
        });

        searchTask.Wait();

        Console.WriteLine("\nУсі операції завершено.");
    }
}