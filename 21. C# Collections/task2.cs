using System;
using System.Collections.Generic;

class MyStack<T>
{
    private List<T> items = new List<T>();

    public void Push(T item)
    {
        items.Add(item);
    }

    public T Pop()
    {
        if (items.Count == 0)
            throw new Exception("Stack is empty");

        T value = items[^1];
        items.RemoveAt(items.Count - 1);
        return value;
    }

    public T Peek()
    {
        if (items.Count == 0)
            throw new Exception("Stack is empty");

        return items[^1];
    }

    public int Count
    {
        get { return items.Count; }
    }
}

class Program
{
    static void Main()
    {
        MyStack<int> stack = new MyStack<int>();

        stack.Push(10);
        stack.Push(20);
        stack.Push(30);

        Console.WriteLine("Count: " + stack.Count);
        Console.WriteLine("Peek: " + stack.Peek());
        Console.WriteLine("Pop: " + stack.Pop());
        Console.WriteLine("Count after pop: " + stack.Count);
    }
}