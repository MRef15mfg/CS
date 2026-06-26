using System;

class Program
{
    static void Main()
    {
        Action showTime = () =>
            Console.WriteLine("Час: " + DateTime.Now.ToLongTimeString());

        Action showDate = () =>
            Console.WriteLine("Дата: " + DateTime.Now.ToShortDateString());

        Action showDay = () =>
            Console.WriteLine("День тижня: " + DateTime.Now.DayOfWeek);

        Func<double, double, double> triangleArea = (a, h) => a * h / 2;

        Func<double, double, double> rectangleArea = (a, b) => a * b;

        Predicate<int> isPositive = x => x > 0;

        showTime();
        showDate();
        showDay();

        Console.WriteLine("Площа трикутника = " + triangleArea(10, 5));
        Console.WriteLine("Площа прямокутника = " + rectangleArea(5, 8));

        Console.WriteLine(isPositive(15));
    }
}