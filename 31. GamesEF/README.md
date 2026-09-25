# Практичне завдання — Вступ до Entity Framework Core

Проєкт зроблений під Linux/Arch Linux і використовує Code First + SQLite.

## Структура

- `Games.Models` — DLL з моделлю `Game` та enum `GameMode`.
- `Games.Data` — DLL з `GameDbContext`, `DbSet<Game>` та EF Core migrations.
- `Games.App` — консольний застосунок для створення/оновлення БД, додавання та виведення ігор.

## 1. Перевірити .NET

```bash
dotnet --version
```

Потрібен .NET SDK 10.x. На Arch Linux:

```bash
sudo pacman -S dotnet-sdk
```

## 2. Встановити EF CLI

```bash
dotnet tool install --global dotnet-ef --version 10.0.0
```

Якщо `dotnet-ef` вже встановлений:

```bash
dotnet tool update --global dotnet-ef --version 10.0.0
```

Перевірка:

```bash
dotnet ef --version
```

## 3. Відновити пакети та зібрати solution

Запускати команди з каталогу, де знаходиться `GamesEF.sln`:

```bash
dotnet restore
dotnet build
```

## 4. Міграції

У репозиторії вже є дві міграції:

- `InitialCreate`
- `AddGameProperties`

Їх можна застосувати:

Щоб база створювалася саме в `Games.App/games.db`, запускайте команду з каталогу `Games.App`:

```bash
cd Games.App
dotnet ef database update --project ../Games.Data --startup-project .
cd ..
```

Або просто запустіть програму: вона сама застосує міграції через `Database.Migrate()`.

## 5. Запуск

Найпростіше з кореня solution:

```bash
./run.sh
```

Або вручну:

```bash
cd Games.App
dotnet run
```

База матиме назву `games.db`.

## 6. Перевірити список міграцій

```bash
dotnet ef migrations list --project Games.Data --startup-project Games.App
```

Очікується:

```text
InitialCreate
AddGameProperties
```

## 7. Подивитися БД через sqlite3 (необов'язково)

```bash
sudo pacman -S sqlite
cd Games.App
sqlite3 games.db
```

Потім у sqlite3:

```sql
.tables
.schema Games
SELECT * FROM Games;
.quit
```

## Важливо

Не видаляйте папку `Games.Data/Migrations`: вона потрібна для демонстрації історії зміни моделі.
