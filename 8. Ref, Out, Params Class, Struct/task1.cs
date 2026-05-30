using System;

class Worker
{
    private string fullName;
    private int age;
    private decimal salary;
    private DateTime hireDate;

    public string FullName
    {
        get { return fullName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Прізвище та ініціали не можуть бути порожніми.");
            fullName = value;
        }
    }

    public int Age
    {
        get { return age; }
        set
        {
            if (value < 18 || value > 100)
                throw new ArgumentException("Некоректний вік.");
            age = value;
        }
    }

    public decimal Salary
    {
        get { return salary; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Некоректна зарплата.");
            salary = value;
        }
    }

    public DateTime HireDate
    {
        get { return hireDate; }
        set
        {
            if (value > DateTime.Now)
                throw new ArgumentException("Некоректна дата прийняття на роботу.");
            hireDate = value;
        }
    }
}

class Program
{
    static void Main()
    {
        Worker[] workers = new Worker[5];

        for (int i = 0; i < workers.Length; i++)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine($"\nПрацівник #{i + 1}");

                    Worker worker = new Worker();

                    Console.Write("Прізвище та ініціали: ");
                    worker.FullName = Console.ReadLine();

                    Console.Write("Вік: ");
                    worker.Age = int.Parse(Console.ReadLine());

                    Console.Write("Заробітна плата: ");
                    worker.Salary = decimal.Parse(Console.ReadLine());

                    Console.Write("Дата прийняття на роботу (дд.мм.рррр): ");
                    worker.HireDate = DateTime.Parse(Console.ReadLine());

                    workers[i] = worker;
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
            }
        }

        Array.Sort(workers, (a, b) => a.FullName.CompareTo(b.FullName));

        Console.Write("\nВведіть мінімальний стаж роботи (років): ");
        int years = int.Parse(Console.ReadLine());

        Console.WriteLine("\nПрацівники зі стажем більше заданого:");

        foreach (Worker worker in workers)
        {
            int experience = DateTime.Now.Year - worker.HireDate.Year;

            if (worker.HireDate > DateTime.Now.AddYears(-experience))
                experience--;

            if (experience > years)
                Console.WriteLine(worker.FullName);
        }
    }
}