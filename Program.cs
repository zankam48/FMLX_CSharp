using System;
class Program
{
    static void Main(string[] args)
    {
        SoftwareEngineer softwareEngineer = new SoftwareEngineer(1, "andi", "Software Development", "C#");
        MechanicalEngineer mechanicalEngineer = new MechanicalEngineer(2, "budi", "Machine Design", "Hydraulic Press");

        Console.WriteLine("Software Engineer Details:");
        softwareEngineer.DisplayDetails();

        Console.WriteLine("\nMechanical Engineer Details:");
        mechanicalEngineer.DisplayDetails();
    }
}