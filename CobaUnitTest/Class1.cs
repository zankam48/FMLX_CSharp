public class RoboticArmController
{
    public double currentAngle {get; set;} = 0;

    public void RotateTo(double angle)
    {
        if (angle < 0 || angle > 180) 
            throw new ArgumentOutOfRangeException("Angle must be between 0 and 180!");
        currentAngle = angle;
    }

    public bool IsAtHomePosition()
    {
        return currentAngle == 0;
    }
}