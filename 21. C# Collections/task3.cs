using System;
using System.Collections;
using System.Collections.Generic;

class SeaCreature
{
    public string Name { get; set; }
    public string Type { get; set; }

    public SeaCreature(string name, string type)
    {
        Name = name;
        Type = type;
    }

    public override string ToString()
    {
        return $"{Name} ({Type})";
    }
}

class Aquarium : IEnumerable<SeaCreature>
{
    private List<SeaCreature> creatures = new List<SeaCreature>();

    public void Add(SeaCreature c)
    {
        creatures.Add(c);
    }

    public IEnumerator<SeaCreature> GetEnumerator()
    {
        return creatures.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Program
{
    static void Main()
    {
        Aquarium aq = new Aquarium();

        aq.Add(new SeaCreature("Nemo", "Fish"));
        aq.Add(new SeaCreature("Dory", "Fish"));
        aq.Add(new SeaCreature("Sharky", "Shark"));

        foreach (var c in aq)
        {
            Console.WriteLine(c);
        }
    }
}