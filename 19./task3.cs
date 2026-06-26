using System;

class CreditCard
{
    public string CardNumber { get; set; }
    public string Owner { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Pin { get; private set; }
    public decimal CreditLimit { get; set; }
    public decimal Balance { get; private set; }

    private bool creditStarted = false;

    public event Action<decimal> DepositEvent;
    public event Action<decimal> WithdrawEvent;
    public event Action CreditStartedEvent;
    public event Action TargetReachedEvent;
    public event Action PinChangedEvent;

    public CreditCard(string number, string owner, DateTime date,
                      string pin, decimal limit, decimal money)
    {
        CardNumber = number;
        Owner = owner;
        ExpirationDate = date;
        Pin = pin;
        CreditLimit = limit;
        Balance = money;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
        DepositEvent?.Invoke(amount);
    }

    public void Withdraw(decimal amount)
    {
        if (Balance + CreditLimit >= amount)
        {
            Balance -= amount;
            WithdrawEvent?.Invoke(amount);

            if (Balance < 0 && !creditStarted)
            {
                creditStarted = true;
                CreditStartedEvent?.Invoke();
            }
        }
        else
        {
            Console.WriteLine("Недостатньо коштів.");
        }
    }

    public void ChangePin(string newPin)
    {
        Pin = newPin;
        PinChangedEvent?.Invoke();
    }

    public void CheckTarget(decimal target)
    {
        if (Balance >= target)
            TargetReachedEvent?.Invoke();
    }
}

class Program
{
    static void Main()
    {
        CreditCard card = new CreditCard(
            "1111 2222 3333 4444",
            "Іван Петренко",
            new DateTime(2028, 10, 1),
            "1234",
            5000,
            1000);

        card.DepositEvent += x => Console.WriteLine("Поповнення: " + x);
        card.WithdrawEvent += x => Console.WriteLine("Витрата: " + x);
        card.CreditStartedEvent += () => Console.WriteLine("Почалося використання кредитних коштів.");
        card.TargetReachedEvent += () => Console.WriteLine("Досягнуто потрібної суми.");
        card.PinChangedEvent += () => Console.WriteLine("PIN змінено.");

        card.Deposit(3000);
        card.CheckTarget(3000);

        card.Withdraw(4500);

        card.ChangePin("5678");
    }
}