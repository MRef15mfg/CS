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

        Console.WriteLine("Введіть:");
        Console.WriteLine("time - поточний час");
        Console.WriteLine("date - поточна дата");

        string request = Console.ReadLine();

        byte[] buffer = Encoding.UTF8.GetBytes(request);

        stream.Write(buffer, 0, buffer.Length);

        buffer = new byte[1024];

        int count = stream.Read(buffer, 0, buffer.Length);

        string answer = Encoding.UTF8.GetString(buffer, 0, count);

        Console.WriteLine("Відповідь сервера: " + answer);

        stream.Close();
        client.Close();
    }
}