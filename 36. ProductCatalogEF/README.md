# ProductCatalogEF

ASP.NET Core MVC + Entity Framework Core + SQLite.

## Реалізовано

- 10 категорій
- 50 товарів
- рівно 5 товарів у кожній категорії
- головна сторінка з усіма товарами
- сторінка детальної інформації для товару з `Id = 1`
- seed виконується автоматично при запуску

## Запуск на Arch Linux

```bash
cd ProductCatalogEF
dotnet restore
dotnet build
./run.sh
```

Після запуску відкрий:

http://localhost:5000

Перша детальна сторінка товару:

http://localhost:5000/Products/Details/1

База SQLite:

`product_catalog.db`
