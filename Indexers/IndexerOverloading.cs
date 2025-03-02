using System;

public class Heroes
{
    private string[] _brands = new string[] {"Grim Reaper", "Cyber Medic", "Heavy Crasher"};
    private string[] _role = new string[] {"Attacker", "Healer", "Tank"};

    public string this[int index]
    {
        set
        {
            this._brands[index] = value;
        }
        get
        {
            return this._brands[index];
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
            
        }
    }
}