using System;

class Freezer
{
    private string model;
    private string brand;
    private double volume;
    private int temperature;
    private bool noFrost;

    public string Model
    {
        get { return model; }
        set { model = value; }
    }

    public string Brand
    {
        get { return brand; }
        set { brand = value; }
    }

    public double Volume
    {
        get { return volume; }
        set { volume = value; }
    }

    public int Temperature
    {
        get { return temperature; }
        set { temperature = value; }
    }

    public bool NoFrost
    {
        get { return noFrost; }
        set { noFrost = value; }
    }

    public Freezer()
        : this("невідомо", "невідомо", 0, 0.0, false)
    {
    }

    public Freezer(string model, string brand, int temperature, double volume, bool noFrost)
    {
        this.model = model;
        this.brand = brand;
        this.temperature = temperature;
        this.volume = volume;
        this.noFrost = noFrost;
    }

    public Freezer(string model, string brand)
        : this(model, brand, -18, 100.0, false)
    {
    }

    public override string ToString()
    {
        return $"Модель: {model}, Бренд: {brand}, Об’єм: {volume} л, Температура: {temperature}°C, NoFrost: {noFrost}";
    }
}

class Program
{
    static void Main()
    {
        Freezer[] freezers = new Freezer[5];

        freezers[0] = new Freezer();
        freezers[1] = new Freezer("FZ-100", "Samsung", -20, 150.5, true);
        freezers[2] = new Freezer("FZ-200", "LG", -18, 200.0, true);
        freezers[3] = new Freezer("FZ-300", "Bosch");
        freezers[4] = new Freezer("FZ-400", "Whirlpool", -22, 180.0, false);

        for (int i = 0; i < freezers.Length; i++)
        {
            Console.WriteLine(freezers[i]);
        }
    }
}