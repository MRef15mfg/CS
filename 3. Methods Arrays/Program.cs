namespace MyNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] arr = new int[5, 5];
            Random rnd = new Random();

            int min = 0, max = 0;
            int minI = 0, minJ = 0, maxI = 0, maxJ = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    arr[i, j] = rnd.Next(-100, 101);

                    if (i == 0 && j == 0)
                    {
                        min = max = arr[i, j];
                    }

                    if (arr[i, j] < min)
                    {
                        min = arr[i, j];
                        minI = i;
                        minJ = j;
                    }

                    if (arr[i, j] > max)
                    {
                        max = arr[i, j];
                        maxI = i;
                        maxJ = j;
                    }
                }
            }

            int sum = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if ((i > minI || (i == minI && j > minJ)) &&
                        (i < maxI || (i == maxI && j < maxJ)))
                    {
                        sum += arr[i, j];
                    }
                }
            }

            Console.WriteLine("\nЧисла : ");

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(arr[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nСума між min і max : " + sum);
        }
    }
}