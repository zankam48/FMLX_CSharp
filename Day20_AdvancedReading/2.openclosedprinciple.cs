public class Robot
{
    public string Type { get; set; }
}

public class RobotOperator
{
    public void Operate(Robot robot)
    {
        if (robot.Type == "Cleaning")
        {
            Console.WriteLine("Cleaning...");
        }
        else if (robot.Type == "Fighting")
        {
            Console.WriteLine("Fighting...");
        }
        // violates ocp, harus manual masukin typenya
    }
}

/*** EXAMPLE OF THE CORRECT ONE ***/
public interface IRobot
{
    void Operate();
}

public class CleaningRobot : IRobot
{
    public void Operate()
    {
        Console.WriteLine("Cleaning...");
    }
}

public class FightingRobot : IRobot
{
    public void Operate()
    {
        Console.WriteLine("Fighting...");
    }
}
