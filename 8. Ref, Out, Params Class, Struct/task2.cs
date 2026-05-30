using System;

class Calculator
{
    public double Add(double a, double b)
    {
        return a + b;
    }

    public double Sub(double a, double b)
    {
        return a - b;
    }

    public double Mul(double a, double b)
    {
        return a * b;
    }

    public double Div(double a, double b)
    {
        if (b == 0)
            throw new DivideByZeroException("Ділення на нуль неможливе.");

        return a / b;
    }
}

class Program
{
    static void Main()
    {
        Calculator calculator = new Calculator();

        try
        {
            Console.Write("Введіть перше число: ");
            double a = double.Parse(Console.ReadLine());

            Console.Write("Введіть друге число: ");
            double b = double.Parse(Console.ReadLine());

            Console.WriteLine("\nОберіть операцію:");
            Console.WriteLine("1 - Додавання");
            Console.WriteLine("2 - Віднімання");
            Console.WriteLine("3 - Множення");
            Console.WriteLine("4 - Ділення");

            Console.Write("Ваш вибір: ");
            int choice = int.Parse(Console.ReadLine());

            double result;

            switch (choice)
            {
                case 1:
                    result = calculator.Add(a, b);
                    break;

                case 2:
                    result = calculator.Sub(a, b);
                    break;

                case 3:
                    result = calculator.Mul(a, b);
                    break;

                case 4:
                    result = calculator.Div(a, b);
                    break;

                default:
                    throw new ArgumentException("Невірно обрана операція.");
            }

            Console.WriteLine($"\nРезультат: {result}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Помилка формату введених даних.");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}