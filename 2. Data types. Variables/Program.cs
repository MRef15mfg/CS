namespace MyNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[5];
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"Число {i + 1} : ");
                arr[i] = Convert.ToInt32(Console.ReadLine());
            }
            
            int sum = 0, max = arr[0], min = arr[0], product = 1;
            foreach (int num in arr)
            {
                sum += num;
                product *= num;
                if (num > max)
                {
                    max = num;
                }
                if (num < min)
                {
                    min = num;
                }
            }


            Console.Write("\nЧисла : ");
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == arr.Length - 1)
                    Console.WriteLine(arr[i]);
                else
                    Console.Write(arr[i] + ", ");
            }
            Console.WriteLine("Сумма : " + sum);
            Console.WriteLine("Максимум : " + max);
            Console.WriteLine("Мінимум : " + min);
            Console.WriteLine("Добуток : " + product);
        }
    }
}