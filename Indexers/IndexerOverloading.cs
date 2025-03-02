using System;

public class Heroes
{
    private string[] _heroes = new string[] {"Grim Reaper", "Cyber Medic", "Heavy Crasher"};
    private string[] _role = new string[] {"Attacker", "Healer", "Tank"};

    public string this[int index]
    {
        set
        {
            this._heroes[index] = value;
        }
        get
        {
            return this._heroes[index];
        }
    }

    public string this[string role]
    {
        set
        {
            this._role[Array.IndexOf(_role, role)] = value;
        }
        get
        {
            return _heroes[Array.IndexOf(_role, role)];
        }
    }
}