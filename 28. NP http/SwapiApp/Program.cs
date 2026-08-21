using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SwapiApp
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private const string BaseUrl = "https://swapi.py4e.com/api/";

        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Star Wars API (SWAPI) ===");
                Console.WriteLine("1. People");
                Console.WriteLine("2. Films");
                Console.WriteLine("3. Starships");
                Console.WriteLine("4. Vehicles");
                Console.WriteLine("5. Species");
                Console.WriteLine("6. Planets");
                Console.WriteLine("0. Вихід");
                Console.Write("\nОберіть категорію (0-6): ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                string endpoint = choice switch
                {
                    "1" => "people/",
                    "2" => "films/",
                    "3" => "starships/",
                    "4" => "vehicles/",
                    "5" => "species/",
                    "6" => "planets/",
                    _ => null
                };

                if (endpoint == null)
                {
                    Console.WriteLine("Невірний вибір. Натисніть клавішу для повернення...");
                    Console.ReadKey();
                    continue;
                }

                await FetchAndDisplay(endpoint);

                Console.WriteLine("\nНатисніть будь-яку клавішу для повернення у меню...");
                Console.ReadKey();
            }
        }

        static async Task FetchAndDisplay(string endpoint)
        {
            string url = BaseUrl + endpoint;
            Console.WriteLine($"\nОтримання даних із {url}...\n");

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                using JsonDocument doc = JsonDocument.Parse(json);
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty("results", out JsonElement results) && results.ValueKind == JsonValueKind.Array)
                {
                    int index = 1;
                    foreach (JsonElement item in results.EnumerateArray())
                    {
                        // У фільмів поле називається "title", в інших сутностей — "name"
                        string displayName = item.TryGetProperty("name", out JsonElement nameProp)
                            ? nameProp.GetString()
                            : item.TryGetProperty("title", out JsonElement titleProp)
                                ? titleProp.GetString()
                                : "Без назви";

                        Console.WriteLine($"{index++,2}. {displayName}");
                    }
                }
                else
                {
                    Console.WriteLine("Дані відсутні або мають невірний формат.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка запиту: {ex.Message}");
            }
        }
    }
}
