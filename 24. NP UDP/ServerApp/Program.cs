using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class ServerGame
{
    private const int Port = 8888;
    private const int TotalRounds = 5;

    static async Task Main(string[] args)
    {
        TcpListener listener = new TcpListener(IPAddress.Any, Port);
        listener.Start();
        Console.WriteLine($"[SERVER] waiting for connection on port {Port}...");

        using TcpClient client = await listener.AcceptTcpClientAsync();
        Console.WriteLine("[SERVER] client connected. game starts.");

        using NetworkStream stream = client.GetStream();
        using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
        using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

        int serverScore = 0;
        int clientScore = 0;

        for (int round = 1; round <= TotalRounds; round++)
        {
            Console.Clear();
            Console.WriteLine($"=== ROUND {round} / {TotalRounds} ===");
            Console.WriteLine($"Score -> Server: {serverScore} | Client: {clientScore}\n");

            await writer.WriteLineAsync($"ROUND|{round}|{serverScore}|{clientScore}");

            string serverChoice = GetUserChoice();
            string clientChoice = await reader.ReadLineAsync();

            Console.WriteLine($"Your choice: {serverChoice}");
            Console.WriteLine($"Client choice: {clientChoice}");

            await writer.WriteLineAsync($"CHOICES|{serverChoice}|{clientChoice}");

            int result = DetermineWinner(serverChoice, clientChoice);
            if (result == 1)
            {
                serverScore++;
                Console.WriteLine("result: You win this round!");
            }
            else if (result == -1)
            {
                clientScore++;
                Console.WriteLine("result: Client wins this round!");
            }
            else
            {
                Console.WriteLine("result: Draw!");
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        Console.Clear();
        Console.WriteLine("=== FINAL RESULT ===");
        Console.WriteLine($"Final Score -> Server: {serverScore} | Client: {clientScore}");

        string finalMessage;
        if (serverScore > clientScore)
        {
            finalMessage = "Server wins the game!";
        }
        else if (clientScore > serverScore)
        {
            finalMessage = "Client wins the game!";
        }
        else
        {
            finalMessage = "The game ended in a draw!";
        }

        Console.WriteLine(finalMessage);
        await writer.WriteLineAsync($"FINAL|{serverScore}|{clientScore}|{finalMessage}");

        Console.WriteLine("\nDisconnecting...");
        listener.Stop();
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

    private static int DetermineWinner(string p1, string p2)
    {
        if (p1 == p2) return 0;
        if ((p1 == "Rock" && p2 == "Scissors") ||
            (p1 == "Scissors" && p2 == "Paper") ||
            (p1 == "Paper" && p2 == "Rock"))
        {
            return 1;
        }
        return -1;
    }
}