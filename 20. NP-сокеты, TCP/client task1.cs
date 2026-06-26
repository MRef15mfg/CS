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

        string message = "Привіт, сервер!";

        byte[] buffer = Encoding.UTF8.GetBytes(message);

        stream.Write(buffer, 0, buffer.Length);

        buffer = new byte[1024];

        int count = stream.Read(buffer, 0, buffer.Length);

        string answer = Encoding.UTF8.GetString(buffer, 0, count);

        Console.WriteLine($"О {DateTime.Now:HH:mm} отримано рядок: {answer}");

        stream.Close();
        client.Close();
    }
}