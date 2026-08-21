using System;

namespace ArrayInterfacesApp
{
    public interface IOutput
    {
        void Show();
        void Show(string info);
    }

    public interface IMath
    {
        int Max();
        int Min();
        float Avg();
        bool Search(int valueToSearch);
    }

    public interface ISort
    {
        void SortAsc();
        void SortDesc();
        void SortByParam(bool isAsc);
    }

    public class Array : IOutput, IMath, ISort
    {
        private int[] _data;

        public Array(int[] data)
        {
            _data = (int[])data.Clone();
        }

        public Array(int size)
        {
            _data = new int[size];
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                _data[i] = rnd.Next(1, 100);
            }
        }

        public void Show()
        {
            Console.WriteLine(string.Join(" ", _data));
        }

        public void Show(string info)
        {
            Console.WriteLine($"{info}: {string.Join(" ", _data)}");
        }

        public int Max()
        {
            if (_data.Length == 0) throw new InvalidOperationException("Масив порожній");

            int max = _data[0];
            for (int i = 1; i < _data.Length; i++)
            {
                if (_data[i] > max)
                {
                    max = _data[i];
                }
            }
            return max;
        }

        public int Min()
        {
            if (_data.Length == 0) throw new InvalidOperationException("Масив порожній");

            int min = _data[0];
            for (int i = 1; i < _data.Length; i++)
            {
                if (_data[i] < min)
                {
                    min = _data[i];
                }
            }
            return min;
        }

        public float Avg()
        {
            if (_data.Length == 0) throw new InvalidOperationException("Масив порожній");

            int sum = 0;
            for (int i = 0; i < _data.Length; i++)
            {
                sum += _data[i];
            }
            return (float)sum / _data.Length;
        }

        public bool Search(int valueToSearch)
        {
            for (int i = 0; i < _data.Length; i++)
            {
                if (_data[i] == valueToSearch)
                {
                    return true;
                }
            }
            return false;
        }

        public void SortAsc()
        {
            System.Array.Sort(_data);
        }

        public void SortDesc()
        {
            System.Array.Sort(_data);
            System.Array.Reverse(_data);
        }

        public void SortByParam(bool isAsc)
        {
            if (isAsc)
            {
                SortAsc();
            }
            else
            {
                SortDesc();
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Array numbers = new Array(new int[] { 45, 12, 78, 23, 89, 5, 34 });

            numbers.Show();
            numbers.Show("Початковий масив");

            Console.WriteLine($"\nМаксимум: {numbers.Max()}");
            Console.WriteLine($"Мінімум: {numbers.Min()}");
            Console.WriteLine($"Середнє арифметичне: {numbers.Avg():F2}");

            int value = 23;
            Console.WriteLine($"Чи є число {value} у масиві: {numbers.Search(value)}");
            Console.WriteLine($"Чи є число 999 у масиві: {numbers.Search(999)}");

            numbers.SortAsc();
            numbers.Show("\nСортування за зростанням");

            numbers.SortDesc();
            numbers.Show("Сортування за спаданням");

            numbers.SortByParam(true);
            numbers.Show("Сортування через SortByParam(true)");

            numbers.SortByParam(false);
            numbers.Show("Сортування через SortByParam(false)");
        }
    }
}
