using System;

// wrong code, not srp
public class RobotNotSrp
{
    public string Name { get; set; }
    public int Position { get; set; }

    public void MoveForward(int steps)
    {
        Position += steps;
        Console.WriteLine($"{Name} moved to position {Position}");
        SaveToFile();
    }

    public void SaveToFile()
    {
        File.WriteAllText("robot_log.txt", $"{Name} is at position {Position}");
    }
}


// srp code
public class RobotSrp
{
    public string Name { get; set; }
    public int Position { get; private set; }

    public void MoveForward(int steps)
    {
        Position += steps;
    }
}

public class RobotLogger
{
    public void LogPosition(RobotSrp robot)
    {
        File.WriteAllText("robot_log.txt", $"{robot.Name} is at position {robot.Position}");
    }
}

public class RobotDisplay
{
    public void ShowPosition(RobotSrp robot)
    {
        Console.WriteLine($"{robot.Name} is at position {robot.Position}");
    }
}
