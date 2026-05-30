using System;

class Program
{
    static void Main()
    {
        try
        {
            Console.Write("Введіть число: ");
            string input = Console.ReadLine();

            int number = int.Parse(input);

            Console.WriteLine("Результат: " + number);
        }
        catch (OverflowException)
        {
            Console.WriteLine("Число виходить за межі типу int.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Введено некоректні дані.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}