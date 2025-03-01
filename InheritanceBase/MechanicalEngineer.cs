using System;

sealed internal class MechanicalEngineer : Engineer
{
    private string _machineType;

    public MechanicalEngineer(int id, string name, string project, string machineType)
        : base(id, name, project)
    {
        this._machineType = machineType;
    }

    public override void DisplayDetails()
    {
        base.DisplayDetails();  // Call base class method
        Console.WriteLine($"Machine Type: {_machineType}");
    }

    public string MachineType
    {
        get { return _machineType; }
        set { _machineType = value; }
    }
}

