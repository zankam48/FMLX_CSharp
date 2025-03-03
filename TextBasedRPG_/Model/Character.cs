using System;

namespace TextBasedRPG;

public abstract class Character
{
    private int _id;
    private string _name;
    private int _health;
    private int _healthMax;

    public string Name { get; set; }
    public int Id { get; set; }
    

    public virtual void GetInfo()
    {
        Console.WriteLine($"Name: {_name}, Health: {_health}/{_healthMax}");
    }

}
