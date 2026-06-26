using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        TcpListener server = new TcpListener(IPAddress.Any, 5000);
        server.Start();

        Console.WriteLine("Сервер запущено...");

        TcpClient client = server.AcceptTcpClient();

        NetworkStream stream = client.GetStream();

        byte[] buffer = new byte[1024];

        int count = stream.Read(buffer, 0, buffer.Length);

        string message = Encoding.UTF8.GetString(buffer, 0, count);

        IPEndPoint endPoint = (IPEndPoint)client.Client.RemoteEndPoint;

        Console.WriteLine($"О {DateTime.Now:HH:mm} від {endPoint.Address} отримано рядок: {message}");

        string answer = "Привіт, клієнт!";

        buffer = Encoding.UTF8.GetBytes(answer);

        stream.Write(buffer, 0, buffer.Length);

        stream.Close();
        client.Close();
        server.Stop();
    }
}