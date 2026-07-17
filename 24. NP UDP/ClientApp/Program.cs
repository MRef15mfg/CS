using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class ClientGame
{
    private const string ServerIp = "127.0.0.1";
    private const int Port = 8888;

    static async Task Main(string[] args)
    {
        try
        {
            using TcpClient client = new TcpClient();
            await client.ConnectAsync(ServerIp, Port);
            Console.WriteLine("[CLIENT] connected to server.");

            using NetworkStream stream = client.GetStream();
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            while (true)
            {
                string serverMessage = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(serverMessage)) break;

                string[] tokens = serverMessage.Split('|');
                string command = tokens[0];

                if (command == "ROUND")
                {
                    int round = int.Parse(tokens[1]);
                    int serverScore = int.Parse(tokens[2]);
                    int clientScore = int.Parse(tokens[3]);

                    Console.Clear();
                    Console.WriteLine($"=== ROUND {round} / 5 ===");
                    Console.WriteLine($"Score -> Server: {serverScore} | You: {clientScore}\n");

                    string myChoice = GetUserChoice();
                    await writer.WriteLineAsync(myChoice);

                    Console.WriteLine("\nWaiting for opponent...");
                }
                else if (command == "CHOICES")
                {
                    string serverChoice = tokens[1];
                    string clientChoice = tokens[2];

                    Console.WriteLine($"Your choice: {clientChoice}");
                    Console.WriteLine($"Server choice: {serverChoice}");

                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                }
                else if (command == "FINAL")
                {
                    int serverScore = int.Parse(tokens[1]);
                    int clientScore = int.Parse(tokens[2]);
                    string message = tokens[3];

                    Console.Clear();
                    Console.WriteLine("=== FINAL RESULT ===");
                    Console.WriteLine($"Final Score -> Server: {serverScore} | You: {clientScore}");
                    Console.WriteLine(message);
                    break;
                }
            }

            Console.WriteLine("\nDisconnected from server.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.ReadLine();
    }

    private static string GetUserChoice()
    {
        while (true)
        {
            Console.Write("Enter choice (Rock, Scissors, Paper): ");
            string input = Console.ReadLine()?.Trim();
            if (input == "Rock" || input == "Scissors" || input == "Paper")
            {
                return input;
            }
            Console.WriteLine("Invalid choice. Try again.");
        }
    }
}