using System;

class Program
{
    static void Main()
    {
        try
        {
            Console.Write("Введіть вираз: ");
            string expression = Console.ReadLine();

            string[] numbers = expression.Split('*');

            int result = 1;

            foreach (string item in numbers)
            {
                result *= int.Parse(item);
            }

            Console.WriteLine("Результат: " + result);
        }
        catch (FormatException)
        {
            Console.WriteLine("Помилка у введеному виразі.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Число занадто велике.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}