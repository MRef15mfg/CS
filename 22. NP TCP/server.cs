using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static List<string> quotes = new List<string>()
    {
        "Не бійся повільного руху, бійся зупинки.",
        "Успіх — це щоденні маленькі кроки.",
        "Завтра починається сьогодні.",
        "Ти сильніший, ніж думаєш.",
        "Кожна помилка — це крок до успіху."
    };

    static Random rnd = new Random();
    static object rndLock = new object();
    static object logLock = new object();

    static void Main()
    {
        TcpListener server = new TcpListener(IPAddress.Any, 5000);
        server.Start();

        Console.WriteLine("Сервер запущено...");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Thread t = new Thread(HandleClient);
            t.Start(client);
        }
    }

    static void HandleClient(object obj)
    {
        TcpClient client = (TcpClient)obj;
        NetworkStream stream = client.GetStream();

        IPEndPoint ep = (IPEndPoint)client.Client.RemoteEndPoint;
        string ip = ep.Address.ToString();

        Log($"Підключився {ip} о {DateTime.Now:HH:mm:ss}");

        byte[] buffer = new byte[1024];

        try
        {
            while (true)
            {
                int count = stream.Read(buffer, 0, buffer.Length);
                if (count == 0) break;

                string request = Encoding.UTF8.GetString(buffer, 0, count).Trim();

                if (request.ToLower() == "exit")
                {
                    Log($"Відключився {ip}");
                    break;
                }

                string quote = GetQuote();

                byte[] response = Encoding.UTF8.GetBytes(quote);
                stream.Write(response, 0, response.Length);
            }
        }
        catch
        {
            Log($"Помилка з клієнтом {ip}");
        }

        stream.Close();
        client.Close();
    }

    static string GetQuote()
    {
        lock (rndLock)
        {
            return quotes[rnd.Next(quotes.Count)];
        }
    }

    static void Log(string text)
    {
        lock (logLock)
        {
            Console.WriteLine(text);
            File.AppendAllText("log.txt", text + Environment.NewLine);
        }
    }
}