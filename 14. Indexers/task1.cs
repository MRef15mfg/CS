using System;
using System.Collections.Generic;

class ReadingList
{
    private readonly List<string> books = new List<string>();

    public int Count
    {
        get { return books.Count; }
    }

    public string this[int index]
    {
        get { return books[index]; }
        set { books[index] = value; }
    }

    public string this[Index index]
    {
        get { return books[index]; }
        set { books[index] = value; }
    }

    public string[] this[Range range]
    {
        get { return books.GetRange(range.Start.Value, range.End.Value - range.Start.Value).ToArray(); }
    }

    public void AddBook(string book)
    {
        if (!books.Contains(book))
        {
            books.Add(book);
        }
    }

    public void RemoveBook(string book)
    {
        books.Remove(book);
    }

    public bool ContainsBook(string book)
    {
        return books.Contains(book);
    }

    public void ShowBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("Список порожній");
            return;
        }

        foreach (string book in books)
        {
            Console.WriteLine(book);
        }
    }
}

class Program
{
    static void Main()
    {
        ReadingList list = new ReadingList();

        list.AddBook("1984");
        list.AddBook("Майстер і Маргарита");
        list.AddBook("Гаррі Поттер");

        Console.WriteLine("Усі книги:");
        list.ShowBooks();

        Console.WriteLine();

        Console.WriteLine("Книга за індексом 0: " + list[0]);
        Console.WriteLine("Книга за індексом ^1: " + list[^1]);

        Console.WriteLine();

        Console.WriteLine("Чи є книга '1984': " + list.ContainsBook("1984"));

        list.RemoveBook("1984");

        Console.WriteLine("Після видалення:");
        list.ShowBooks();
    }
}