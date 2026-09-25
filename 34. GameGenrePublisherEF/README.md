# GameGenrePublisherEF

Навчальний EF Core проєкт на C# для Linux/Arch Linux.

## Структура

- `GameGenrePublisher.Models` — сутності `Game`, `Genre`, `Publisher`.
- `GameGenrePublisher.Data` — `DbContext` та конфігурація EF Core.
- `GameGenrePublisher.App` — консольна демонстрація CRUD та потрібних запитів.

## Що реалізовано

- `Games`, `Genres`, `Publishers`.
- `Game <-> Genre` — many-to-many через таблицю `GameGenres`.
- `Publisher -> Game` — one-to-many.
- `Game` має 5 властивостей, крім `Id`.
- `Publisher` має 5 властивостей, крім `Id`.
- CRUD для всіх трьох сутностей.
- Eager loading для `Games -> Genres` через `Include`.
- Eager loading для `Genres -> Games` через `Include`.
- Explicit loading для `Publisher -> Games` через `Entry(...).Collection(...).LoadAsync()`.
- Запити:
  1. Всі ігри вказаного жанру.
  2. Жанри вказаної гри.
  3. Всі ігри видавця.

## Запуск на Arch Linux

Перевір .NET:

```bash
dotnet --version
```

Якщо .NET SDK не встановлено:

```bash
sudo pacman -S dotnet-sdk
```

Далі з кореня solution:

```bash
dotnet restore
dotnet build
dotnet run --project GameGenrePublisher.App
```

База SQLite буде створена автоматично у:

`GameGenrePublisher.App/game_store.db`
