using System;
using System.Collections.Generic;

class EmployeeManager
{
    private Dictionary<string, string> users = new Dictionary<string, string>();

    public void Add(string login, string password)
    {
        users[login] = password;
    }

    public void Remove(string login)
    {
        users.Remove(login);
    }

    public void Update(string login, string newLogin, string newPassword)
    {
        if (users.ContainsKey(login))
        {
            users.Remove(login);
            users[newLogin] = newPassword;
        }
    }

    public string GetPassword(string login)
    {
        if (users.ContainsKey(login))
            return users[login];

        return "User not found";
    }
}

class Program
{
    static void Main()
    {
        EmployeeManager em = new EmployeeManager();

        em.Add("admin", "1234");
        em.Add("user1", "pass1");

        Console.WriteLine(em.GetPassword("admin"));

        em.Update("user1", "user2", "newpass");

        em.Remove("admin");

        Console.WriteLine(em.GetPassword("user2"));
    }
}