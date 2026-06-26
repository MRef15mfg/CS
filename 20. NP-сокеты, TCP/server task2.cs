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

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();

            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[1024];

            int count = stream.Read(buffer, 0, buffer.Length);

            string request = Encoding.UTF8.GetString(buffer, 0, count);

            string answer = "";

            if (request.ToLower() == "time")
                answer = DateTime.Now.ToLongTimeString();
            else if (request.ToLower() == "date")
                answer = DateTime.Now.ToShortDateString();
            else
                answer = "Невідома команда";

            buffer = Encoding.UTF8.GetBytes(answer);

            stream.Write(buffer, 0, buffer.Length);

            stream.Close();
            client.Close();
        }
    }
}