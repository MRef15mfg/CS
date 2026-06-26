using System;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        TcpClient client = new TcpClient();
        client.Connect("127.0.0.1", 5000);

        NetworkStream stream = client.GetStream();

        Console.WriteLine("Підключено до сервера цитат");
        Console.WriteLine("Enter = нова цитата, exit = вихід");

        while (true)
        {
            string input = Console.ReadLine();

            if (input == "exit")
            {
                byte[] exitMsg = Encoding.UTF8.GetBytes("exit");
                stream.Write(exitMsg, 0, exitMsg.Length);
                break;
            }

            if (string.IsNullOrWhiteSpace(input))
                input = "quote";

            byte[] data = Encoding.UTF8.GetBytes(input);
            stream.Write(data, 0, data.Length);

            byte[] buffer = new byte[1024];
            int count = stream.Read(buffer, 0, buffer.Length);

            string quote = Encoding.UTF8.GetString(buffer, 0, count);

            Console.WriteLine("Цитата: " + quote);
        }

        stream.Close();
        client.Close();
    }
}