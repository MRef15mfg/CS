using Microsoft.EntityFrameworkCore;
using ProductCatalogEF.Models;

namespace ProductCatalogEF.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(ProductCatalogDbContext db)
    {
        await db.Database.EnsureCreatedAsync();

        if (await db.Products.AnyAsync())
            return;

        var categories = new List<Category>
        {
            new() { Id = 1, Name = "Ноутбуки", Description = "Ноутбуки для роботи, навчання та ігор." },
            new() { Id = 2, Name = "Смартфони", Description = "Сучасні смартфони для щоденного використання." },
            new() { Id = 3, Name = "Навушники", Description = "Дротові та бездротові навушники." },
            new() { Id = 4, Name = "Клавіатури", Description = "Механічні та мембранні клавіатури." },
            new() { Id = 5, Name = "Миші", Description = "Комп'ютерні миші для роботи та геймінгу." },
            new() { Id = 6, Name = "Монітори", Description = "Монітори для офісу, дизайну та ігор." },
            new() { Id = 7, Name = "Ігрові консолі", Description = "Популярні домашні і портативні консолі." },
            new() { Id = 8, Name = "Фотоапарати", Description = "Камери для фото та відеозйомки." },
            new() { Id = 9, Name = "Годинники", Description = "Смарт-годинники для спорту та повсякденного життя." },
            new() { Id = 10, Name = "Аксесуари", Description = "Корисні аксесуари для техніки." }
        };

        await db.Categories.AddRangeAsync(categories);

        var products = new List<Product>
        {
            // 1
            new() { Id = 1, Name = "Lenovo IdeaPad 5", Price = 799.99m, Description = "Універсальний ноутбук з 15.6-дюймовим дисплеєм та швидким SSD.", ImageUrl = "💻", CategoryId = 1 },
            new() { Id = 2, Name = "ASUS TUF Gaming A15", Price = 1199.99m, Description = "Ігровий ноутбук із продуктивною відеокартою та швидким дисплеєм.", ImageUrl = "🎮", CategoryId = 1 },
            new() { Id = 3, Name = "HP Pavilion 14", Price = 749.50m, Description = "Компактний ноутбук для навчання, офісу та роботи з документами.", ImageUrl = "💼", CategoryId = 1 },
            new() { Id = 4, Name = "Acer Aspire 5", Price = 689.00m, Description = "Практична модель з хорошим балансом продуктивності та автономності.", ImageUrl = "🖥️", CategoryId = 1 },
            new() { Id = 5, Name = "MacBook Air M3", Price = 1299.00m, Description = "Тонкий ноутбук Apple з чипом M3 та тривалою автономною роботою.", ImageUrl = "🍎", CategoryId = 1 },

            // 2
            new() { Id = 6, Name = "Samsung Galaxy S24", Price = 899.99m, Description = "Флагманський смартфон з яскравим AMOLED-дисплеєм.", ImageUrl = "📱", CategoryId = 2 },
            new() { Id = 7, Name = "Google Pixel 9", Price = 949.00m, Description = "Смартфон з чистим Android та акцентом на мобільну фотографію.", ImageUrl = "📸", CategoryId = 2 },
            new() { Id = 8, Name = "Xiaomi 14", Price = 699.99m, Description = "Потужний смартфон з якісним дисплеєм і швидкою зарядкою.", ImageUrl = "⚡", CategoryId = 2 },
            new() { Id = 9, Name = "OnePlus 12", Price = 799.00m, Description = "Швидкий смартфон з великим акумулятором та плавним дисплеєм.", ImageUrl = "🚀", CategoryId = 2 },
            new() { Id = 10, Name = "iPhone 15", Price = 999.00m, Description = "Смартфон Apple з яскравим дисплеєм та сучасною камерою.", ImageUrl = "📱", CategoryId = 2 },

            // 3
            new() { Id = 11, Name = "Sony WH-1000XM5", Price = 349.99m, Description = "Бездротові навушники з активним шумозаглушенням.", ImageUrl = "🎧", CategoryId = 3 },
            new() { Id = 12, Name = "AirPods Pro 2", Price = 249.00m, Description = "Компактні навушники Apple з ANC та прозорим режимом.", ImageUrl = "🎵", CategoryId = 3 },
            new() { Id = 13, Name = "Sennheiser HD 560S", Price = 179.00m, Description = "Дротові накладні навушники для якісного прослуховування музики.", ImageUrl = "🎼", CategoryId = 3 },
            new() { Id = 14, Name = "JBL Live 660NC", Price = 119.99m, Description = "Універсальні бездротові навушники з ANC.", ImageUrl = "🔊", CategoryId = 3 },
            new() { Id = 15, Name = "HyperX Cloud III", Price = 129.99m, Description = "Ігрова гарнітура з комфортною посадкою та чітким мікрофоном.", ImageUrl = "🎙️", CategoryId = 3 },

            // 4
            new() { Id = 16, Name = "Keychron K2", Price = 89.99m, Description = "Компактна механічна клавіатура для роботи та геймінгу.", ImageUrl = "⌨️", CategoryId = 4 },
            new() { Id = 17, Name = "Logitech MX Keys S", Price = 109.00m, Description = "Тиха бездротова клавіатура для продуктивної роботи.", ImageUrl = "⌨️", CategoryId = 4 },
            new() { Id = 18, Name = "Razer BlackWidow V4", Price = 169.99m, Description = "Механічна ігрова клавіатура з підсвічуванням.", ImageUrl = "💡", CategoryId = 4 },
            new() { Id = 19, Name = "Corsair K70 RGB", Price = 159.50m, Description = "Повнорозмірна механічна клавіатура для геймерів.", ImageUrl = "🌈", CategoryId = 4 },
            new() { Id = 20, Name = "SteelSeries Apex 3", Price = 69.99m, Description = "Вологостійка ігрова клавіатура з RGB-підсвічуванням.", ImageUrl = "🎮", CategoryId = 4 },

            // 5
            new() { Id = 21, Name = "Logitech G Pro X Superlight", Price = 149.99m, Description = "Легка бездротова миша для професійного геймінгу.", ImageUrl = "🖱️", CategoryId = 5 },
            new() { Id = 22, Name = "Razer DeathAdder V3", Price = 89.99m, Description = "Ергономічна ігрова миша з точним сенсором.", ImageUrl = "🖱️", CategoryId = 5 },
            new() { Id = 23, Name = "SteelSeries Rival 5", Price = 79.50m, Description = "Універсальна геймерська миша з додатковими кнопками.", ImageUrl = "🎯", CategoryId = 5 },
            new() { Id = 24, Name = "Microsoft Bluetooth Mouse", Price = 29.99m, Description = "Компактна миша Bluetooth для офісної роботи.", ImageUrl = "💼", CategoryId = 5 },
            new() { Id = 25, Name = "Logitech MX Master 3S", Price = 99.99m, Description = "Професійна миша для продуктивної роботи.", ImageUrl = "⚙️", CategoryId = 5 },

            // 6
            new() { Id = 26, Name = "LG UltraGear 27GP850", Price = 329.99m, Description = "27-дюймовий ігровий монітор з високою частотою оновлення.", ImageUrl = "🖥️", CategoryId = 6 },
            new() { Id = 27, Name = "Dell UltraSharp U2723QE", Price = 549.00m, Description = "4K-монітор для офісу та професійної роботи з кольором.", ImageUrl = "🎨", CategoryId = 6 },
            new() { Id = 28, Name = "Samsung Odyssey G5", Price = 299.99m, Description = "Вигнутий ігровий монітор з високою частотою оновлення.", ImageUrl = "🎮", CategoryId = 6 },
            new() { Id = 29, Name = "AOC 24G2", Price = 179.00m, Description = "Доступний геймерський монітор з IPS-матрицею.", ImageUrl = "🖥️", CategoryId = 6 },
            new() { Id = 30, Name = "ASUS ProArt PA278QV", Price = 399.99m, Description = "Монітор для дизайну та створення контенту.", ImageUrl = "🖌️", CategoryId = 6 },

            // 7
            new() { Id = 31, Name = "PlayStation 5 Slim", Price = 499.99m, Description = "Сучасна домашня консоль Sony для ігор нового покоління.", ImageUrl = "🎮", CategoryId = 7 },
            new() { Id = 32, Name = "Xbox Series X", Price = 499.00m, Description = "Потужна консоль Microsoft з підтримкою 4K-геймінгу.", ImageUrl = "🟩", CategoryId = 7 },
            new() { Id = 33, Name = "Nintendo Switch OLED", Price = 349.99m, Description = "Гібридна консоль з яскравим OLED-дисплеєм.", ImageUrl = "🕹️", CategoryId = 7 },
            new() { Id = 34, Name = "Steam Deck OLED", Price = 549.00m, Description = "Портативний ПК для запуску бібліотеки Steam.", ImageUrl = "🎮", CategoryId = 7 },
            new() { Id = 35, Name = "Nintendo Switch Lite", Price = 199.99m, Description = "Легка портативна консоль для ігор у дорозі.", ImageUrl = "🎲", CategoryId = 7 },

            // 8
            new() { Id = 36, Name = "Canon EOS R10", Price = 979.00m, Description = "Бездзеркальна камера для фото та відео.", ImageUrl = "📷", CategoryId = 8 },
            new() { Id = 37, Name = "Sony Alpha a6700", Price = 1399.00m, Description = "Компактна бездзеркальна камера з потужним автофокусом.", ImageUrl = "📸", CategoryId = 8 },
            new() { Id = 38, Name = "Nikon Z50", Price = 899.99m, Description = "Універсальна бездзеркальна камера для творчих зйомок.", ImageUrl = "📷", CategoryId = 8 },
            new() { Id = 39, Name = "Fujifilm X-S20", Price = 1199.00m, Description = "Камера для фото та відео з сучасною стабілізацією.", ImageUrl = "🎞️", CategoryId = 8 },
            new() { Id = 40, Name = "GoPro HERO13 Black", Price = 399.99m, Description = "Екшн-камера для активного відпочинку та подорожей.", ImageUrl = "🏔️", CategoryId = 8 },

            // 9
            new() { Id = 41, Name = "Apple Watch Series 10", Price = 429.00m, Description = "Смарт-годинник для спорту, дзвінків та повсякденних задач.", ImageUrl = "⌚", CategoryId = 9 },
            new() { Id = 42, Name = "Samsung Galaxy Watch 7", Price = 299.99m, Description = "Смарт-годинник з функціями здоров'я та фітнесу.", ImageUrl = "⌚", CategoryId = 9 },
            new() { Id = 43, Name = "Garmin Venu 3", Price = 449.00m, Description = "Спортивний смарт-годинник з розширеним моніторингом активності.", ImageUrl = "🏃", CategoryId = 9 },
            new() { Id = 44, Name = "Huawei Watch GT 5", Price = 249.99m, Description = "Стильний годинник з великим часом автономної роботи.", ImageUrl = "⌚", CategoryId = 9 },
            new() { Id = 45, Name = "Amazfit Balance", Price = 199.00m, Description = "Легкий смарт-годинник для спорту та щоденного використання.", ImageUrl = "💪", CategoryId = 9 },

            // 10
            new() { Id = 46, Name = "Anker 737 Power Bank", Price = 129.99m, Description = "Потужний повербанк для ноутбуків та смартфонів.", ImageUrl = "🔋", CategoryId = 10 },
            new() { Id = 47, Name = "Apple MagSafe Charger", Price = 39.00m, Description = "Бездротовий зарядний пристрій MagSafe.", ImageUrl = "🔌", CategoryId = 10 },
            new() { Id = 48, Name = "Baseus 100W USB-C Hub", Price = 79.99m, Description = "Багатофункціональний USB-C хаб для ноутбука.", ImageUrl = "🔗", CategoryId = 10 },
            new() { Id = 49, Name = "Logitech C920 Webcam", Price = 69.99m, Description = "Вебкамера Full HD для відеозв'язку та стримінгу.", ImageUrl = "📹", CategoryId = 10 },
            new() { Id = 50, Name = "UGREEN USB-C Cable", Price = 14.99m, Description = "Надійний USB-C кабель для зарядки та передачі даних.", ImageUrl = "🔌", CategoryId = 10 }
        };

        await db.Products.AddRangeAsync(products);
        await db.SaveChangesAsync();
    }
}
