using TextBasedRPG;

public interface IWeapon
{
    void ApplyStats(Heroes heroes);
    void ApplyRefinement();
}

public class Sword : IWeapon
{
    public void ApplyStats(Heroes heroes)
    {
    }
    public void ApplyRefinement()
    {
    }
}

public class Staff : IWeapon
{
    public void ApplyStats(Heroes heroes)
    {
    }
    public void ApplyRefinement()
    {
    }
}

public class Bow : IWeapon
{
    public void ApplyStats(Heroes heroes)
    {
    }
    public void ApplyRefinement()
    {
    }
}
