using TextBasedRPG;

public interface IWeapon
{
    void ApplyStats(Heroes heroes);
}

public class Sword : IWeapon
{
    public void ApplyStats(Heroes heroes)
    {
        heroes.AttackPower += 2;
    }
}

public class Staff : IWeapon
{
    public void ApplyStats(Heroes heroes)
    {
        heroes.Element += 2;
    }
}

public class Bow : IWeapon
{
    public void ApplyStats(Heroes heroes)
    {
        heroes.Health += 2;
    }
}
