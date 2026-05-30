using System;

class CreditCard
{
    public string Number { get; set; }
    public string Owner { get; set; }
    public string CVC { get; set; }
    public DateTime ExpirationDate { get; set; }

    public CreditCard(string number, string owner,
                      string cvc, DateTime expirationDate)
    {
        if (number.Length != 16)
            throw new Exception("Номер картки повинен містити 16 цифр.");

        if (cvc.Length != 3)
            throw new Exception("CVC повинен містити 3 цифри.");

        if (expirationDate < DateTime.Now)
            throw new Exception("Термін дії картки минув.");

        Number = number;
        Owner = owner;
        CVC = cvc;
        ExpirationDate = expirationDate;
    }

    public void ShowInfo()
    {
        Console.WriteLine("Номер: " + Number);
        Console.WriteLine("Власник: " + Owner);
        Console.WriteLine("CVC: " + CVC);
        Console.WriteLine("Термін дії: " +
                          ExpirationDate.ToShortDateString());
    }
}

class Program
{
    static void Main()
    {
        try
        {
            CreditCard card = new CreditCard(
                "1234567812345678",
                "Іван Петренко",
                "123",
                new DateTime(2028, 12, 31)
            );

            card.ShowInfo();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
    }
}