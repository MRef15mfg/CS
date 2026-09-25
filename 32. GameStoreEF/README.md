# GameStoreEF

Практичне завдання з Entity Framework Core для відеоігор.

## Структура

- `GameStore.Models` — моделі `Game`, `Developer`, `Customer`, `Order`, `OrderItem`.
- `GameStore.Data` — `GameStoreDbContext` та налаштування зв'язків.
- `GameStore.App` — Seeder та 6 запитів.

## Зв'язки

- Developer -> Game: one-to-many.
- Customer -> Order: one-to-many.
- Order <-> Game: many-to-many через `OrderItem`.

## Seeder

Створюється:

- 5 розробників
- 10 ігор
- 8 клієнтів
- 10 замовлень
- 20 позицій `OrderItem`

## Запуск на Arch Linux

Потрібен .NET 10 SDK.

```bash
sudo pacman -S dotnet-sdk
```

Далі:

```bash
dotnet restore
dotnet build
dotnet run --project GameStore.App
```

База `game_store.db` створюється автоматично через `EnsureCreated()`.

## Якщо потрібно працювати через міграції

Встановити інструмент:

```bash
dotnet tool install --global dotnet-ef --version 10.0.0
```

Створити міграцію:

```bash
dotnet ef migrations add InitialCreate --project GameStore.Data --startup-project GameStore.App
```

Застосувати міграцію:

```bash
dotnet ef database update --project GameStore.Data --startup-project GameStore.App
```

Після переходу на міграції замість `EnsureCreated()` використовуйте `Database.Migrate()` у `GameStore.App/Program.cs`.
